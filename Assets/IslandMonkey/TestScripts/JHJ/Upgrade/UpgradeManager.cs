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
            // ���̺� ���� �ε�
            UpgradeLoad();
        }

        // ���۽� ���׷��̵� ȣ��
        public void CityUpgrage(int itemId)
        {
            cityUpgrade.UpgradeItem(itemId); 
            //cityUpgrade ��ũ��Ʈ���� ȣ�� (ID item : upgrade)
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

        // ���׷��̵� ������ ����
        public void UpgradeSave()
        {
            // ��� ������ Ŭ�����ϰ� ����
            UpgradeListClear();

            // ����Ʈ �ۼ�
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

            // ������ ����
            DataManager.SaveData();
        }
        // ������ �ε�
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

        // Ŭ���� ȣ��
        void UpgradeListClear()
        {
            DataManager.upgradeData.cityItems.Clear();
            DataManager.upgradeData.residentsItems.Clear();
            DataManager.upgradeData.comfortItems.Clear();

        }*/
    }
}