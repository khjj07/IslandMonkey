using System;
using Unity.Collections;
using UnityEngine;

namespace Assets._0_IslandMonkey.Scripts.ScriptableObject
{
    [Serializable]
    public class BuildingPurchaseData
    {
        [Header("���� ����"), ReadOnly]
        public bool isVoyage=false;
        [Header("�ü� ������")]
        public Sprite iconImage;

        [Header("�ü� �̸�")]
        public string facilityName;

        [Header("�ü� Ÿ��")]
        public BuildingManager.Type facilityType;

        [TextArea, Header("�ü� ����")]
        public string facilityExplanation;

        [Header("���� ���")]
        public int cost;

        [Header("�ҿ� �ð�")]
        public int duration;
    }

    [Serializable]
    public class VoyageBuildingPurchaseData : BuildingPurchaseData
    {
        [Header("���� ����"), ReadOnly]
        public new bool isVoyage = true;
    }
}