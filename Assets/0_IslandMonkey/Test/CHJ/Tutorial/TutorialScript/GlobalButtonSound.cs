using UnityEngine;
using UnityEngine.UI;

public class GlobalButtonSound : MonoBehaviour
{
    public AudioClip clickSound; // Editor에서 설정할 클릭 소리
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}