using System;
using Unity.Collections;
using UnityEngine;

namespace Assets._0_IslandMonkey.Scripts.ScriptableObject
{
    [Serializable]
    public class BuildingPurchaseData
    {
        [Header("시설 아이콘")]
        public Sprite iconImage;

        [Header("시설 이름")]
        public string facilityName;

        [Header("시설 타입")]
        public BuildingManager.Type facilityType;

        [TextArea, Header("시설 설명")]
        public string facilityExplanation;

        [Header("구매 비용")]
        public int cost;

        [Header("소요 시간")]
        public int duration;
    }
}