using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialClick : TutorialBase
{
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject clickPanel;
    public override void Enter()
    {
        clickPanel.SetActive(true);
        icon.GetComponentInChildren<Button>().interactable = true;
    }

    public override void Execute(TutorialController controller)
    {
        if (icon.activeSelf == false)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        clickPanel.SetActive(false);
    }
}
