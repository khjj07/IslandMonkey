using System;
using UniRx;
using UnityEngine;

namespace Assets.IslandMonkey.TestScripts.JHJ.Upgrade
{
    public class Monkey : MonoBehaviour
    {
        [SerializeField]
        private Subject<Unit> upgradeSubject = new Subject<Unit>();

        public int MonkeyLevel { get; set; }

        public IObservable<Unit> OnUpgradeAsObservable()
        {
            return upgradeSubject;
        }
        private void Start()
        {
            // 씬 전환이 일어났을 때 GameManager 인스턴스가 유지되도록 설정
            DontDestroyOnLoad(gameObject);
        }

        public void MonkeyUpgrade()
        {
            MonkeyLevel++;
            upgradeSubject.OnNext(Unit.Default);
        }
    }
}