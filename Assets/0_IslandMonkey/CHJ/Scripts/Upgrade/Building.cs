using System;
using TMPro;
using UniRx;
using UnityEngine;

// �������̽��� �ִ� �ڸ� �������̽��� ������ �и�����
// ������

namespace Assets._0_IslandMonkey.CHJ.Scripts.Upgrade
{

    public class Building : Singleton<Building>
    {
        private Subject<Unit> _upgradeSubject = new Subject<Unit>();
        public IObservable<Unit> OnUpgradeAsObservable() => _upgradeSubject;

        public int buildingLevel = 1;

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
            // �� ��ȯ�� �Ͼ�� �� GameManager �ν��Ͻ��� �����ǵ��� ����

                if (buildingLevelText != null)

                {
                    buildingLevelText.text = " " + buildingLevel;
                    int _nextBuildingLevel = buildingLevel + 1;
                    buildingNextLevelText.text = " " + _nextBuildingLevel;
            }
                
        }


        public void BuildingUpgrade()
        {
            buildingLevel++;
            GameManager._totalGold -= 1000;
            Debug.Log("������");
            //_upgradeSubject.OnNext(Unit.Default);
        }

    }
}