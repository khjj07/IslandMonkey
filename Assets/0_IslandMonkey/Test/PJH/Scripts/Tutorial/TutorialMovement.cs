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
    [SerializeField] private float moveSpeed = 1.0f;

    private bool isMoved = false;

    private void Update()
    {
        Vector3 direction = endPosition.transform.position - monkey.transform.position;
        Debug.Log(direction);
        if (isMoved)
        {
            if (direction.magnitude <= 0.1f)
            {
                isMoved = false;
            }
            monkey.transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            //monkey.transform.rotation = Quaternion.Lerp(monkey.transform.rotation, Quaternion.LookRotation(direction - new Vector3(0,0,180)), Time.deltaTime * moveSpeed);
        }
    }

    public override void Enter()
    {
        isMoved = true;
        monkeyAnimator.SetTrigger(animationName);
        icon.SetActive(true);
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
    }
}