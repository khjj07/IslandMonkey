using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InStateMonkey : MonoBehaviour, IStateMonkey
{ 
    private MonkeyContext _monkeyContext;

    public void Handle(MonkeyContext monkeyContext)
    {
        if (!_monkeyContext)
        {
            _monkeyContext = monkeyContext;
        }
        Debug.Log("Jump IN");
        // 점프해서 건물 안으로 이동
        // 출입구 -> 건물내부 위치로 이동
        // 애니메이션 재생
    }
}
