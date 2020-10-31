using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

[Serializable]
public class GameVariables
{
    [Header("Game Variables")]
    [Tooltip("Game Variables Tooltip")]
    public int playerMoney = 5000;
    public int playerFood = 0;

    [Header("Game Achievements")]
    public int housesConstructed = 0;
    public int numberOfVillagers = 0;
    
    // [Header("Audio")]
    // public AudioClip[] metalImpactSounds;
    // public AudioClip[] environmentImpactSounds;

    public List<Tilemap> tileMaps = new List<Tilemap>();
}

public class GlobalVariables : MonoBehaviour
{
    private static GlobalVariables _monoInstance;
    protected static GlobalVariables monoInstance
    {
    get
    {
        if (_monoInstance == null)
        {
            _monoInstance = GameObject.Instantiate(Resources.Load("GlobalVariables") as GameObject).GetComponent<GlobalVariables>();
            DontDestroyOnLoad(_monoInstance);
        }
        return _monoInstance;
        }
    }

    [SerializeField]
    protected GameVariables variables;
    public static GameVariables Variables
    {
        get { return monoInstance.variables; }
    }

    //Singleton
    private void Awake()
    {
        if (_monoInstance == null)
        {
            _monoInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
