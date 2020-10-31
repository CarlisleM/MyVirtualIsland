using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmManager : MonoBehaviour
{

    public int delayAmount = 1;
    protected float timer;

    public Tile farmTile;

    public TileBase groundTile;

    [SerializeField]
    private Tilemap groundMap;

    [SerializeField]
    private Tilemap farmMap;

    [SerializeField]
    private int maxFarmIncome;

    [SerializeField]
    private Color maxFarmColor, minFarmColor, clearFarm;

    private Dictionary<Vector3Int, float> farmTiles = new Dictionary<Vector3Int, float>();

    [SerializeField]
    private float testAddFarmAmount;

    public static List<Vector3Int> iList = new List<Vector3Int>();

    public void Awake()
    {
        iList.Add(new Vector3Int(492, 505, 0));
        iList.Add(new Vector3Int(493, 505, 0));
        iList.Add(new Vector3Int(494, 505, 0));
    }

    public void AddIncome(Vector3Int worldPosition, float incomeAmount)
    {
        // Vector3Int gridPosition = farmMap.WorldToCell(UtilsClass.GetMouseWorldPosition());
        ChangeFarmValue(worldPosition, incomeAmount);
        VisualiseIncome();
    }

    private void ChangeFarmValue(Vector3Int gridPosition, float changeBy)
    {
        if (!farmTiles.ContainsKey(gridPosition))
        {
            farmTiles.Add(gridPosition, 0f);
        }

        float newValue = farmTiles[gridPosition] + changeBy;

        if (newValue <= 0f)
        {
            farmTiles.Remove(gridPosition);

            farmMap.SetTileFlags(gridPosition, TileFlags.None);
            farmMap.SetColor(gridPosition, clearFarm);
            farmMap.SetTileFlags(gridPosition, TileFlags.LockColor);
        }
        else
        {
            farmTiles[gridPosition] = Mathf.Clamp(newValue, 0f, maxFarmIncome);
        }

    }

    private void VisualiseIncome()
    {
        foreach (var farm in farmTiles)
        {
            float farmPercent = farm.Value / maxFarmIncome;

            Color newTileColor = maxFarmColor * farmPercent + minFarmColor * (1f - farmPercent);

            farmMap.SetTileFlags(farm.Key, TileFlags.None);
            farmMap.SetColor(farm.Key, newTileColor);
            farmMap.SetTileFlags(farm.Key, TileFlags.LockColor);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayAmount)
        {
            timer = 0f;
            for (int i = 0; i < iList.Count; i++)
            {
                AddIncome(iList[i], testAddFarmAmount);
            }   
        }        

        if (!BuildingManager.currentlyBuilding)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int gridPosition = farmMap.WorldToCell(UtilityHelper.GetMouseWorldPosition());
                
                if (farmMap.GetTile(gridPosition) == farmTile)
                {
                    CollectIncome(gridPosition);
                }
            } 
        }

        // if (Input.GetMouseButtonDown(1))
        // {
        //     Vector3Int gridPosition = farmMap.WorldToCell(UtilityHelper.GetMouseWorldPosition());

        //     if (groundMap.GetTile(gridPosition) == groundTile) // Check if it is a ground tile where the farm can be placed
        //     {
        //         MapManager.PlaceTile(farmMap, gridPosition, farmTile, 100);
        //         iList.Add(gridPosition);
        //     }
        // }   
    }

    public void CollectIncome(Vector3Int gridPosition)
    {
        GlobalVariables.Variables.playerMoney += (int)farmTiles[gridPosition];
        farmTiles[gridPosition] = 0;
    }

}
