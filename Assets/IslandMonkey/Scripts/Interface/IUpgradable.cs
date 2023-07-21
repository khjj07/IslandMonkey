using System;
using UniRx;

namespace Assets.IslandMonkey.Scripts.Interface
{
    interface IUpgradable
    {
        IObservable<Unit> OnUpgradeAsObservable();
        void Upgrade();
    }
}
