/*using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayLoginSample : MonoBehaviour
{
    public void Awake()
    {
        // �ʱ�ȭ �Լ�, �ν��Ͻ��� ����� ����
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        // ����׿� ����
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

    }

    public void Do_Login()
    {
        if (!Social.localUser.authenticated) //���� ���� ����� ������ ������ ���� �� �Ǿ����� üũ
        {
            //���� ����
            Social.localUser.Authenticate((bool isSuccess) =>
                {
                    if (isSuccess)
                    {
                        Debug.Log("���� ���� ->" + Social.localUser.userName);
                    }
                    else
                    {
                        Debug.Log("���� ����");
                    }
                }
            );
        }
        
    }
    public void Do_Logout()
    {
        // �α׾ƿ�
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
}*/
