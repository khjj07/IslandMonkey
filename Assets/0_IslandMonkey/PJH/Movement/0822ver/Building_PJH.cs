using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_PJH : MonoBehaviour
{
    public bool IsInMonkey { get { return _isInMonkey; } set { _isInMonkey = value; } }
    private bool _isInMonkey = false;

    // Building에 도착했을때 원숭이의 animator로가 아래의 animator로 바뀜(새로운 건물 도착할 때마다 다른 애니메이션 필요하기 때문)
    public RuntimeAnimatorController MonkeyWithBuildingAnimatorController;

    public void changeIsInMonkey()
    {
        if (_isInMonkey)
        {
            _isInMonkey = true;
        }
        else
        {
            _isInMonkey = false;
        }
    }
}
