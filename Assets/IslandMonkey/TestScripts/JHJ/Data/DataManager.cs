using UnityEngine;

namespace Assets.IslandMonkey.TestScripts.JHJ
{
    public class DataManager : MonoBehaviour
    {

        /*static DataSaver dataSaver;

        public static Data data;
        public static UpgradeData upgradeData;

        private void Awake()
        {
            Init();
        }

        void Init()
        {
            // 클래스 인스턴스 생성
            data = new Data();
            upgradeData = new UpgradeData();

            //Cached component from our Gameobject
            dataSaver = GetComponent<DataSaver>();

            LoadData();

            // 모든 첫 시작에 돈 100, 초당 1 지급
            if (!data.isFirstStartComplete)
            {
                data.isFirstStartComplete = true;

                data.Money = 100;
                data.MoneyByClick = 1;

                SaveData();
            }

        }

        //"DataManager.SaveData();"
        public static void SaveData()
        {
            object[] obj = new object[2]; 
            // 로컬 배열
            // 필요한 오브젝트 기록
            obj[0] = data;
            obj[1] = upgradeData;

            dataSaver.Save(obj); 
            // 데이터 저장
        }

        // "DataManager.LoadData();"
        public static void LoadData()
        {
            object[] obj = dataSaver.Load();  
            // 데이터 배열 파일에서 얻어오기
            // 필요한 오브젝트 확인
            if (obj != null)
            {
                data = obj[0] as Data;
                upgradeData = obj[1] as UpgradeData;
            }

        }*/

    }

}
