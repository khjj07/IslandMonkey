using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class GameManager : Singleton<GameManager>
    {
        // ���� ���� ����
        public bool isLive;


        // �ǹ�, ������, ���, ����, �ٳ��� ��ȭ ����
        // ...

        public int goldAmount;
        public int ShellfishCount;
        public int BananaCount;

        // �ǹ� �Ǽ� ���� ����

        public Sprite[] buildingSprites;
        public String[] buildingName;
        public int[] buildingGold;



        // UI ����
        public TextMeshProUGUI GoldText;

        public Image BuildingPanel;
        private Animator UIanim;
        private bool isBuildingClick;



      

        // �̱��� ������ ����ϱ� ���� �ν��Ͻ� ����
        private static GameManager _instance;

        // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
        public static GameManager Instance
        {
            get
            {
                // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ�
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
            // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� ����
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            //���� ��ȯ�Ǵ��� �ν��Ͻ��� �ı����� ����
            DontDestroyOnLoad(gameObject);

             UIanim = BuildingPanel.GetComponent<Animator>();
        }

        void LateUpdate()
        {
            GoldText.text = string.Format("{0:n0}", Mathf.SmoothStep(float.Parse(GoldText.text), goldAmount, 0.5f));
        }


        // �ǹ��� �����ϴ� ���
        public void PurchaseBuilding(int cost)
        {
            // ��尡 ������� Ȯ���ϰ� �ǹ��� ����
            if (goldAmount >= cost)
            {
                //�ǹ� ����
                //��� ����

                // �ǹ��� ������ �� 
                // ...

            }
        }

        // �����̸� �����ϴ� ���
        public void SpawnMonkey()
        {
            // �����̸� ����
            // ...
        }

        // ��带 ȹ���ϴ� ���
        public void GainGold(int amount)
        {
            goldAmount += amount;
            // ��带 ȹ��
            // ...

        }

        public void BuildingClick()
        {
            // ���� Ŭ����
            // ...
        }

        // ���� ��ŸƮ
        public void StartGame()
        {
            // ���� ���۽�
            // �� ��ȯ �� �ʴ� �ٳ��� ���
        }

        public void Pause()
        {
            // ���� ����
        }

        public void ShowRewardAd()
        {
            // ���� ��û
        }

        // �ٳ��� �ʴ� ����
        IEnumerator BananaPerSecond()
        {
            // �ʴ� �ٳ��� ����?
            yield return new WaitForSeconds(1);
        }

        //���� ��ư Ŭ��
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


        //���� �߰�





        // ���� �Ŵ����� ���¸� �ʱ�ȭ
        public void ResetGameManager()
        {

            goldAmount = 0;
            ShellfishCount = 0;
            BananaCount = 0;
        }
    }

