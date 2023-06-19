using System;
using UniRx;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField]
    private Subject<Unit> upgradeSubject = new Subject<Unit>();

    public int Level { get; set; }

    public IObservable<Unit> OnUpgradeAsObservable()
    {
        return upgradeSubject;
    }

    public void Upgrade()
    {
        Level++;
        upgradeSubject.OnNext(Unit.Default);
    }
}