/*using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayLoginSample : MonoBehaviour
{
    public void Awake()
    {
        // 초기화 함수, 인스턴스를 만드는 역할
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        // 디버그용 변수
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

    }

    public void Do_Login()
    {
        if (!Social.localUser.authenticated) //현재 기기와 연결된 계정이 인증이 아직 안 되었는지 체크
        {
            //계정 인증
            Social.localUser.Authenticate((bool isSuccess) =>
                {
                    if (isSuccess)
                    {
                        Debug.Log("인증 성공 ->" + Social.localUser.userName);
                    }
                    else
                    {
                        Debug.Log("인증 실패");
                    }
                }
            );
        }
        
    }
    public void Do_Logout()
    {
        // 로그아웃
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
}*/
