using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HouseTileScript : MonoBehaviour
{

    public int familySize = 0;

    private List<Vector3> houseTiles = new List<Vector3>();

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                houseTiles.Add(new Vector3(gameObject.transform.position.x+i, gameObject.transform.position.y+j, 0));
            }
        }        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (houseTiles.Contains(GlobalVariables.Variables.tileMaps[5].WorldToCell(UtilityHelper.GetMouseWorldPosition())))
            {
                Debug.Log("House at " + gameObject.transform.position + " was clicked");
            }
        }  

        if (Input.GetMouseButtonDown(1) && (houseTiles.Contains(GlobalVariables.Variables.tileMaps[5].WorldToCell(UtilityHelper.GetMouseWorldPosition()))))
        {
            Debug.Log("Increasing the family size by 1");
            familySize += 1;
        }  
    }
}
