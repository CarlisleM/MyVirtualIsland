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
    private Tile sandTile, groundTile, grassTile, farmTile, houseTile;

    [SerializeField]
    private TileBase farmScriptedTile;

    [SerializeField]
    private TileBase waterTile, beachTile;

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
    private Tile tileToBuild;
    private TileBase tileBaseToBuild;
    private Tilemap mapToBuildOn;

    public GameObject tileCostText;
    
    // Enter building mode and select the object to build
    public void StartBuildingTile(Tile tile)
    {
        currentlyBuilding = true;

        tileBaseToBuild = null;
        tileToBuild = tile;
        
        selectedObject.GetComponent<SpriteRenderer>().sprite = tile.sprite;
        selectedObject.transform.position = UtilityHelper.GetMouseWorldPosition();

        if (selectedObject.activeSelf == false)
        {
            selectedObject.SetActive(true);
        }
    }

    public void StartBuildingTileBase(TileBase tile)
    {
        currentlyBuilding = true;

        tileToBuild = null;
        tileBaseToBuild = tile;

        selectedObject.GetComponent<SpriteRenderer>().sprite = sandTile.sprite; // Need a new way to select which sprite to create
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

            if (!validPlacement)
            {
                selectedObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                selectedObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (Input.GetMouseButtonDown(0) && validPlacement)
            {
                tileLocation = beachMap.WorldToCell(UtilityHelper.GetMouseWorldPosition());

                if (tileToBuild != null)
                {
                    MapManager.PlaceTile(mapToBuildOn, tileLocation, tileToBuild, tilePrice);
                    
                    if (GlobalVariables.Variables.playerMoney >= tilePrice) // Checks if the player has enough money here AND in mapmanager, needs optimising
                    {   
                        GameObject costText = Instantiate(tileCostText, tileLocation, Quaternion.identity) as GameObject;
                        costText.transform.GetChild(0).GetComponent<TextMesh>().text = "" + tilePrice;
                    }
                }
                else
                {
                    MapManager.PlaceTileBase(mapToBuildOn, tileLocation, tileBaseToBuild, tilePrice);

                    if (GlobalVariables.Variables.playerMoney >= tilePrice) // Checks if the player has enough money here AND in mapmanager, needs optimising
                    {
                        GameObject costText = Instantiate(tileCostText, tileLocation, Quaternion.identity) as GameObject;
                        costText.transform.GetChild(0).GetComponent<TextMesh>().text = "" + tilePrice;
                    }
                }
                
                if (tileToBuild == farmTile)
                {
                    FarmManager.iList.Add(tileLocation);
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
            
            if (tileToBuild == groundTile)
            {
                validPlacement = isValid(beachMap, tileLocation, null, beachTile);
            }

            if ((tileToBuild == grassTile) || tileToBuild == farmTile)
            {
                validPlacement = isValid(groundMap, tileLocation, groundTile, null);
            }

            if (tileToBuild == beachTile)
            {
                validPlacement = isValid(beachMap, tileLocation, null, waterTile);
            }
        }
    }

    public bool isValid(Tilemap requiredTileMap, Vector3Int tileLocation, Tile requiredTile, TileBase requiredTileBase)
    {
        if (requiredTile != null)
        {
            if (requiredTileMap.GetTile(tileLocation) == requiredTile)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (requiredTileMap.GetTile(tileLocation) == requiredTileBase)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        
    }
}
