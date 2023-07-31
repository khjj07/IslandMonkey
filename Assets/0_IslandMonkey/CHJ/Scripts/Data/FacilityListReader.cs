using System.Collections.Generic;
using UnityEngine;

public class FacilityListReader : Singleton<FacilityListReader>
{
    public string facilityList;  // File name without extension
    public List<string[]> rowData = new List<string[]>();

    void Awake()
    {
        Load(facilityList);
    }

    public void Load(string facilityList)
    {
        TextAsset dataAsset = Resources.Load<TextAsset>("Data/facility_list");
        string[] lines = dataAsset.text.Split('\n');
        for (int i = 2; i < lines.Length; i++)  // 헤더 줄 생략
        {
            string[] row = lines[i].Split(',');
            rowData.Add(row);
        }
    }
}