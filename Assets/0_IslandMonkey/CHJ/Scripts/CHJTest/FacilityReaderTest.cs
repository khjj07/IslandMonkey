using UnityEngine;

public class FacilityReaderTest : MonoBehaviour
{
    void Start()
    {
        // FacilityListReader�� �̱��� �ν��Ͻ��� ����
        FacilityListReader facilityListReader = FacilityListReader.instance;

        // FacilityListReader�� rowData�� ���
        for (int i = 2; i < facilityListReader.rowData.Count; i++)  // ��� �� �ǳʶٱ�
        {
            string[] row = facilityListReader.rowData[i];
            Debug.Log(row[0]);  // �ü� id ���
        }
    }
}