using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TutorialMovement : TutorialBase
{
    [SerializeField] private GameObject monkey;
    [SerializeField] private GameObject endPosition;
    [SerializeField] private string animationName;
    [SerializeField] private Animator monkeyAnimator;
    [SerializeField] private GameObject icon;
    [SerializeField] private float moveSpeed;

    private bool isMoved = false;

    private void Update()
    {
        Vector3 direction = endPosition.transform.position - monkey.transform.position;
        if (isMoved)
        {
            if (direction.magnitude <= 0.1f)
            {
                isMoved = false;
            }
            monkey.transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }

    public override void Enter()
    {
        icon.SetActive(true);
        icon.GetComponent<AudioSource>().Play();

        monkeyAnimator.SetTrigger(animationName);
        monkey.GetComponent<AudioSource>().Play();
        isMoved = true;
    }

    public override void Execute(TutorialController controller)
    {
        if (!isMoved)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        monkeyAnimator.SetTrigger("Stand");
        monkey.GetComponent<AudioSource>().Stop();
    }
}