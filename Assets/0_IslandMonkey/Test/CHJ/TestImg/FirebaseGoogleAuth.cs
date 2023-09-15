using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;
using System.Net;

public class FirebaseGoogleAuth : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestEmail()
            .Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        // ���� �÷��� ���� Ȱ��ȭ

        auth = FirebaseAuth.DefaultInstance; // Firebase �׼���
    }


    public void TryGoogleLogin()
    {
        if (!Social.localUser.authenticated) // �α��� �Ǿ� ���� �ʴٸ�
        {
            Social.localUser.Authenticate(success => // �α��� �õ�
            {
                if (success) // �����ϸ�
                {
                    Debug.Log("Success");
                    StartCoroutine(TryFirebaseLogin()); // Firebase Login �õ�
                }
                else // �����ϸ�
                {
                    Debug.Log("Fail");
                }
            });
        }
    }


    public void TryGoogleLogout()
    {
        if (Social.localUser.authenticated) // �α��� �Ǿ� �ִٸ�
        {
            PlayGamesPlatform.Instance.SignOut(); // Google �α׾ƿ�
            auth.SignOut(); // Firebase �α׾ƿ�
        }
    }


    IEnumerator TryFirebaseLogin()
    {
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
            yield return null;
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();


        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Success!");
        });
    }
}