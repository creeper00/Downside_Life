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
    private Text RichMoneyChange;
    [SerializeField]
    Transform prefab, crookPrefab, snakePrefab, gangPrefab;
    [SerializeField]
    GameObject contents;

    public enum Tabs
    {
        crook, snake, gang
    }

    private Tabs currentTab;

    void Awake()
    {
        instance = this;
    }

    public void resetScrollView()
    {
        foreach (Transform child in contents.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>스크롤 뷰를 사기꾼 목록으로 만들고 갱신함</summary>
    public void showCrooks()
    {
        resetScrollView();
        contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, GameManager.instance.crooks.Count * 140, 0);
        int index = 0;
        foreach (var unit in GameManager.instance.crooks)
        {
            var listItemObject = Instantiate(prefab, contents.transform);
            var crookItemList = listItemObject.GetComponent<CrookListItem>();
            crookItemList.SetUnitInformation(index, unit);
            ++index;
        }
    }

    public void showSnakes()
    {
        resetScrollView();
        contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        for (int i = 0; i < GameManager.instance.snakes.Count; i++)
        {
            Instantiate(prefab, contents.transform);
            contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, contents.GetComponent<RectTransform>().sizeDelta.y + 140, 0);
        }
    }

    public void showGangs()
    {
        resetScrollView();
        contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        for (int i = 0; i < GameManager.instance.gangs.Count; i++)
        {
            Instantiate(prefab, contents.transform);
            contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, contents.GetComponent<RectTransform>().sizeDelta.y + 140, 0);
        }
    }

    public void DeleteSlot(Tabs tab, int slotIndex)
    {
        switch(tab)
        {
            case Tabs.crook:
                Destroy(crookTab.transform.Find("Slot" + slotIndex).GetChild(0).gameObject);
                break;
            case Tabs.snake:
                Destroy(snakeTab.transform.Find("Slot" + slotIndex).GetChild(0).gameObject);
                break;
            default:
                Debug.Log("Incorrect unit type.");
                break;
        }
    }

    public void UpdateRichMoneyChange()
    {
        
    }

    public void ChangeTab(Tabs tab)
    {
        currentTab = tab;
        crookTab.SetActive(currentTab == Tabs.crook);
        snakeTab.SetActive(currentTab == Tabs.snake);
        gangTab.SetActive(currentTab == Tabs.gang);
        switch (tab)
        {
            case Tabs.crook:
                showCrooks();

                break;
            case Tabs.snake:
                showSnakes();

                break;
            case Tabs.gang:
                showGangs();

                break;
        }
    }
}
