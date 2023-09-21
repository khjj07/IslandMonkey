using System;
using Unity.Collections;
using UnityEngine;

namespace Assets._0_IslandMonkey.Scripts.ScriptableObject
{
    [Serializable]
    public class BuildingPurchaseData
    {
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
}