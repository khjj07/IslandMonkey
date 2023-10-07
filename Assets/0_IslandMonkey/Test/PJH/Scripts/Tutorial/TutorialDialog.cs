using UnityEngine;

[RequireComponent(typeof(DialogSystem))]
public class TutorialDialog : TutorialBase
{
    [SerializeField] private GameObject dialogArea;
    // 캐릭터들의 대사를 진행하는 DialogSystem
    private DialogSystem dialogSystem;

    public override void Enter()
    {
        dialogArea.SetActive(true);
        dialogSystem = GetComponent<DialogSystem>();
        dialogSystem.Setup();
    }

    public override void Execute(TutorialController controller)
    {
        // 현재 분기에 진행되는 대사 진행
        bool isCompleted = dialogSystem.UpdateDialog();

        // 현재 분기의 대사 진행이 완료되면
        if (isCompleted == true)
        {
            dialogArea.SetActive(false);
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
    }
}