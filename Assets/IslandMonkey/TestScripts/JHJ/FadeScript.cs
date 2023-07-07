using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public class FadeScript : MonoBehaviour
{
    private Image image;    // 검은 이미지

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        Color color = image.color;

        if (color.a < 1)
        {
            color.a += Time.deltaTime;

        }
        image.color=color;
    }
}
