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

        public bool IsInMonkey { get { return _isInMonkey; } set { _isInMonkey = value; } }
        private bool _isInMonkey = false;

        // Building에 도착했을때 원숭이의 animator로가 아래의 animator로 바뀜(새로운 건물 도착할 때마다 다른 애니메이션 필요하기 때문)
        public RuntimeAnimatorController MonkeyWithBuildingAnimatorController;

        [SerializeField]
        private TextMeshProUGUI buildingLevelText;
        [SerializeField]
        private TextMeshProUGUI buildingNextLevelText;

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
                    int _nextBuildingLevel = buildingLevel + 1;
                    buildingNextLevelText.text = " " + _nextBuildingLevel;
            }
                
        }


        public void changeIsInMonkey()
        {
            if (_isInMonkey)
            {
                _isInMonkey = true;
            }
            else
            {
                _isInMonkey = false;
            }
        }





        public void BuildingUpgrade()
        {
            buildingLevel++;
            GameManager._totalGold -= 1000;
            Debug.Log("레벨업");
            //_upgradeSubject.OnNext(Unit.Default);
        }

    }
}