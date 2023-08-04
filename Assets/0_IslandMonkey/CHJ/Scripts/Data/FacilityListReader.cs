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
        // 파일 경로로 데이터 에셋 불러오기
        string[] lines = dataAsset.text.Split('\n');
        
    }
}