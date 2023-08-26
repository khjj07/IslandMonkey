using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutStateMonkey : MonoBehaviour, IStateMonkey
{
    private MonkeyContext _monkeyContext;

    public void Handle(MonkeyContext monkeyContext)
    {
        if (!_monkeyContext)
        {
            _monkeyContext = monkeyContext;
        }
        Debug.Log("Jump Out");
        // 점프해서 건물 밖으로 이동
        // 건물내부 -> 출입구 위치로 이동
        // 애니메이션 재생
    }
}
