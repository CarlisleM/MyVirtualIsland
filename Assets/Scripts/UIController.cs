using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] panelList;
    
    [SerializeField]
    private GameObject UIButtons;

    [SerializeField]
    private GameObject seasonDataPanel;

    [SerializeField]
    private GameObject toggleMenuButton;

    [SerializeField]
    private TextMeshProUGUI playerMoney;

    [SerializeField]
    private TextMeshProUGUI playerFood;

    private bool showMenu = true;

    private void Start()
    {
        GlobalVariables.Variables.playerMoneyText.GetComponent<TextMeshProUGUI>().text = "Money: " + GlobalVariables.Variables.playerMoney;
        playerFood.GetComponent<TextMeshProUGUI>().text = "Food: " + GlobalVariables.Variables.playerFood;
    }

    // Set selected panel to active and disable all others. Disable selected panel if it is already active.
    public void SetPanelActive(GameObject currentPanel)
    {
        if (currentPanel.activeSelf == true)
        {
            currentPanel.SetActive(false);
        }
        else
        {
            foreach (GameObject panel in panelList)
            {
                if (panel == currentPanel)
                {
                    panel.SetActive(true);
                }
                else
                {
                    panel.SetActive(false);
                }
            }
        }
    }

    public void ToggleMenuBar()
    {
        if (showMenu)
        {
            LeanTween.move(UIButtons.GetComponent<RectTransform>(), new Vector3(0, -115f, 0f), 0.25f);
            LeanTween.move(seasonDataPanel.GetComponent<RectTransform>(), new Vector3(0, -115f, 0f), 0.25f);
            LeanTween.move(toggleMenuButton.GetComponent<RectTransform>(), new Vector3(0, 0, 0f), 0.25f);            
        }
        else
        {
            LeanTween.move(UIButtons.GetComponent<RectTransform>(), new Vector3(0, 0, 0f), 0.25f);
            LeanTween.move(seasonDataPanel.GetComponent<RectTransform>(), new Vector3(0, 0, 0f), 0.25f);
            LeanTween.move(toggleMenuButton.GetComponent<RectTransform>(), new Vector3(0, 110, 0f), 0.25f);
        }

        showMenu = !showMenu;
    }

    public void TogglePanel(GameObject go)
    {
        if (go.activeSelf)
        {
            go.SetActive(false);
        }
        else
        {
            go.SetActive(true);
        }
    }

    // void FixedUpdate()
    // {
    //     if (GlobalVariables.Variables.currentlySelectedVillager != null)
    //     {
    //         Debug.Log(GlobalVariables.Variables.currentlySelectedVillager.GetComponent<Villager>().villagerTask);
    //     }
    // }

    // public void ArrangeIsland()
    // {
    //     // Change what the controls do
    // }   
}
