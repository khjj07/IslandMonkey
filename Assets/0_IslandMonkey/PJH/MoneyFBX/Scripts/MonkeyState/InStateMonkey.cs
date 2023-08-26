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
        // �����ؼ� �ǹ� ������ �̵�
        // ���Ա� -> �ǹ����� ��ġ�� �̵�
        // �ִϸ��̼� ���
    }
}
