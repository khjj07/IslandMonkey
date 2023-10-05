using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBtnToMyRoom : MonoBehaviour
{
    private TutorialUIManager tutorialUIManager;
    private Button button;

    private void Start()
    {
        tutorialUIManager = FindObjectOfType<TutorialUIManager>();
        button = GetComponent<Button>();

        if (button != null && tutorialUIManager != null)
        {
            button.onClick.AddListener(tutorialUIManager.PopUpMyRoom);
        }
        else
        {
            Debug.LogError("TutorialUIManager or Button is missing!");
        }
    }
}
