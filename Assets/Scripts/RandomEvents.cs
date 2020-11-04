using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomEvents : MonoBehaviour
{
    [SerializeField]
    private TileBase beachTile;

    private List<Vector3Int> beachTileLocations = new List<Vector3Int>();
    private List<Vector3Int> shoreTileLocations = new List<Vector3Int>();

    public GameObject playerNotification;

    public GameObject[] userPanelList;

    public List<bool> activePanelList = new List<bool>();

    public GameObject spawnObject;

    private void Start () {        
        StartRandomEvents();
    } 

    private void Update()
    {
        // if (Input.GetMouseButtonDown(1))
        // {
        //     NotifyPlayer();
        // }
    }

    public void StartRandomEvents()
    {
        StartCoroutine("RandomlyGenerateEvent");
    }

    IEnumerator RandomlyGenerateEvent()
    {
        LocateBeachTiles();

        float spawnTime  = Random.Range(60, 120); // Random time between two given times
        int spawnLocation = Random.Range(0, beachTileLocations.Count);

        yield return new WaitForSeconds(spawnTime);
        Debug.Log("Waited " + spawnTime + " and spawned at " + beachTileLocations[spawnLocation]);
        // GlobalVariables.Variables.tileMaps[6].SetTile()
        // This should be set tile not instantiate.
        // May have to be an object so that it can move around on tiles
        Instantiate(spawnObject, new Vector3(beachTileLocations[spawnLocation].x+0.5f, beachTileLocations[spawnLocation].y+0.5f, 0), Quaternion.identity);
        // Instantiate(spawnObject, new Vector3()beachTileLocations[spawnLocation], Quaternion.identity);
        StartCoroutine("RandomlyGenerateEvent");    
    }

    public void LocateBeachTiles()
    {
        Tilemap tilemap = GlobalVariables.Variables.tileMaps[0];

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == beachTile) {
                    beachTileLocations.Add(new Vector3Int(x, y, 0));
                }
            }
        }        

        // beachTileLocations gives us all beach tiles without other tiles on top of them. Used for random spawn locations
        for (int i = 0; i < beachTileLocations.Count; i++)
        {
            for (int j = 1; j < GlobalVariables.Variables.tileMaps.Count; j++)
            {
                if (GlobalVariables.Variables.tileMaps[j].GetTile(beachTileLocations[i]) != null)
                {
                    beachTileLocations.Remove(beachTileLocations[i]);
                }
            }
        }

        // shoreTileLocations gives us all uncovered beach tiles at the waters edge. Used for crab spawn locations and items washing ashore.
        for (int i = 0; i < beachTileLocations.Count; i++)
        {
            if (
                    (GlobalVariables.Variables.tileMaps[0].GetTile(new Vector3Int(beachTileLocations[i][0]+1, beachTileLocations[i][1], 0)) != beachTile) ||
                    (GlobalVariables.Variables.tileMaps[0].GetTile(new Vector3Int(beachTileLocations[i][0]-1, beachTileLocations[i][1], 0)) != beachTile) ||
                    (GlobalVariables.Variables.tileMaps[0].GetTile(new Vector3Int(beachTileLocations[i][0], beachTileLocations[i][1]+1, 0)) != beachTile) ||
                    (GlobalVariables.Variables.tileMaps[0].GetTile(new Vector3Int(beachTileLocations[i][0], beachTileLocations[i][1]-1, 0)) != beachTile)
                )
            {
                shoreTileLocations.Add(beachTileLocations[i]);      // Shore tiles do not include beach tiles
                beachTileLocations.Remove(beachTileLocations[i]);   // Beach tiles do not include shore tiles
            }
        }
    }

    // Locations are not accurate, need to +0.5 on both axis

    public void NotifyPlayer()
    {
        playerNotification.SetActive(true);

        // Blue effect on screen
        // Disable user controls

        foreach (GameObject panel in userPanelList)
        {
            activePanelList.Add(panel.activeSelf);
        }

        foreach (GameObject panel in userPanelList)
        {
            panel.SetActive(false);
        }
    }

    public void NotifyPlayerAccept()
    {
        playerNotification.SetActive(false);

        for (int i = 0; i < userPanelList.Length; i++)
        {
            if (activePanelList[i] == true)
            {
                userPanelList[i].SetActive(true);
            }
        }

        activePanelList.Clear();

        // Enable user controls
    }

}
