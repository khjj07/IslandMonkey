using UnityEngine;

public class FacilityReaderTest : MonoBehaviour
{
    void Start()
    {
        // FacilityListReader의 싱글톤 인스턴스에 접근
        FacilityListReader facilityListReader = FacilityListReader.instance;

        // FacilityListReader의 rowData를 사용
        for (int i = 2; i < facilityListReader.rowData.Count; i++)  // 헤더 행 건너뛰기
        {
            string[] row = facilityListReader.rowData[i];
            Debug.Log(row[0]);  // 시설 id 출력
        }
    }
}