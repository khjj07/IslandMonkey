using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class GameManager : Singleton<GameManager>
    {
        // 게임 실행 관리
        public bool isLive;


        // 건물, 원숭이, 골드, 조개, 바나나 재화 관리
        // ...

        public int goldAmount;
        public int ShellfishCount;
        public int BananaCount;

        // 건물 건설 관련 변수

        public Sprite[] buildingSprites;
        public String[] buildingName;
        public int[] buildingGold;



        // UI 관리
        public TextMeshProUGUI GoldText;

        public Image BuildingPanel;
        private Animator UIanim;
        private bool isBuildingClick;



      

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
                        UnityEngine.Debug.Log("no Singleton obj");
                }
                return _instance;
            }
        }

        private void Awake()
        {
            UIanim = BuildingPanel.GetComponent<Animator>();

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

             UIanim = BuildingPanel.GetComponent<Animator>();
        }

        void LateUpdate()
        {
            GoldText.text = string.Format("{0:n0}", Mathf.SmoothStep(float.Parse(GoldText.text), goldAmount, 0.5f));
        }


        // 건물을 구매하는 기능
        public void PurchaseBuilding(int cost)
        {
            // 골드가 충분한지 확인하고 건물을 구매
            if (goldAmount >= cost)
            {
                //건물 구매
                //골드 감소

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

        // 게임 스타트
        public void StartGame()
        {
            // 게임 시작시
            // 씬 전환 및 초당 바나나 계산
        }

        public void Pause()
        {
            // 게임 정지
        }

        public void ShowRewardAd()
        {
            // 광고 시청
        }

        // 바나나 초당 생산
        IEnumerator BananaPerSecond()
        {
            // 초당 바나나 생산?
            yield return new WaitForSeconds(1);
        }

        //빌딩 버튼 클릭
        public void ClickBuildingBtn()
        {
            if (isBuildingClick)
            {
                UIanim.SetTrigger("doHide");
                isBuildingClick = false;
            }

            if (isBuildingClick)
                UIanim.SetTrigger("doHide");
            else
                UIanim.SetTrigger("doShow");

            isBuildingClick = !isBuildingClick;
        }


        //차후 추가





        // 게임 매니저의 상태를 초기화
        public void ResetGameManager()
        {

            goldAmount = 0;
            ShellfishCount = 0;
            BananaCount = 0;
        }
    }

