using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAIController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] villagers;

    void Start()
    {
        System.Random r = new System.Random();

        foreach (GameObject villager in villagers)
        {
            int xInt = r.Next(-14, 22); 
            int yInt = r.Next(-9, 10);

            Debug.Log(xInt);
            Debug.Log(yInt);

            villager.GetComponent<VillagerScript>().waypoint.transform.position = new Vector3(xInt+500, yInt+500, 0);
        }
    }

    void Update()
    {
        
    }
}
