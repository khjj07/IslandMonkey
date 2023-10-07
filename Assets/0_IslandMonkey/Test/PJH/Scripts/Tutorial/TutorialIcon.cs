using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIcon : TutorialBase
{
    [SerializeField] private GameObject icon;
    private bool isCompleted = false;

    public override void Enter()
    {
        icon.SetActive(true);
        StartCoroutine(nameof(Stay));
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
    }

    private IEnumerator Stay()
    {
        yield return new WaitForSeconds(3.0f);

        isCompleted = true;
    }
}
