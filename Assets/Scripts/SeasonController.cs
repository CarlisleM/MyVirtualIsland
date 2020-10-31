using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SeasonController : MonoBehaviour
{
    // 0 Spring
    // 1 Summer
    // 2 Fall/Autumn
    // 3 Winter

    [SerializeField]
    private Sprite[] seasonIcons;
    
    [SerializeField]
    private Image seasonIcon;

    System.DateTime moment = DateTime.Now;

    int daysInCurrentMonth;
    float seasonLength;

    [SerializeField] private int numberOfSeasons = 4;

    public void ChangeSeason(Sprite icon)
    {
        seasonIcon.GetComponent<Image>().sprite = icon;
    }

    void Start()
    {
        CheckSeason();
        DaytNightCycle();
    }

    private void DaytNightCycle ()
    {
        if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 6)
        {
            Debug.Log("Time: " + DateTime.Now.Hour + " Cycle: Night time");
        }
        else
        {
            Debug.Log("Time: " + DateTime.Now.Hour + " Cycle: Day time");
        }
    }

    private void CheckSeason()
    {
        DateTime moment = DateTime.Now;

        daysInCurrentMonth = DateTime.DaysInMonth(moment.Year, moment.Month); // Days in the month 
        seasonLength = (float)daysInCurrentMonth/numberOfSeasons; // Length of each season in decimal form

        int seasonDays = (int)Mathf.Floor(seasonLength); // Number of days in each season
        float seasonPartialDay = seasonLength - Mathf.Floor(seasonLength); // Number of hours and minutes in decimal form
        float seasonHours = 24 * seasonPartialDay; // Number of hours
        float seasonMinutes = Mathf.RoundToInt(60 * (seasonHours - Mathf.Floor(seasonHours))); // Number of minutes

        List<DateTime> seasonChangeDays = new List<DateTime>();

        // First day of the month
        DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
        seasonChangeDays.Add(time);

        for (int i = 0; i < numberOfSeasons; i++)
        {
            DateTime newTime = seasonChangeDays[i].AddMinutes(seasonMinutes);
            newTime = newTime.AddHours(Mathf.Floor(seasonHours));
            newTime = newTime.AddDays(seasonDays);

            seasonChangeDays.Add(newTime);
        }

        // for (int i = 0; i < numberOfSeasons; i++)
        // {
        //     Debug.Log(seasonChangeDays[i]);
        // }

        int currentSeason = 0;

        for (int i = 0; i < numberOfSeasons-1; i++)
        {
            if ((DateTime.Now > seasonChangeDays[i]) && (DateTime.Now < seasonChangeDays[i+1]))
            {
                currentSeason = i;
            }
        }

        switch(currentSeason)
        {
            case 0:
            {
                // Spring
                Debug.Log("Spring");
                ChangeSeason(seasonIcons[0]);
                break;
            }
            case 1:
            {
                // Summer
                Debug.Log("Summer");
                ChangeSeason(seasonIcons[1]);
                break;
            }
            case 2:
            {
                // Autumn (Fall)
                Debug.Log("Autumn");
                ChangeSeason(seasonIcons[2]);
                break;
            }
            case 3:
            {
                // Winter
                Debug.Log("Winter");
                ChangeSeason(seasonIcons[3]);
                break;
            }
            case 4:
            {
                // 5th Season
                break;
            }
        }

    }


}
