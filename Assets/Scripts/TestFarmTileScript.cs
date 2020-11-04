using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFarmTileScript : MonoBehaviour
{
    public int delayAmount = 1;
    protected float timer;

    public int farmDollarydoos = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayAmount)
        {
            timer = 0f;
            farmDollarydoos += 1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (new Vector3(Mathf.Floor(gameObject.transform.position.x), Mathf.Floor(gameObject.transform.position.y), 0) == GlobalVariables.Variables.tileMaps[0].WorldToCell(UtilityHelper.GetMouseWorldPosition()))
            {
                Debug.Log(gameObject.transform.position);
                Debug.Log("Collected: " + farmDollarydoos);
                farmDollarydoos = 0;
            }
        }   
    }
}
