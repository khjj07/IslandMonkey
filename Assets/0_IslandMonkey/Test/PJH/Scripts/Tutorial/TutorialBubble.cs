using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBubble : TutorialBase
{
    [SerializeField] private GameObject bubble;
    private bool isCompleted = false;

    public override void Enter()
    {
        bubble.SetActive(true);
        bubble.GetComponent<Animator>().SetTrigger("Bubble");
        bubble.GetComponent<AudioSource>().Play();
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
