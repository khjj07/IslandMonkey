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
        // �����ؼ� �ǹ� ������ �̵�
        // �ǹ����� -> ���Ա� ��ġ�� �̵�
        // �ִϸ��̼� ���
    }
}
