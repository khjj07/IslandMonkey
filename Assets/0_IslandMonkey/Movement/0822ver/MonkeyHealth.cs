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

    // 임의 설정) 범위: 0~10, 1초에 1씩 체력감소 및 회복
    public float currentMonkeyHealth;
    public MonkeyPosition currentMonkeyPosition;

    [SerializeField]
    private float maxHealth = 3.0f; 
    [SerializeField]
    private float minHealth = 0.0f;
    [SerializeField]
    private float healthUpdatePerSecond = 1.0f; // 1초

    private MonkeyMovement MonkeyMovement;

    private void Awake()
    {
        MonkeyMovement = GetComponent<MonkeyMovement>();
    }

    void Start()
    {
        currentMonkeyHealth = maxHealth;
        currentMonkeyPosition = MonkeyPosition.InOwnFacility;

        StartCoroutine(UpdateHealth()); // 1초마다 health 업데이트
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

            yield return new WaitForSeconds(1.0f); // 1초 동안 대기
        }
    }
    private void SetDestination()
    {
        if (currentMonkeyPosition == MonkeyPosition.InOwnFacility && currentMonkeyHealth <= minHealth)
        {
            Debug.Log("집을 떠나자 숭숭");
            //MonkeyMovement.GoToOtherFacility();
        }
        else if (currentMonkeyPosition == MonkeyPosition.InOtherFacility && currentMonkeyHealth >= maxHealth)
        {
            Debug.Log("집으로 가자 숭숭");
            //MonkeyMovement.ComeBackHome();
        }
    }
}
