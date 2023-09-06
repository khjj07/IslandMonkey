using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewBuildingPurchaseData", menuName = "IslandMonkey/BuildingPurchaseData")]
public class BuildingPurchaseDataAsset : ScriptableObject
{
    [Serializable]
    public class BuildingPurchaseData
    {
        [Header("���� ����")]
        public bool isStudy;

        [Header("�ü� ������")]
        public Sprite iconImage;

        [Header("�ü� �̸�")]
        public string facilityName;

        [TextArea, Header("�ü� ����")]
        public string facilityExplanation;

        [Header("���� ���")]
        public int cost;

        [Header("�ҿ� �ð�")]
        public int duration;
    }

    public List<BuildingPurchaseData> data;
}
