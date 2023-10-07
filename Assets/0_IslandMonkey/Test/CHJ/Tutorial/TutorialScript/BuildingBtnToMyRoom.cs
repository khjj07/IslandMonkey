using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBtnToMyRoom : MonoBehaviour
{
    private TutorialUIManager tutorialUIManager;
    private TutorialNextManager tutorialNextUIManager;
    private Button button;

    private void Start()
    {
        tutorialUIManager = FindObjectOfType<TutorialUIManager>();
        tutorialNextUIManager = FindObjectOfType<TutorialNextManager>();
        button = GetComponent<Button>();

        if (button != null && tutorialUIManager != null)
        {
            button.onClick.AddListener(tutorialUIManager.PopUpMyRoom);
        }
        else if (button != null && tutorialNextUIManager != null)
        {
            button.onClick.AddListener(tutorialNextUIManager.SetUpDrawMachine);
        }
        else
        {
            Debug.LogError("TutorialUIManager or Button is missing!");
        }
    }
}
