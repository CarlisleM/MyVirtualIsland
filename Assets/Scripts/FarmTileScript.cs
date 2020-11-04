using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmTileScript : MonoBehaviour
{
    public int increaseIncomeTimer = 2;
    protected float timer;

    public int farmIncome = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= increaseIncomeTimer)
        {
            timer = 0f;
            farmIncome += 1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.transform.position == UtilityHelper.SnapToGrid(GlobalVariables.Variables.tileMaps[0].WorldToCell(UtilityHelper.GetMouseWorldPosition())))
            {
                Debug.Log("Collected: " + farmIncome);
                GlobalVariables.Variables.playerMoney += farmIncome;
                GlobalVariables.Variables.playerMoneyText.text = "Money: " + GlobalVariables.Variables.playerMoney; // Needs better system to update whenever money amount changes
                farmIncome = 0;
            }
        }   
    }
}
