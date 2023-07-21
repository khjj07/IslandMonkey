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
    [Serializable]
    public class BuildingData
    {
        public int BuildingType;
        public int BuildingLevel;
        public Vector3 BuildingPosition;
        // 여기에 다른 필요한 빌딩 데이터를 추가할 수 있습니다.
    }




    public class Building : MonoBehaviour
    {
        private Subject<Unit> _upgradeSubject = new Subject<Unit>();
        public IObservable<Unit> OnUpgradeAsObservable() => _upgradeSubject;


        public int buildingLevel = 1;

        [SerializeField]
        private TextMeshProUGUI buildingLevelText;


        private void Start()
        {
            // 씬 전환이 일어났을 때 GameManager 인스턴스가 유지되도록 설정
            DontDestroyOnLoad(gameObject);

            Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {

                if (buildingLevelText != null)

                {
                    buildingLevelText.text = " " + buildingLevel;
                }


            })
            .AddTo(this); // 옵저버블에 GameManager를 연결하여 OnDestroy() 시 옵저버블 구독 해지


        }


        public void BuildingUpgrade()
        {
            buildingLevel++;
            if (buildingLevelText != null)

            {
                buildingLevelText.text = " " + buildingLevel;
            }
            //_upgradeSubject.OnNext(Unit.Default);
        }

    }
}