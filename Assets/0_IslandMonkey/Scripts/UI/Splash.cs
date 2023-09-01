using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public float duration =1.0f;

    public void Start()
    {
        Observable.Timer(TimeSpan.FromSeconds(duration)).Subscribe(_ =>
        {
            SceneManager.LoadSceneAsync("IngameScene");
        }).AddTo(gameObject);
    }

}
