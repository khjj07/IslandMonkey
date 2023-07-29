using UnityEngine.SceneManagement;
using UnityEngine;

public class ScreenBtn : MonoBehaviour
{
    public UIManager uiManager;
    //public ScreenState linkedScreenState;

    public void OnScreenButtonClick()
    {
        Debug.Log("확인!");
        //uiManager.SetScreenState(linkedScreenState);
    }
}
//이것도 난해해..ㅠㅠ..