using UnityEngine;

namespace Manager
{
    public class UpgradeManager : MonoBehaviour
    {
        /*[SerializeField]
        [Header("Components")]
        private Building cityUpgrade;

        private void Awake()
        {
            // 세이브 파일 로드
            UpgradeLoad();
        }

        // 시작시 업그레이드 호출
        public void CityUpgrage(int itemId)
        {
            cityUpgrade.UpgradeItem(itemId); 
            //cityUpgrade 스크립트에서 호출 (ID item : upgrade)
        }

        public void ResidentsUpgrade(int itemId)
        {
            residentsUpgrade.UpgradeItem(itemId);
        }

        public void ComfortUpgrade(int itemId)
        {
            comfortUpgrade.UpgradeItem(itemId);
        }

        public void AutomationUpgrade(int itemId)
        {
            automationUpgrade.UpgradeItem(itemId);
        }


        //Update UI
        public void UpdateUI()
        {
            cityUpgrade.UpdateUI();
            residentsUpgrade.UpdateUI();
            comfortUpgrade.UpdateUI();
            automationUpgrade.UpdateUI();
        }

        // 업그레이드 데이터 저장
        public void UpgradeSave()
        {
            // 모든 아이템 클리어하고 시작
            UpgradeListClear();

            // 리스트 작성
            for (int i = 0; i < cityUpgrade.Items.Length; i++)
            {
                DataManager.upgradeData.cityItems.Add(cityUpgrade.Items[i].itemData);
            }
            for (int i = 0; i < residentsUpgrade.Items.Length; i++)
            {
                DataManager.upgradeData.residentsItems.Add(residentsUpgrade.Items[i].itemData);
            }
            for (int i = 0; i < comfortUpgrade.Items.Length; i++)
            {
                DataManager.upgradeData.comfortItems.Add(comfortUpgrade.Items[i].itemData);
            }
            for (int i = 0; i < automationUpgrade.Items.Length; i++)
            {
                DataManager.upgradeData.automationItems.Add(automationUpgrade.Items[i].itemData);
            }

            // 데이터 저장
            DataManager.SaveData();
        }
        // 데이터 로드
        public void UpgradeLoad()
        {
            if (DataManager.upgradeData.cityItems.Count > 0)
                for (int i = 0; i < cityUpgrade.Items.Length; i++)
                {
                    //write item from file to item on scene
                    cityUpgrade.Items[i].itemData = DataManager.upgradeData.cityItems[i];
                }
            if (DataManager.upgradeData.residentsItems.Count > 0)
                for (int i = 0; i < residentsUpgrade.Items.Length; i++)
                {
                    residentsUpgrade.Items[i].itemData = DataManager.upgradeData.residentsItems[i];
                }
            if (DataManager.upgradeData.comfortItems.Count > 0)
                for (int i = 0; i < comfortUpgrade.Items.Length; i++)
                {
                    comfortUpgrade.Items[i].itemData = DataManager.upgradeData.comfortItems[i];
                }
            if (DataManager.upgradeData.automationItems.Count > 0)
                for (int i = 0; i < automationUpgrade.Items.Length; i++)
                {
                    automationUpgrade.Items[i].itemData = DataManager.upgradeData.automationItems[i];
                }

        }

        // 클리어 호출
        void UpgradeListClear()
        {
            DataManager.upgradeData.cityItems.Clear();
            DataManager.upgradeData.residentsItems.Clear();
            DataManager.upgradeData.comfortItems.Clear();

        }*/
    }
}