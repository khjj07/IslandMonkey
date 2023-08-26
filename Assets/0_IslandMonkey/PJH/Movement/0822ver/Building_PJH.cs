using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_PJH : MonoBehaviour
{
    public bool IsInMonkey { get { return _isInMonkey; } set { _isInMonkey = value; } }
    private bool _isInMonkey = false;

    // Building�� ���������� �������� animator�ΰ� �Ʒ��� animator�� �ٲ�(���ο� �ǹ� ������ ������ �ٸ� �ִϸ��̼� �ʿ��ϱ� ����)
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
