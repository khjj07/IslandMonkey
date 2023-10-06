using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStoryScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(2f));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("TutorialStoryScene");
    }
}
