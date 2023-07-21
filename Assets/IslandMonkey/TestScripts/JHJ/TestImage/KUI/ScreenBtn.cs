using UnityEngine.SceneManagement;
using UnityEngine;

public class ScreenBtn : MonoBehaviour
{
    public UIManager uiManager;
    public ScreenState linkedScreenState;

    public void OnScreenButtonClick()
    {
        Debug.Log("»Æ¿Œ!");
        //uiManager.SetScreenState(linkedScreenState);
    }
}