using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap beachMap, groundMap, decorativeGroundMap, decorationsMap, farmMap, structuresMap;

    [SerializeField]
    private TileBase waterTile, beachTile, groundTile, grassTile, farmTile, houseTile;

    [SerializeField]
    private TileBase[] houseTiles;

    [SerializeField]
    private GameObject selectedObject;

    [SerializeField]
    private List<Button> buildableObjects = new List<Button>();

    public static List<Vector3Int> objectsBuilt = new List<Vector3Int>();
    public static List<Tilemap> mapBuiltOn = new List<Tilemap>();

    public static bool currentlyBuilding = false;
    private int currentObject;

    private bool validPlacement = false;

    private int tilePrice;
    private Sprite tileSprite;
    private TileBase tileBaseToBuild;
    private Tilemap mapToBuildOn;

    public GameObject tileCostText;

    public void SetBuildingSprite(Sprite sprite)
    {
        tileSprite = sprite;
    }

    // Enter building mode and select the object to build
    public void StartBuildingTileBase(TileBase tile)
    {
        currentlyBuilding = true;

        tileBaseToBuild = tile;

        selectedObject.GetComponent<SpriteRenderer>().sprite = tileSprite; // Need a new way to select which sprite to create
        selectedObject.transform.position = UtilityHelper.GetMouseWorldPosition();

        if (selectedObject.activeSelf == false)
        {
            selectedObject.SetActive(true);
        }
    }

    public void SetTilePrice(int cost)
    {
        tilePrice = cost;
    }

    public void SetMapToBuildOn(Tilemap map)
    {
        mapToBuildOn = map;
    }

    // Exit building mode
    public void CancelBuilding()
    {
        currentlyBuilding = false;
        selectedObject.SetActive(false);

        // This works but we want to revert not set to null.
        // This could be done by making a seperate list that stores the previous tile but this would cause issues with farms

        // Maybe we could put all new tiles on a temporary tilemap, and upon hitting confirm they are set on the correct maps

        // for (int i = 0; i < objectsBuilt.Count; i++)
        // {
        //     mapBuiltOn[i].SetTile(objectsBuilt[i], null);
        // }

        objectsBuilt.Clear();
        mapBuiltOn.Clear();
    }

    public void ConfirmBuilding()
    {
        currentlyBuilding = false;
        selectedObject.SetActive(false);
        objectsBuilt.Clear();
        mapBuiltOn.Clear();
    }

    // Have the selected object follow the mouse while snapping to the grid
    private void Update()
    {
        if (currentlyBuilding)
        {
            Vector3 gridPosition = UtilityHelper.SnapToGrid(UtilityHelper.GetMouseWorldPosition());
            selectedObject.transform.position = gridPosition;
            // This could be called whenever cell changes rather than constantly (small optimisation)
            Vector3Int tileLocation = groundMap.WorldToCell(UtilityHelper.GetMouseWorldPosition());

            selectedObject.GetComponent<SpriteRenderer>().color = validPlacement ? Color.white : Color.red; // Change white to regular colour maybe?

            if (Input.GetMouseButtonDown(0) && validPlacement)
            {
                tileLocation = beachMap.WorldToCell(UtilityHelper.GetMouseWorldPosition());

                // MapManager.PlaceTileLarge(mapToBuildOn, tileLocation, houseTiles, tilePrice, 3, 3);
                MapManager.PlaceTile(mapToBuildOn, tileLocation, tileBaseToBuild, tilePrice);

                if (GlobalVariables.Variables.playerMoney >= tilePrice) // Checks if the player has enough money here AND in mapmanager, needs optimising
                {
                    GameObject costText = Instantiate(tileCostText, tileLocation, Quaternion.identity) as GameObject;
                    costText.transform.GetChild(0).GetComponent<TextMesh>().text = "" + tilePrice;
                }
            }

            // Check if the object can be placed there
            if (tileBaseToBuild == beachTile)
            {
                if (
                        ((beachMap.GetTile(tileLocation) == null) || (beachMap.GetTile(tileLocation) == waterTile)) &&
                        ((beachMap.GetTile(new Vector3Int(tileLocation.x, tileLocation.y+1, 0)) != waterTile) ||
                        (beachMap.GetTile(new Vector3Int(tileLocation.x, tileLocation.y-1, 0)) != waterTile) ||
                        (beachMap.GetTile(new Vector3Int(tileLocation.x+1, tileLocation.y, 0)) != waterTile) ||
                        (beachMap.GetTile(new Vector3Int(tileLocation.x-1, tileLocation.y, 0)) != waterTile))
                    )
                {
                    validPlacement = true;
                }
                else
                {
                    validPlacement = false;
                }
            }
            
            if (tileBaseToBuild == groundTile)
            {
                validPlacement = isValidPlacement(beachMap, tileLocation, beachTile, 1, 1);
            }

            if ((tileBaseToBuild == grassTile) || tileBaseToBuild == farmTile)
            {
                validPlacement = isValidPlacement(groundMap, tileLocation, groundTile, 1, 1);
            }

            if (tileBaseToBuild == houseTile)
            {
                validPlacement = isValidPlacement(groundMap, tileLocation, groundTile, 3, 3);
            }
        }
    }

    // Can simplify this and reduce duplicate code
    public bool isValidPlacement(Tilemap requiredTileMap, Vector3Int tileLocation, TileBase requiredTileBase, int structureHeight, int structureWidth)
    {
        int tilesValid = 0;

        for (int i = 0; i < structureWidth; i++)
        {
            for (int j = 0; j < structureHeight; j++)
            {
                if (requiredTileMap.GetTile(new Vector3Int(tileLocation.x+i, tileLocation.y+j, 0)) == requiredTileBase)
                {
                    tilesValid += 1;
                }
            }
        }

        return tilesValid == (structureHeight*structureWidth); // Return true if all tiles are valid
    }
}

