using System;
using TMPro;
using UniRx;
using UnityEngine;

// 인터페이스가 있던 자리 인터페이스도 파일을 분리해줘
// 오케이

namespace Assets._0_IslandMonkey.CHJ.Scripts.Upgrade
{

    public class Building : Singleton<Building>
    {
        private Subject<Unit> _upgradeSubject = new Subject<Unit>();
        public IObservable<Unit> OnUpgradeAsObservable() => _upgradeSubject;

        public int buildingLevel = 1;

        [SerializeField]
        private TextMeshProUGUI buildingLevelText;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            // 씬 전환이 일어났을 때 GameManager 인스턴스가 유지되도록 설정

                if (buildingLevelText != null)

                {
                    buildingLevelText.text = " " + buildingLevel;
                }
                
        }


        public void BuildingUpgrade()
        {
            buildingLevel++;
            GameManager._totalGold -= 100;
            Debug.Log("레벨업");
            //_upgradeSubject.OnNext(Unit.Default);
        }

    }
}