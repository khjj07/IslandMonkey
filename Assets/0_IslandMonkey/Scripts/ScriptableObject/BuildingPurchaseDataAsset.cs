using System.Collections;
using System.Collections.Generic;
using Assets._0_IslandMonkey.Scripts.ScriptableObject;
using UnityEngine;

public class BuildingPurchaseDataAsset : ScriptableObject
{
    public virtual List<BuildingPurchaseData> GetData()
    {
        return new List<BuildingPurchaseData>();
    }
}
