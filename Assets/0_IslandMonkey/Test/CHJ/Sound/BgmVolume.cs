using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmVolume : MonoBehaviour
{
    public AudioSource audioSource;  // ����� �ҽ�
    public Slider volumeSlider;     // ������ ������ �����̴�

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ���� ����
        volumeSlider.value = audioSource.volume;

        // �����̴� �̺�Ʈ �߰�
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    // �����̴� ���� ����� �� ȣ��Ǵ� �Լ�
    public void ChangeVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }
}
