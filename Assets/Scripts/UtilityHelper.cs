using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class UtilityHelper
{

    // Taken from CodeMonkey.Utils;
    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ() {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static Vector3 SnapToGrid(Vector3 pos)
    {
        float x = Mathf.FloorToInt(pos.x);
        float y = Mathf.FloorToInt(pos.y);
        float z = 0;

        return new Vector3(x, y, z);
    }

}
