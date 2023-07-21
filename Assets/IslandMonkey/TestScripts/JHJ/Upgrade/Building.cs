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
        // ���⿡ �ٸ� �ʿ��� ���� �����͸� �߰��� �� �ֽ��ϴ�.
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
            // �� ��ȯ�� �Ͼ�� �� GameManager �ν��Ͻ��� �����ǵ��� ����
            DontDestroyOnLoad(gameObject);

            Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {

                if (buildingLevelText != null)

                {
                    buildingLevelText.text = " " + buildingLevel;
                }


            })
            .AddTo(this); // �������� GameManager�� �����Ͽ� OnDestroy() �� �������� ���� ����


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