using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private GameManager.Job kindOfUnit;
    [SerializeField]
    private GameObject slotItemPrefab;
    [SerializeField]
    private int slotIndex;

    UnitCheckButton unitAsk;
    GameObject unitNew;
    GameObject unitPopup;
    GameObject unitPopupYesButton;

    GameObject GangItemAsk;
    GameObject GangItemCheck;

    GameObject ItemList;

    void Awake()
    {
        unitNew = GameObject.Find("Unit");
        unitPopup = unitNew.transform.Find("UnitCheck").gameObject;
        unitPopupYesButton = unitPopup.transform.Find("UnitCheck_yes").gameObject;
        unitAsk = unitPopupYesButton.GetComponent<UnitCheckButton>();
        GangItemAsk = GameObject.Find("GangItemAsk");
        GangItemCheck = GangItemAsk.transform.Find("GangItemCheck").gameObject;
        ItemList = GameObject.Find("ItemList");
    }

    public GameObject item
    {
        get
        {
            if ( transform.childCount > 1 )
            {
                return transform.GetChild(1).gameObject;
            }
            else
            {
                return null;
            }
        }
        set { }
    }

    public void OnDrop(PointerEventData eventData)
    {
        unitPopup.SetActive(true);
        unitAsk.slot = this;
    }
    public void B()
    {
        if (UnitCheckButton.showSlot == true && item == null && GameManager.instance.CanAttatchUnit(kindOfUnit, slotIndex))
        {
            Debug.Log("sibal");
            //슬롯에 Gameobject의 Child로 Prefab을 가져다 붙임
            switch (kindOfUnit)
            {
                case GameManager.Job.crook:
                    item = Instantiate(slotItemPrefab, transform);
                    break;
                case GameManager.Job.snake:
                    item = Instantiate(slotItemPrefab, transform);
                    break;
                case GameManager.Job gang:
                    item = Instantiate(slotItemPrefab, transform);
                    break;
            }

            GameManager.instance.AttatchUnit(kindOfUnit, UnitDragHandler.itemBeingDraggedIndex, slotIndex);     //데이터 상으로 유닛을 이동
            if (UnitDragHandler.unitBeingDragged != null) Destroy(UnitDragHandler.unitBeingDragged);           //드래그하던 아이콘 오브젝트 파괴

            //유닛 스크롤 뷰 갱신
            switch (kindOfUnit)
            {
                case GameManager.Job.crook:
                    UnitsManager.instance.ShowCrooks();
                    break;
                case GameManager.Job.snake:
                    UnitsManager.instance.ShowSnakes();
                    break;
                case GameManager.Job gang:
                    UnitsManager.instance.ShowGangs();
                    GangItemCheck.SetActive(true);
                    ItemList.GetComponent<Image>().color = Color.red;
                    break;
            }
            InitializeSlotObject();                         //슬롯에 유닛 정보를 띄움
            UnitCheckButton.showSlot = false;
        }
    }
    /// <summary>슬롯에 유닛 정보를 띄움</summary>
    private void InitializeSlotObject()
    {
        string statusText;
        switch (kindOfUnit)
        {
            case GameManager.Job.crook:
                GameManager.Crook currentCrook = GameManager.instance.attatchedCrooks[slotIndex];
                statusText = "Lv " + currentCrook.level;
                switch(currentCrook.type)
                {
                    case 0: statusText += " 상수형"; break;
                    case 1: statusText += " 계수형"; break;
                    case 2: statusText += " 밸런스형"; break;
                    case 3: statusText += " 호구형"; break;
                }
                statusText += " 사기꾼";
                item.transform.Find("Status").GetComponent<Text>().text = statusText;

                item.transform.Find("ItemSlot").GetComponent<ItemSlot>().unitIndex = slotIndex;

                item.transform.Find("MoneyStealText").GetComponent<Text>().text = currentCrook.GetRichConstantDown() + " + " + currentCrook.GetRichRatioDown() + "%";
                item.transform.Find("ReturnPercentageText").GetComponent<Text>().text = currentCrook.GetMoneyUp() + "%";

                item.transform.Find("RetireButton").GetComponent<RetireButton>().InitializeRetireButton(kindOfUnit, slotIndex);
                break;
            case GameManager.Job.snake:
                GameManager.Snake currentSnake = GameManager.instance.attatchedSnakes[slotIndex];
                statusText = "Lv " + currentSnake.level + " 꽃뱀";
                item.transform.Find("Status").GetComponent<Text>().text = statusText;

                item.transform.Find("ItemSlot").GetComponent<ItemSlot>().unitIndex = slotIndex;

                item.transform.Find("Attribute").GetComponent<Text>().text = "아무것도 안 하고 있음";

                item.transform.Find("RetireButton").GetComponent<RetireButton>().InitializeRetireButton(kindOfUnit, slotIndex);
                break;
            case GameManager.Job.gang:
                GameManager.Gang currentGang = GameManager.instance.attatchedGangs[slotIndex];
                statusText = "Lv " + currentGang.level + " 갱단";
                item.transform.Find("Status").GetComponent<Text>().text = statusText;

                item.transform.Find("ItemSlot").GetComponent<ItemSlot>().unitIndex = slotIndex;

                item.transform.Find("Attack").GetComponent<Text>().text = currentGang.attack().ToString();

                item.transform.Find("Attribute").GetComponent<Text>().text = "특수능력 없음";

                item.transform.Find("RetireButton").GetComponent<RetireButton>().InitializeRetireButton(kindOfUnit, slotIndex);
                break;
        }
        
    }
}
