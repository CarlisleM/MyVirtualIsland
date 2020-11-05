using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HouseTile : Tile
{
    public GameObject house;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = house;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/House Tile")]
    public static void CreateHouseTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save House Tile", "New House Tile", "Asset", "Save House Tile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<HouseTile>(), path);
    }
#endif
}
