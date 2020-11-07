using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public static class MapManager
{
    // Place a tile for a certain cost at a position on the grid
    public static void PlaceTile(Tilemap map, Vector3Int pos, Tile tileType, int cost)
    {
        if (GlobalVariables.Variables.playerMoney >= cost)
        {
            map.SetTile(pos, tileType);
            GlobalVariables.Variables.playerMoney -= cost;
            GlobalVariables.Variables.playerMoneyText.GetComponent<TextMeshProUGUI>().text = "Money: " + GlobalVariables.Variables.playerMoney;
            
            // This is used for reverting if the user cancels
            // BuildingManager.objectsBuilt.Add(pos);
            // BuildingManager.mapBuiltOn.Add(map);
        }
    }

    // Place a tilebase for a certain cost at a position on the grid
    public static void PlaceTile(Tilemap map, Vector3Int pos, TileBase tileType, int cost)
    {
        if (GlobalVariables.Variables.playerMoney >= cost)
        {
            map.SetTile(pos, tileType);
            GlobalVariables.Variables.playerMoney -= cost;
            GlobalVariables.Variables.playerMoneyText.GetComponent<TextMeshProUGUI>().text = "Money: " + GlobalVariables.Variables.playerMoney;
            
            // This is used for reverting if the user cancels
            // BuildingManager.objectsBuilt.Add(pos);
            // BuildingManager.mapBuiltOn.Add(map);
        }
    }

    public static void PlaceTileLarge(Tilemap map, Vector3Int pos, TileBase[] tileType, int cost, int structureHeight, int structureWidth)
    {
        if (GlobalVariables.Variables.playerMoney >= cost)
        {
            int tileCounter = 0;

            for (int i = 0; i < structureWidth; i++)
            {
                for (int j = 0; j < structureHeight; j++)
                {
                    Debug.Log("Setting tile: " + tileType[tileCounter] + " at: " + new Vector3Int(pos.x+i, pos.y+j, 0));
                    map.SetTile(new Vector3Int(pos.x+i, pos.y+j, 0), tileType[tileCounter]); // Tile 0 has a gameobject attached that is the size of the generated structure     
                    tileCounter += 1;               
                }
            }
            
            GlobalVariables.Variables.playerMoney -= cost;
            GlobalVariables.Variables.playerMoneyText.GetComponent<TextMeshProUGUI>().text = "Money: " + GlobalVariables.Variables.playerMoney;
        }
        
        // This is used for reverting if the user cancels
        BuildingManager.objectsBuilt.Add(pos);
        BuildingManager.mapBuiltOn.Add(map);        
    }

    // Check how many tiles are placed at a position on the grid // 0 = null, 1 = ground/water, 2 = has been built on already
    public static void CountTiles(Vector3Int pos)
    {
        int tileCount = 0;

        for (int i = 0; i < GlobalVariables.Variables.tileMaps.Count; i++)
        {
            if (GlobalVariables.Variables.tileMaps[i].GetTile(pos) != null)
            {
                tileCount++;
            }
        }
    }
}