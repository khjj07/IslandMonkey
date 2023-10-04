using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Speaker { FireMonkey = 0, BuilbuildingOwner = 1 }

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Dialog[] dialogs;                       // ���� �б��� ��� ���
    [SerializeField]
    private GameObject speakerImage;                   // ��ȭâ Image UI
    [SerializeField]
    private TextMeshProUGUI textNames;                        // ���� ������� ĳ���� �̸� ��� Text UI
    [SerializeField]
    private TextMeshProUGUI textDialogues;                    // ���� ��� ��� Text UI
    [SerializeField]
    private GameObject objectArrows;                  // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� ������Ʈ
    [SerializeField]
    private float typingSpeed;                  // �ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
    [SerializeField]
    private KeyCode keyCodeSkip = KeyCode.Space;    // Ÿ���� ȿ���� ��ŵ�ϴ� Ű

    private int currentIndex = -1;
    private bool isTypingEffect = false;            // �ؽ�Ʈ Ÿ���� ȿ���� ���������
    private Speaker currentSpeaker = Speaker.FireMonkey;

    public void Setup()
    {
        for (int i = 0; i < 2; ++i)
        {
            // ��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
            InActiveObjects(i);
        }

        SetNextDialog();
    }

    public bool UpdateDialog()
    {
        if (Input.GetKeyDown(keyCodeSkip) || Input.GetMouseButtonDown(0))
        {
            // �ؽ�Ʈ Ÿ���� ȿ���� ������϶� ���콺 ���� Ŭ���ϸ� Ÿ���� ȿ�� ����
            if (isTypingEffect == true)
            {
                // Ÿ���� ȿ���� �����ϰ�, ���� ��� ��ü�� ����Ѵ�
                StopCoroutine("TypingText");
                isTypingEffect = false;
                textDialogues.text = dialogs[currentIndex].dialogue;
                // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
                objectArrows.SetActive(true);

                return false;
            }

            // ���� ��� ����
            if (dialogs.Length > currentIndex + 1)
            {
                SetNextDialog();
            }
            // ��簡 �� �̻� ���� ��� true ��ȯ
            else
            {
                // ��� ĳ���� �̹����� ��Ӱ� ����
                for (int i = 0; i < 2; ++i)
                {
                    // ��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
                    InActiveObjects(i);
                }

                return true;
            }
        }

        return false;
    }

    private void SetNextDialog()
    {
        // ���� ȭ���� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
        InActiveObjects((int)currentSpeaker);

        currentIndex++;

        // ���� ȭ�� ����
        currentSpeaker = dialogs[currentIndex].speaker;
        speakerImage.gameObject.SetActive(true);

        // ���� ȭ�� �̸� �ؽ�Ʈ Ȱ��ȭ �� ����
        textNames.gameObject.SetActive(true);
        textNames.text = GetSpeakerName(dialogs[currentIndex].speaker);

        // ȭ���� ��� �ؽ�Ʈ Ȱ��ȭ �� ���� (Typing Effect)
        textDialogues.gameObject.SetActive(true);
        StartCoroutine(nameof(TypingText));
    }

    private string GetSpeakerName(Speaker speaker)
    {
        string name = "";
        switch (speaker)
        {
            case Speaker.BuilbuildingOwner:
                name = "������";
                break;
            case Speaker.FireMonkey:
                name = "�ҹ��";
                break;
        }

        return name;
    }

    private void InActiveObjects(int index)
    {
        speakerImage.gameObject.SetActive(false);
        textNames.gameObject.SetActive(false);
        textDialogues.gameObject.SetActive(false);
        objectArrows.SetActive(false);
    }

    private IEnumerator TypingText()
    {
        int index = 0;

        isTypingEffect = true;

        // �ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
        while (index < dialogs[currentIndex].dialogue.Length)
        {
            textDialogues.text = dialogs[currentIndex].dialogue.Substring(0, index);

            index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;

        // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
        objectArrows.SetActive(true);
    }
}

[System.Serializable]
public struct Dialog
{
    public Speaker speaker; // ȭ��
    [TextArea(3, 5)]
    public string dialogue;	// ���
}

