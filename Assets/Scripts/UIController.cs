using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool showMenu = true;

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

    // public void ArrangeIsland()
    // {
    //     // Change what the controls do
    // }

}
