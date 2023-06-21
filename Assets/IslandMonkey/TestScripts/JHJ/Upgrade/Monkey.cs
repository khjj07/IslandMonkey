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

        public void MonkeyUpgrade()
        {
            MonkeyLevel++;
            upgradeSubject.OnNext(Unit.Default);
        }
    }
}