using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.IslandMonkey.TestScripts.JHJ
{
    public class DataSaver : MonoBehaviour
    {
        // 파일로 뺀다
        public string fileName;

        //(int[]), Save(string[]), Save(SampleClass[])
        public void Save(object[] objects)
        {
            // 로컬로 저장
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // 만든 파일 열기
            using (FileStream fileStream = File.Open(Application.persistentDataPath + "/" + fileName + ".bin", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, objects); // 파일 오브젝트 작성
                fileStream.Close();  // 파일 닫기
            }
        }
        // 데이터 로드
        public object[] Load()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // 파일이 존재하는 지 확인
            if (!File.Exists(Application.persistentDataPath + "/" + fileName + ".bin"))
                return null;

            using (FileStream fileStream = (File.Open(Application.persistentDataPath + "/" + fileName + ".bin", FileMode.Open)))
            {
                object[] obj = binaryFormatter.Deserialize(fileStream) as object[];

                return obj; // 데이터 매니저로 리턴
            }
        }

    }
}
