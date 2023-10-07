using UnityEngine;

[RequireComponent(typeof(DialogSystem))]
public class TutorialDialog : TutorialBase
{
    [SerializeField] private GameObject dialogArea;
    // ĳ���͵��� ��縦 �����ϴ� DialogSystem
    private DialogSystem dialogSystem;

    public override void Enter()
    {
        dialogArea.SetActive(true);
        dialogSystem = GetComponent<DialogSystem>();
        dialogSystem.Setup();
    }

    public override void Execute(TutorialController controller)
    {
        // ���� �б⿡ ����Ǵ� ��� ����
        bool isCompleted = dialogSystem.UpdateDialog();

        // ���� �б��� ��� ������ �Ϸ�Ǹ�
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