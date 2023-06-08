using System.Collections;
using UnityEngine;

namespace Assets.IslandMonkey.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {

        // 건물, 원숭이, 골드
        // ...

        private int buildingCount;
        private int monkeyCount;
        private int goldAmount;


        // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
        private static GameManager _instance;

        // 인스턴스에 접근하기 위한 프로퍼티
        public static GameManager Instance
        {
            get
            {
                // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                    if (_instance == null)
                        Debug.Log("no Singleton obj");
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            //씬이 전환되더라도 인스턴스가 파괴되지 않음
            DontDestroyOnLoad(gameObject);
        }

        // 건물을 구매하는 기능
        public void PurchaseBuilding(int cost)
        {
            // 골드가 충분한지 확인하고 건물을 구매
            if (goldAmount >= cost)
            {
                buildingCount++;
                goldAmount -= cost;

                // 건물을 구매한 후 
                // ...

            }
        }

        // 원숭이를 생성하는 기능
        public void SpawnMonkey()
        {
            // 원숭이를 생성
            // ...
        }

        // 골드를 획득하는 기능
        public void GainGold(int amount)
        {
            goldAmount += amount;
            // 골드를 획득
            // ...

        }

        public void BuildingClick()
        {
            // 빌딩 클릭시
            // ...
        }

        //Method to start the game
        public void StartGame()
        {
            // 게임 시작시
            // 씬 전환 및 초당 골드 계산
        }
        //Method to pause
        public void Pause()
        {
            // 게임 정지
        }

        //Mthhod call ads from AdmobManager
        public void ShowRewardAd()
        {
            // 광고 시청
        }

        //The loop of adding money per second
        IEnumerator MoneyPerSecond()
        {
            // 초당 골드 생산?
            yield return new WaitForSeconds(1);
        }

        //차후 추가





        // 게임 매니저의 상태를 초기화
        public void ResetGameManager()
        {
            buildingCount = 0;
            monkeyCount = 0;
            goldAmount = 0;
        }
    }
}