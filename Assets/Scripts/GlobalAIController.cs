using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAIController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] villagers;

    [SerializeField]
    private GameObject[] campFire;

    private bool findFire = false;

    void Start()
    {
        System.Random r = new System.Random();

        foreach (GameObject villager in villagers)
        {
            int xInt = r.Next(-14, 22); 
            int yInt = r.Next(-9, 10);

            villager.GetComponent<VillagerScript>().waypoint.transform.position = new Vector3(xInt+500, yInt+500, 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            findFire = true;

            foreach (GameObject villager in villagers)
            {
                Transform [] t = villager.GetComponentsInChildren<Transform>();
                Transform villagerLocation = t[1];

                float closestCampFireDist = 1000000;
                int closestCampFire = 0;

                for (int i = 0; i < campFire.Length; i++)
                {  
                    float dist = Vector3.Distance(campFire[i].transform.position, villagerLocation.transform.position);

                    if (dist < closestCampFireDist)
                    {
                       closestCampFireDist = dist;
                       closestCampFire = i;
                    }
                }

                villager.GetComponent<VillagerScript>().waypoint.transform.position = campFire[closestCampFire].transform.position;

                // villager.GetComponent<VillagerScript>().waypoint.transform.position = new Vector3(500, 500, 0);
            }
        }

        if (findFire)
        {
            foreach (GameObject villager in villagers)
            {
                Transform [] t = villager.GetComponentsInChildren<Transform>();
                Transform villagerLocation = t[1];

                for (int i = 0; i < campFire.Length; i++)
                {  
                    if ((Vector3.Distance(campFire[i].transform.position, villagerLocation.transform.position) < 3))
                    {
                        villager.GetComponent<VillagerScript>().waypoint.transform.position = villagerLocation.transform.position;
                    }
                }
            }
        }
        
    }
}
