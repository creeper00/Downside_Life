using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager instance;

    [Header("탭 전환 관련")]
    [SerializeField]
    private float bookmarkClickedYChange;
    [SerializeField]
    private GameObject crookBookmark, snakeBookmark, gangBookmark;
    [SerializeField]
    private GameObject crookTab, snakeTab, gangTab;
    [Header("부자 돈 변화 창 관련")]
    [SerializeField]
    private Text RichMoneyChange;
    [Header("Unit List관련")]
    [SerializeField]
    private GameObject unitContents;
    [SerializeField]
    private Transform crookListItemPrefab, snakeListItemPrefab, gangListItemPrefab, factoryPrefab;
    [Header("Item List관련")]
    [SerializeField]          //씬 충돌 나지 않도록 나중에 주석 해제할 것
    private Transform itemPrefab;
    [SerializeField]
    public GameObject itemContents;
    [Header("공장 관련")]
    public int numberOfFactories = 3;
    [SerializeField]
    private Transform attachedGangListItemPrefab;
    [SerializeField]
    GameObject factory1, factory2, factory3;
    [SerializeField]
    private GameObject[] attachedGangScrollViewContents = new GameObject[3];
    [Header("슬롯")]
    [SerializeField]
    public List<Slot> crookSlots, gangSlots, snakeSlots;

    public enum Tabs
    {
        initialnull, crook, snake, gang
    }

    public Tabs currentTab;

    void Awake()
    {
        currentTab = Tabs.initialnull;
        ChangeTab(Tabs.crook);

        //튜토리얼
        GameManager.instance.firstUnit.SetActive(true);
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
            listItemObject.GetComponent<UnitListItem>().SetUnitInformation(index, unit);
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
            listItemObject.GetComponent<UnitListItem>().SetUnitInformation(index, unit);
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
            listItemObject.GetComponent<UnitListItem>().SetUnitInformation(index, unit);
            ++index;
        }
    }

    public void ResetAttachedGangsScrollView()
    {
        for (int i = 0; i < numberOfFactories; ++i)
        {
            foreach (Transform child in attachedGangScrollViewContents[i].GetComponent<Transform>())
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void ShowAttachedGangs()
    {
        ResetAttachedGangsScrollView();
        for (int i = 0; i < numberOfFactories; ++i)
        {
            attachedGangScrollViewContents[i].GetComponent<RectTransform>().sizeDelta = new Vector3(Mathf.Max(GameManager.instance.attachedGangs[i].Count * 70 - 240, 0), 0, 0);
            foreach(var gang in GameManager.instance.attachedGangs[i])
            {
                var attachedGangListItemObject = Instantiate(attachedGangListItemPrefab, attachedGangScrollViewContents[i].transform);
                attachedGangListItemObject.GetComponent<AttachedGangListItem>().SetGangUnitInformation(gang);
            }
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
        itemContents.GetComponent<RectTransform>().sizeDelta = new Vector3(GameManager.instance.crookItems.Count * 90 - 690, 0, 0);
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
        ResetItemScrollView();
        itemContents.GetComponent<RectTransform>().sizeDelta = new Vector3(GameManager.instance.snakeItems.Count * 90 - 690, 0, 0);
        int index = 0;
        foreach (var item in GameManager.instance.snakeItems)
        {
            var listItemObject = Instantiate(itemPrefab, itemContents.transform);
            var snakeItemListItem = listItemObject.GetComponent<ItemListItem>();
            snakeItemListItem.SetItemInformation(index, item);
            ++index;
        }
    }

    public void ShowGangItems()
    {
        ResetItemScrollView();
        itemContents.GetComponent<RectTransform>().sizeDelta = new Vector3(GameManager.instance.gangItems.Count * 90 - 690, 0, 0);
        int index = 0;
        foreach (var item in GameManager.instance.gangItems)
        {
            var listItemObject = Instantiate(itemPrefab, itemContents.transform);
            var gangItemListItem = listItemObject.GetComponent<ItemListItem>();
            gangItemListItem.SetItemInformation(index, item);
            ++index;
        }
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
        crookBookmark.transform.position -= new Vector3((currentTab == Tabs.crook) ? bookmarkClickedYChange : 0, 0, 0);
        snakeBookmark.transform.position -= new Vector3((currentTab == Tabs.snake) ? bookmarkClickedYChange : 0, 0, 0);
        gangBookmark.transform.position -= new Vector3((currentTab == Tabs.gang) ? bookmarkClickedYChange : 0, 0, 0);

        currentTab = tab;

        crookTab.SetActive(currentTab == Tabs.crook);
        snakeTab.SetActive(currentTab == Tabs.snake);
        gangTab.SetActive(currentTab == Tabs.gang);

        crookBookmark.transform.position += new Vector3((currentTab == Tabs.crook) ? bookmarkClickedYChange : 0, 0, 0);
        snakeBookmark.transform.position += new Vector3((currentTab == Tabs.snake) ? bookmarkClickedYChange : 0, 0, 0);
        gangBookmark.transform.position += new Vector3((currentTab == Tabs.gang) ? bookmarkClickedYChange : 0, 0, 0);

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
        factories.Add(factory1);
        factories.Add(factory2);
        factories.Add(factory3);

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
