using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets._0_IslandMonkey.CHJ.Scripts.Upgrade
{
    public class Monkey : Singleton<Monkey>
    {
        private Subject<Unit> _upgradeSubject = new Subject<Unit>();
        public IObservable<Unit> OnUpgradeAsObservable() => _upgradeSubject;

        public int monkeyLevel = 1;

        [SerializeField]
        private TextMeshProUGUI monkeyLevelText;

        private void Start()
        {
            // 씬 전환이 일어났을 때 GameManager 인스턴스가 유지되도록 설정
            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {
            // 씬 전환이 일어났을 때 GameManager 인스턴스가 유지되도록 설정


            if (monkeyLevelText != null)

            {
                monkeyLevelText.text = " " + monkeyLevel;
            }
        }

        public void MonkeyUpgrade()
        {
            monkeyLevel++;
            GameManager._totalGold -= 100;
            Debug.Log("레벨업");
        }
    }
}