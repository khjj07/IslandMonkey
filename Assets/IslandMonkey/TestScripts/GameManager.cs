using UnityEngine;

public class GameManager : Singleton<GameManager>
{

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
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }


    // �ǹ�, ������, ���

    private int buildingCount;
    private int monkeyCount;
    private int goldAmount;

    private void Awake()
    {
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
    }

    // �ǹ��� �����ϴ� ���
    public void PurchaseBuilding(int cost)
    {
        // ��尡 ������� Ȯ���ϰ� �ǹ��� ����
        if (goldAmount >= cost)
        {
            buildingCount++;
            goldAmount -= cost;

            // �ǹ��� ������ �� 

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


    //���� �߰�





    // ���� �Ŵ����� ���¸� �ʱ�ȭ
    public void ResetGameManager()
    {
        buildingCount = 0;
        monkeyCount = 0;
        goldAmount = 0;
    }
}