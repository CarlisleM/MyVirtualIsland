using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MapManager
{
        // Place a tile for a certain cost at a position on the grid
    public static void PlaceTile(Tilemap map, Vector3Int pos, Tile tileType, int cost)
    {
        if (GlobalVariables.Variables.playerMoney > cost)
        {
            map.SetTile(pos, tileType);
            GlobalVariables.Variables.playerMoney -= cost;
            BuildingManager.objectsBuilt.Add(pos);
            BuildingManager.mapBuiltOn.Add(map);
        }
    }

    // Place a tilebase for a certain cost at a position on the grid
    public static void PlaceTileBase(Tilemap map, Vector3Int pos, TileBase tileType, int cost)
    {
        if (GlobalVariables.Variables.playerMoney > cost)
        {
            map.SetTile(pos, tileType);
            GlobalVariables.Variables.playerMoney -= cost;
            BuildingManager.objectsBuilt.Add(pos);
            BuildingManager.mapBuiltOn.Add(map);
        }
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