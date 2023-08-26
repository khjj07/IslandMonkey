using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBuildingStateMonkey : MonoBehaviour, IStateMonkey
{
    private MonkeyContext _monkeyContext;

    public void Handle(MonkeyContext monkeyContext)
    {
        if (!_monkeyContext)
        {
            _monkeyContext = monkeyContext;
        }
        Debug.Log("inBuilding");
        // 애니메이션 재생, 이때 건물이랑 프레임 맞춰서 같이 실행
    }
}
