using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonkeyHealth : MonoBehaviour
{
    public enum MonkeyPosition 
    {
        InOwnFacility,
        Moving,
        InOtherFacility,
    }

    // ���� ����) ����: 0~10, 1�ʿ� 1�� ü�°��� �� ȸ��
    public float currentMonkeyHealth;
    public MonkeyPosition currentMonkeyPosition;

    [SerializeField]
    private float maxHealth = 3.0f; 
    [SerializeField]
    private float minHealth = 0.0f;
    [SerializeField]
    private float healthUpdatePerSecond = 1.0f; // 1��

    private MonkeyMovement MonkeyMovement;

    private void Awake()
    {
        MonkeyMovement = GetComponent<MonkeyMovement>();
    }

    void Start()
    {
        currentMonkeyHealth = maxHealth;
        currentMonkeyPosition = MonkeyPosition.InOwnFacility;

        StartCoroutine(UpdateHealth()); // 1�ʸ��� health ������Ʈ
    }


    private IEnumerator UpdateHealth()
    {
        while (true)
        {
            switch (currentMonkeyPosition)
            {
                case (MonkeyPosition.InOwnFacility):
                    currentMonkeyHealth -= healthUpdatePerSecond;
                    break;
                case (MonkeyPosition.InOtherFacility):
                    currentMonkeyHealth += healthUpdatePerSecond;
                    break;
                case MonkeyPosition.Moving:
                    break;
            }
            SetDestination();

            yield return new WaitForSeconds(1.0f); // 1�� ���� ���
        }
    }
    private void SetDestination()
    {
        if (currentMonkeyPosition == MonkeyPosition.InOwnFacility && currentMonkeyHealth <= minHealth)
        {
            Debug.Log("���� ������ ����");
            //MonkeyMovement.GoToOtherFacility();
        }
        else if (currentMonkeyPosition == MonkeyPosition.InOtherFacility && currentMonkeyHealth >= maxHealth)
        {
            Debug.Log("������ ���� ����");
            //MonkeyMovement.ComeBackHome();
        }
    }
}
