using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoyageUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalShellText; // TextMeshPro 오브젝트를 할당받을 변수

    private void Start()
    {
        UpdateTotalShellText();
    }
    public void OnClickGoHomeBtn()
    {
        SceneManager.LoadScene("JHJ");
        
    }

    private void UpdateTotalShellText()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                if (_totalShellText != null)

                {
                    _totalShellText.text = " " + GameManager._totalShell;
                }

            });



    }
}
