using System;
using Assets._0_IslandMonkey.Scripts.Extension;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

// 인터페이스가 있던 자리 인터페이스도 파일을 분리해줘
// 오케이

namespace Assets._0_IslandMonkey.Scripts.Abstract
{

    public class Building : MonoBehaviour
    {
        #region 수입

        public int goldIncome = 100;
        public int bananaIncome = 100;
        public int clamIncome = 100;
        public float goldEarnInterval = 1.0f;
        public float bananaEarnInterval = 1.0f;
        public float clamEarnInterval = 1.0f;
        public GameObject model;
        public TextMeshPro goldEarningLabel;
        public TextMeshPro bananaEarningLabel;
        public TextMeshPro clamEarningLabel;

        public UnityEvent goldEarnEvent;
        public UnityEvent bananaEarnEvent;
        public UnityEvent clamEarnEvent;

        #endregion

        public void Start()
        {
            CreateEarnStream();
        }
        public void CreateEarnStream()
        {
            if (goldIncome > 0)
            {
                Observable.Interval(TimeSpan.FromSeconds(goldEarnInterval)).Subscribe(_ =>
                {
                    GameManager.instance.EarnGold(goldIncome);
                    AnimateGoldEarningText(goldIncome.FormatLargeNumber());
                    goldEarnEvent.Invoke();
                }).AddTo(gameObject);
            }
            if (bananaIncome > 0)
            {
                Observable.Interval(TimeSpan.FromSeconds(bananaEarnInterval)).Subscribe(_ =>
                {
                    GameManager.instance.EarnBanana(bananaIncome);
                    bananaEarnEvent.Invoke();
                }).AddTo(gameObject);
            }
            if (clamIncome > 0)
            {
                Observable.Interval(TimeSpan.FromSeconds(clamEarnInterval)).Subscribe(_ =>
                {
                    GameManager.instance.EarnClam(clamIncome);
                    clamEarnEvent.Invoke();
                }).AddTo(gameObject);
            }
        }

        public void SqueezeModel()
        {
            model.transform.DOShakeScale(0.2f, Vector3.one*0.02f);
        }

        public void AnimateGoldEarningText(string content)
        {
            var instance = Instantiate(goldEarningLabel);
            instance.SetText("+"+content+"골드");
            instance.transform.localScale=Vector3.zero;
            
            instance.transform.DOScale(1, 1)
                .SetRelative();

            instance.transform.DOMoveY(1.5f, 1)
                .SetRelative()
                .OnComplete(()=> instance.DOColor(new Vector4(instance.color.r, instance.color.g, instance.color.b, 0f), 0.5f)
                    .OnComplete(()=>Destroy(instance.gameObject)));
        }

        public void SetPlace(Place place)
        {
            place = place;
            Vector3 newPosition = transform.position;
            newPosition.x = place.transform.position.x;
            newPosition.z = place.transform.position.z;
            transform.position = newPosition;
        }
    }
}