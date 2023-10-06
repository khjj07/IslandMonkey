using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClick : TutorialBase
{
    [SerializeField] private GameObject icon;
    public override void Enter()
    {
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
    }
}
