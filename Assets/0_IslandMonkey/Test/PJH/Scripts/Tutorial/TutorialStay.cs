using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStay : TutorialBase
{
    [SerializeField] private GameObject fire;
    [SerializeField] private string animationName;
    [SerializeField] private Animator monkeyAnimator;
    [SerializeField] private Animator buildingAnimator;
    private bool isCompleted = false;

    public override void Enter()
    {
        monkeyAnimator.SetTrigger(animationName);
        buildingAnimator.SetTrigger(animationName);
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
        if (fire != null)
        {
            fire.SetActive(true);
        }
        yield return new WaitForSeconds(5.0f);

        isCompleted = true;
    }
}
