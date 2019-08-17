using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMarks : MonoBehaviour
{
    public void ClickCrookTab()
    {
        UnitsManager.instance.ChangeTab(UnitsManager.Tabs.crook);
    }

    public void ClickSnakeTab()
    {
        UnitsManager.instance.ChangeTab(UnitsManager.Tabs.snake);
    }

    public void ClickGangTab()
    {
        UnitsManager.instance.ChangeTab(UnitsManager.Tabs.gang);
        UnitsManager.instance.ShowFactories();
    }
}
