using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager instance;

    [SerializeField]
    private float bookmarkClickedYChange;
    [SerializeField]
    private GameObject crookBookmark, snakeBookmark, gangBookmark;
    private Vector3 crookBookmarkPosition, snakeBookmarkPosition, gangBookmarkPosition;
    [SerializeField]
    private GameObject crookTab, snakeTab, gangTab;
    [SerializeField]
    private Text RichMoneyChange;
    [SerializeField]
    Transform crookListItemPrefab, snakeListItemPrefab, gangListItemPrefab, factoryPrefab;
    [SerializeField]
    private GameObject unitContents;
    //[SerializeField]          //씬 충돌 나지 않도록 나중에 주석 해제할 것
    private Transform itemPrefab;
    [SerializeField]
    private GameObject itemContents;
    [SerializeField]
    GameObject factory1, factory2, factory3;

    public enum Tabs
    {
        crook, snake, gang
    }

    public Tabs currentTab;

    void Awake()
    {
        crookBookmarkPosition = crookBookmark.transform.position;
        snakeBookmarkPosition = snakeBookmark.transform.position;
        gangBookmarkPosition = gangBookmark.transform.position;
    }

    private void ResetUnitScrollView()
    {
        foreach (Transform child in unitContents.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>스크롤 뷰를 사기꾼 목록으로 만들고 갱신함</summary>
    public void ShowCrooks()
    {
        ResetUnitScrollView();
        unitContents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, GameManager.instance.crooks.Count * 120 - 510, 0);
        int index = 0;
        foreach (var unit in GameManager.instance.crooks)
        {
            var listItemObject = Instantiate(crookListItemPrefab, unitContents.transform);
            var crookItemList = listItemObject.GetComponent<UnitListItem>();
            crookItemList.SetUnitInformation(index, unit);
            ++index;
        }
    }

    public void ShowSnakes()
    {
        ResetUnitScrollView();
        unitContents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, GameManager.instance.snakes.Count * 120 - 510, 0);
        int index = 0;
        foreach (var unit in GameManager.instance.snakes)
        {
            var listItemObject = Instantiate(snakeListItemPrefab, unitContents.transform);
            var snakeItemList = listItemObject.GetComponent<UnitListItem>();
            snakeItemList.SetUnitInformation(index, unit);
            ++index;
        }
    }

    public void ShowGangs()
    {
        ResetUnitScrollView();
        unitContents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, GameManager.instance.gangs.Count * 120 - 510, 0);
        int index = 0;
        foreach (var unit in GameManager.instance.gangs)
        {
            var listItemObject = Instantiate(gangListItemPrefab, unitContents.transform);
            var gangItemList = listItemObject.GetComponent<UnitListItem>();
            gangItemList.SetUnitInformation(index, unit);
            ++index;
        }
    }

    private void ResetItemScrollView()
    {
        foreach (Transform child in itemContents.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowCrookItems()
    {
        ResetItemScrollView();
        itemContents.GetComponent<RectTransform>().sizeDelta = new Vector3(GameManager.instance.crookItems.Count * 80, 0, 0);
        int index = 0;
        foreach(var item in GameManager.instance.crookItems)
        {
            var listItemObject = Instantiate(itemPrefab, itemContents.transform);
            var crookItemListItem = listItemObject.GetComponent<ItemListItem>();
            crookItemListItem.SetItemInformation(index, item);
            ++index;
        }
    }

    public void ShowSnakeItems()
    {

    }

    public void ShowGangItems()
    {

    }

    public void DeleteSlot(Tabs tab, int slotIndex)
    {
        switch(tab)
        {
            case Tabs.crook:
                Destroy(crookTab.transform.Find("Slot" + slotIndex).GetChild(1).gameObject);
                break;
            case Tabs.snake:
                Destroy(snakeTab.transform.Find("Slot" + slotIndex).GetChild(1).gameObject);
                break;
            case Tabs.gang:
                Destroy(gangTab.transform.Find("Slot" + slotIndex).GetChild(1).gameObject);
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
        crookBookmark.transform.position = crookBookmarkPosition + new Vector3((currentTab == Tabs.crook) ? bookmarkClickedYChange : 0, 0, 0);

        snakeTab.SetActive(currentTab == Tabs.snake);
        snakeBookmark.transform.position = snakeBookmarkPosition + new Vector3((currentTab == Tabs.snake) ? bookmarkClickedYChange : 0, 0, 0);

        gangTab.SetActive(currentTab == Tabs.gang);
        gangBookmark.transform.position = gangBookmarkPosition + new Vector3((currentTab == Tabs.gang) ? bookmarkClickedYChange : 0, 0, 0);
        switch (tab)
        {
            case Tabs.crook:
                ShowCrooks();
                ShowCrookItems();
                break;
            case Tabs.snake:
                ShowSnakes();
                ShowSnakeItems();
                break;
            case Tabs.gang:
                ShowGangs();
                ShowGangItems();
                break;
        }
    }

    public void ShowFactories()
    {
        List<GameObject> factories = new List<GameObject>();
        factories.Add(GameObject.Find("Factory1"));
        factories.Add(GameObject.Find("Factory2"));
        factories.Add(GameObject.Find("Factory3"));
        for (int i=0; i<3; i++)
        {
            foreach (Transform child in factories[i].GetComponent<Transform>())
            {
                Destroy(child.gameObject);
            }
        }
        for (int i=0; i<GameManager.instance.factories.Length; i++)
        {
            if (GameManager.instance.factories[i] != null)
            {
                var a = Instantiate(factoryPrefab, factories[i].transform);
                var b = a.GetComponent<Factory>();
                b.SetFactoryListItem(GameManager.instance.factories[i]);
            }
        }
    }
}
