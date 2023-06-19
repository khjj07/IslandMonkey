using System;
using UniRx;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private Subject<Unit> _upgradeSubject = new();

    public int Level { get; private set; }

    public IObservable<Unit> OnUpgradeAsObservable()
    {
        
        return _upgradeSubject;
    }

    public void Upgrade()
    {
        Level++;
        _upgradeSubject.OnNext(Unit.Default);
    }
}