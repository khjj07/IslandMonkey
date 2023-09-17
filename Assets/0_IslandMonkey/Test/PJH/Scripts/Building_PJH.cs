using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class Building_PJH : MonoBehaviour
{
    public bool isOccupied { get { return _isOccupied; } set { _isOccupied = value; } }
    public bool _isOccupied = false;

    public bool isInMonkey { get { return _isInMonkey.Value; } set { _isInMonkey.Value = value; } }
    private ReactiveProperty<bool> _isInMonkey = new ReactiveProperty<bool>(false);

    private Animator _animator;
    public Transform entrance;

    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        entrance = transform.Find("Entrance");
        PlayAnimation();
    }

    private void Start()
    {
        _isInMonkey
            .DistinctUntilChanged()
            .Subscribe(_ =>
            {
                PlayAnimation();
            });
    }

    public void ChangeIsOccupied()
    {
        _isOccupied = _isOccupied ?  false :  true;
    }
    public void ChangeIsInMonkey()
    {
        _isInMonkey.Value = _isInMonkey.Value ? false : true;
    }

    public void PlayAnimation()
    {
        if (_isInMonkey.Value)
        {
            _animator.SetBool("isInMonkey", true);
        }
        else
        {
            _animator.SetBool("isInMonkey", false);
        }
    }
}
