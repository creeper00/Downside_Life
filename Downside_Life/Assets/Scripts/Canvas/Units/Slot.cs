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
    private GameObject slotPrefab;
    [SerializeField]
    private int slotIndex;

    public GameObject item
    {
        get
        {
            if ( transform.childCount > 0 )
            {
                return transform.GetChild(0).gameObject;
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
        if ( UnitDragHandler.unitBeingDragged != null && item == null && GameManager.instance.CanAttatchUnit(kindOfUnit, slotIndex))
        {
            item = Instantiate(slotPrefab, transform);                  //슬롯에 Gameobject의 Child로 Prefab을 가져다 붙임

            GameManager.instance.AttatchUnit(kindOfUnit, UnitDragHandler.itemBeingDraggedIndex, slotIndex);     //데이터 상으로 유닛을 이동
            if (UnitDragHandler.unitBeingDragged != null ) Destroy(UnitDragHandler.unitBeingDragged);           //드래그하던 아이콘 오브젝트 파괴
            
            switch(kindOfUnit)
            {
                case GameManager.Job.crook:
                    UnitsManager.instance.showCrooks();
                    break;
                case GameManager.Job.snake:
                    UnitsManager.instance.showSnakes();
                    break;
                case GameManager.Job gang:
                    UnitsManager.instance.showGangs();
                    break;
            }
            InitializeSlotObject();                         //슬롯에 유닛 정보를 띄움
        }
    }

    /// <summary>슬롯에 유닛 정보를 띄움</summary>
    private void InitializeSlotObject()
    {
        switch(kindOfUnit)
        {
            case GameManager.Job.crook:
                GameManager.Crook currentCrook = GameManager.instance.attatchedCrooks[slotIndex];
                item.transform.Find("Status").GetComponent<Text>().text = "Lv " + currentCrook.level + "사기꾼";

                item.transform.Find("MoneyStealText").GetComponent<Text>().text = currentCrook.richConstantDown + " + " + currentCrook.richPercentageDown + "%";
                item.transform.Find("ReturnPercentageText").GetComponent<Text>().text = currentCrook.playerPercentageUp + "%";

                item.transform.Find("RetireButton").GetComponent<RetireButton>().InitializeRetireButton(kindOfUnit, slotIndex);
                break;
            case GameManager.Job.snake:
                break;
            case GameManager.Job.gang:
                break;
        }
        
    }
}
