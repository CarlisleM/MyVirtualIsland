﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestFarmScript : Tile
{
    
    public GameObject farm;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = farm;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/TestFarmScript")]
    public static void CreateTestFarmTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Farm Tile", "New Farm Tile", "Asset", "Save Farm Tile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TestFarmScript>(), path);
    }
#endif
}
