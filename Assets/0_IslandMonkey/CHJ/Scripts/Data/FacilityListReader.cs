using System.Collections.Generic;
using UnityEngine;

public class FacilityListReader : MonoBehaviour
{
    public string facilityList;  // CSV file name
    public List<string[]> rowData = new List<string[]>();

    void Start()
    {
        Load(facilityList);
    }

    public void Load(string facilityList)
    {

        TextAsset dataAsset = Resources.Load<TextAsset>(facilityList);
        string[] lines = dataAsset.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(',');
            rowData.Add(row);
        }
    }
}
