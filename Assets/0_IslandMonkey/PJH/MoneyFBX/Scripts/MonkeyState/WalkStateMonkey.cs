using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStateMonkey : MonoBehaviour, IStateMonkey
{
    private MonkeyContext _monkeyContext;

    public void Handle(MonkeyContext monkeyContext)
    {
        if (!_monkeyContext)
        {
            _monkeyContext = monkeyContext;
        }
        Debug.Log("walk");
        
        // �ִϸ��̼� ���(�ȱ�)
    }
}
