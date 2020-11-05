using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HouseTileScript : MonoBehaviour
{

    public int familySize = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed");
            Debug.Log(gameObject.transform.position);
            Debug.Log(GlobalVariables.Variables.tileMaps[5].WorldToCell(UtilityHelper.GetMouseWorldPosition()));
            Debug.Log(GlobalVariables.Variables.tileMaps[5].GetTile(GlobalVariables.Variables.tileMaps[5].WorldToCell(UtilityHelper.GetMouseWorldPosition())));
            if (gameObject.transform.position == GlobalVariables.Variables.tileMaps[5].WorldToCell(UtilityHelper.GetMouseWorldPosition()))
            {
                Debug.Log("House at " + gameObject.transform.position + " was clicked");
            }
        }  

        if (Input.GetMouseButtonDown(1) && (gameObject.transform.position == GlobalVariables.Variables.tileMaps[5].WorldToCell(UtilityHelper.GetMouseWorldPosition())))
        {
            Debug.Log("Increasing the family size by 1");
            familySize += 1;
        }  
    }
}
