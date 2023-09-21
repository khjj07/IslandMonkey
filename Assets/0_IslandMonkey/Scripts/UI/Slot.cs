using System.Collections;
using System.Collections.Generic;
using Assets._0_IslandMonkey.Scripts.Extension;
using Assets._0_IslandMonkey.Scripts.ScriptableObject;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public BuildingManager.Type type;
    public bool isLocked;
    public Image icon;
    public TextMeshProUGUI facilityName;
    public TextMeshProUGUI facilityExplanation;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI duration;
    public Image lockSlot;
    public void Start()
    {
        this.UpdateAsObservable().Where(_ => isLocked).DistinctUntilChanged()
            .Subscribe(_ => lockSlot.gameObject.SetActive(true));
        this.UpdateAsObservable().Where(_ => !isLocked).DistinctUntilChanged()
            .Subscribe(_ => lockSlot.gameObject.SetActive(false));
    }
    public void Build(BuildingPurchaseData data)
    {
        type = data.facilityType;
        icon.sprite = data.iconImage;
        facilityName.SetText(data.facilityName);
        facilityExplanation.SetText(data.facilityExplanation);
        cost.SetText(data.cost.FormatLargeNumber());
        duration.SetText(data.duration.FormatLargeNumber());
    }
   
    public virtual void Purchase()
    {

    }
}
