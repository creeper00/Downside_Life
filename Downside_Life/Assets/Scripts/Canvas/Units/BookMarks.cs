using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMarks : MonoBehaviour
{
    public void ClickCrookTab()
    {
        UnitsManager.instance.ChangeTab(UnitsManager.Tabs.crook);
        UnitsManager.instance.showCrooks();
    }

    public void ClickSnakeTab()
    {
        UnitsManager.instance.ChangeTab(UnitsManager.Tabs.snake);
        UnitsManager.instance.showSnakes();
    }

    public void ClickGangTab()
    {
        UnitsManager.instance.ChangeTab(UnitsManager.Tabs.gang);
        UnitsManager.instance.showGangs();
    }
}
