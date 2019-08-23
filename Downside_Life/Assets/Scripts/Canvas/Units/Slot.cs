using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    [HideInInspector]
    public static int currentActiveSlotIndex = 0;
    [SerializeField]
    private GameManager.Job kindOfUnit;
    [SerializeField]
    private GameObject slotItemPrefab;
    [SerializeField]
    private int slotIndex;
    [HideInInspector]
    private Sprite crookConstant, crookRate, crookBalanced, crookIdiot; // 순서대로 상수형, 계수형, 밸런스형, 호구형
    [HideInInspector]
    private Sprite snakeDesp, snakeWaste, snakeSlow, snakeMoney; // 순서대로 둔감형, 낭비형, 둔화형, 갈취형

    UnitCheckButton unitAsk;
    GameObject unitNew;
    GameObject unitPopup;
    GameObject unitPopupYesButton;
    Text unitText;
    public int staminaUse_Unit=3;

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
        GangItemCheck = GangItemAsk.transform.GetChild(0).gameObject;
        ItemList = GameObject.Find("ItemList");
        unitText = unitPopup.GetComponentInChildren<Text>();
        unitText.text = "정말 투입하시겠습니까?\n" + "행동력 : " + staminaUse_Unit;
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
        if ( UnitDragHandler.unitBeingDragged != null )
        {
            unitPopup.SetActive(true);
            unitAsk.slot = this;
        }
    }
    public void B()
    {
        if (UnitCheckButton.showSlot == true && item == null && GameManager.instance.CanAttatchUnit(kindOfUnit, slotIndex))
        {
            //Debug.Log("sibal");
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
                    currentActiveSlotIndex = slotIndex;
                    GangItemCheck.SetActive(true);
                    ItemList.GetComponent<Image>().color = Color.red;
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
                    break;
            }
            InitializeSlotObject();                         //슬롯에 유닛 정보를 띄움
            UnitCheckButton.showSlot = false;
        }
    }
    /// <summary>슬롯에 유닛 정보를 띄움</summary>
    public void InitializeSlotObject()
    {
        string statusText;
        switch (kindOfUnit)
        {
            case GameManager.Job.crook:
                GameManager.Crook currentCrook = GameManager.instance.attatchedCrooks[slotIndex];
                statusText = "Lv " + currentCrook.level;
                /*switch(currentCrook.type)
                {
                    case 0: statusText += " 상수형"; break;
                    case 1: statusText += " 계수형"; break;
                    case 2: statusText += " 밸런스형"; break;
                    case 3: statusText += " 호구형"; break;
                }*/
                statusText += " " + currentCrook.GetType();
                item.transform.Find("CrookImage").GetComponent<Image>().sprite = currentCrook.GetIcon();
                statusText += " 사기꾼";
                item.transform.Find("Status").GetComponent<Text>().text = statusText;

                item.transform.Find("ItemSlot").GetComponent<ItemSlot>().unitIndex = slotIndex;

                item.transform.Find("MoneyStealText").GetComponent<Text>().text = currentCrook.GetRichConstantDown() + " + " + currentCrook.GetRichRatioDown() + "%";
                item.transform.Find("ReturnPercentageText").GetComponent<Text>().text = currentCrook.GetMoneyUp() + "%";

                item.transform.Find("RetireButton").GetComponent<RetireButton>().InitializeRetireButton(kindOfUnit, slotIndex);
                break;
            case GameManager.Job.snake:
                GameManager.Snake currentSnake = GameManager.instance.attatchedSnakes[slotIndex];
                statusText = "Lv " + currentSnake.level + " "+currentSnake.GetType()+" 꽃뱀";
                item.transform.Find("SnakeImage").GetComponent<Image>().sprite = currentSnake.GetIcon();
                item.transform.Find("Status").GetComponent<Text>().text = statusText;

                item.transform.Find("ItemSlot").GetComponent<ItemSlot>().unitIndex = slotIndex;

                switch(currentSnake.type)
                {
                    case 0: item.transform.Find("Attribute").GetComponent<Text>().text = "절박함 "+currentSnake.GetDesperateControl()+"% 감소"; break;
                    case 1: item.transform.Find("Attribute").GetComponent<Text>().text = "일정확률로 " + currentSnake.GetLowerBound() + " 만원~" + currentSnake.GetUpperBound() + " 만원 획득"; break;
                    case 2: item.transform.Find("Attribute").GetComponent<Text>().text = currentSnake.GetBehaviorCostIncrease()+" 억원 비용 증가"; break;
                    case 3: item.transform.Find("Attribute").GetComponent<Text>().text = currentSnake.RichCycleIncrease() + " 주기 증가"; break;
                }
                item.transform.Find("RetireButton").GetComponent<RetireButton>().InitializeRetireButton(kindOfUnit, slotIndex);
                break;
            case GameManager.Job.gang:
                UnitsManager.instance.ShowAttachedGangs();
                break;
        }
        
    }
}
