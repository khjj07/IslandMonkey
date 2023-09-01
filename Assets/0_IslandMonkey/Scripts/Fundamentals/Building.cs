using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

// �������̽��� �ִ� �ڸ� �������̽��� ������ �и�����
// ������

namespace Assets._0_IslandMonkey.Scripts.Abstract
{

    public class Building : MonoBehaviour
    {
        #region ����

        public int goldIncome = 100;
        public int bananaIncome = 100;
        public int clamIncome = 100;
        public float goldEarnInterval = 1.0f;
        public float bananaEarnInterval = 1.0f;
        public float clamEarnInterval = 1.0f;
        public UnityEvent goldEarnEvent;
        public UnityEvent bananaEarnEvent;
        public UnityEvent clamEarnEvent;

        #endregion


        public void CreateEarnStream()
        {
            if (goldIncome > 0)
            {
                Observable.Interval(TimeSpan.FromSeconds(goldEarnInterval)).Subscribe(_ =>
                {
                    GameManager.instance.EarnGold(goldIncome);
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