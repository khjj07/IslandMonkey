using System;
using UnityEngine;
using UniRx;

public class Ground : MonoBehaviour
{
    private bool isOccupied = false;

    public bool IsOccupied => isOccupied;


    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }
}