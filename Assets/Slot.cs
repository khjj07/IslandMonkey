using System.Collections;
using System.Collections.Generic;
using Assets._0_IslandMonkey.Scripts.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool isStudy;
    public bool isLocked;
    public Image icon;
    public TextMeshProUGUI facilityName;
    public TextMeshProUGUI facilityExplanation;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI duration;

    public void Build(BuildingPurchaseDataAsset.BuildingPurchaseData data)
    {
        isStudy = data.isStudy;
        icon.sprite = data.iconImage;
        facilityName.SetText(data.facilityName);
        facilityExplanation.SetText(data.facilityExplanation);
        cost.SetText(data.cost.FormatLargeNumber());
        duration.SetText(data.duration.FormatLargeNumber());
    }
}
