using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTabPanel : TabPanel<BuildingTabPanel>
{
    public Image tabButton;
    public Sprite activeImage;
    public Sprite inActiveImage;

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
