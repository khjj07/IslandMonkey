using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour
{
    public float splashDuration = 3f; // ���÷��� ȭ�� ǥ�� �ð� (��)

    private float timer;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= splashDuration)
        {
            //LoadVoyageScene();
        }
    }
}
