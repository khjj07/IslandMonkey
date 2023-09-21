using Assets._0_IslandMonkey.Scripts.ScriptableObject;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._0_IslandMonkey.Scripts.UI
{
    public class BuildingTabPanel : TabPanel<BuildingTabPanel>
    {
        public Image tabButton;
        public Sprite activeImage;
        public Sprite inActiveImage;
        public Slot slotPrefab;
        public BuildingPurchaseDataAsset purchaseData;
        public RectTransform origin;

        public void Start()
        {
            BuildItem();
        }

        private void BuildItem()
        {
            foreach (var data in purchaseData.GetData())
            {
                var instance = Instantiate(slotPrefab);
                instance.transform.SetParent(origin, false);
                instance.Build(data);
            }
        }

        public override void React()
        {
            if(current)
            {
                var buildingTab = (BuildingTabPanel)current;
                buildingTab.tabButton.sprite = inActiveImage;
            }
            
            base.React();
            tabButton.sprite= activeImage;
        }

        public void Quit()
        {
            gameObject.SetActive(false);
            tabButton.sprite = inActiveImage;
        }
    }
}
