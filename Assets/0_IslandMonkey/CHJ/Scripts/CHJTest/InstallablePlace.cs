using System;
using UnityEngine;
using UniRx;

public class InstallablePlace : MonoBehaviour
{
    private ReactiveProperty<bool> isOccupied = new ReactiveProperty<bool>(false);

    public IReadOnlyReactiveProperty<bool> IsOccupied => isOccupied;

    public void SetOccupied(bool occupied)
    {
        isOccupied.Value = occupied;
    }
}