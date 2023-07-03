using System;
using TMPro;
using UniRx;
using UnityEngine;

interface IUpgradable
{
    IObservable<Unit> OnUpgradeAsObservable();
    void Upgrade();
}

namespace Assets.IslandMonkey.TestScripts.JHJ.Upgrade
{
    public class Building : MonoBehaviour, IUpgradable
    {
        private Subject<Unit> _upgradeSubject = new Subject<Unit>();
        public IObservable<Unit> OnUpgradeAsObservable() => _upgradeSubject;


        public void Upgrade()
        {
            buildingLevel++;
        }

        public int buildingLevel = 1;

        [SerializeField]
        private TextMeshProUGUI buildingLevelText;

        public int BuildingLevel
        {
            get => buildingLevel;
            private set
            {
                buildingLevel = value;
                BuildingLevelText();
            }
        }

        public void BuildingUpgrade()
        {
            BuildingLevel++;
            _upgradeSubject.OnNext(Unit.Default);
        }

        private void BuildingLevelText()
        {
            buildingLevelText.text = " " + BuildingLevel;
        }
    }
}