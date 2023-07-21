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

        private void Start()
        {
            // �� ��ȯ�� �Ͼ�� �� GameManager �ν��Ͻ��� �����ǵ��� ����
            DontDestroyOnLoad(gameObject);

                Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    
                        BuildingLevelText();
                    
                })
                .AddTo(this); // �������� GameManager�� �����Ͽ� OnDestroy() �� �������� ���� ����

           
        }
        public void Upgrade()
        {
            buildingLevel++;
        }

        public int buildingLevel = 1;

        [SerializeField]
        private TextMeshProUGUI buildingLevelText;

        public int BuildingLevel;

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