using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager instance;

    [SerializeField]
    private GameObject crookTab, snakeTab, gangTab;
    [SerializeField]
    private GameObject Slot1, Slot2, Slot3;
    [SerializeField]
    private Text RichMoneyChange;
    
    public enum Tabs
    {
        crook, snake, gang
    }

    private Tabs currentTab;

    void Awake()
    {
        instance = this;
        currentTab = Tabs.crook;
        ShowTab();
    }

    public void ChangeTab(Tabs tab)
    {
        currentTab = tab;
        ShowTab();
    }

    private void ShowTab()
    {
        crookTab.SetActive(currentTab == Tabs.crook);
        snakeTab.SetActive(currentTab == Tabs.snake);
        gangTab.SetActive(currentTab == Tabs.gang);
    }
}
