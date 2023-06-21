using System;
using UniRx;
using UnityEngine;

namespace Assets.IslandMonkey.TestScripts.JHJ.Upgrade
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private Subject<Unit> _upgradeSubject = new();

    
        public int buildingLevel = 1;

        public IObservable<Unit> OnUpgradeAsObservable()
        {
        
            return _upgradeSubject;
        }

        public void BuildingUpgrade()
        {
            buildingLevel++;
            _upgradeSubject.OnNext(Unit.Default);
        }
    }
}