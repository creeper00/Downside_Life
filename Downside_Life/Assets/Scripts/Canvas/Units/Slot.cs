using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
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
        Debug.Log("detected drop");
        if ( UnitDragHandler.unitBeingDragged != null && item == null )
        {
            item = Instantiate(slotPrefab, transform);
            item.transform.SetParent(transform);
            GameManager.instance.moveCrookToSlot(UnitDragHandler.itemBeingDraggedIndex, slotIndex);
            if (UnitDragHandler.unitBeingDragged != null ) GameObject.Destroy(UnitDragHandler.unitBeingDragged);
            UnitsManager.instance.showCrooks();
            InitializeSlotObject();
        }
    }

    private void InitializeSlotObject()
    {
        GameManager.Crook currentCrook = GameManager.instance.attatchedCrooks[slotIndex];
        item.transform.Find("Level").GetComponent<Text>().text = "Lv " + currentCrook.level + "사기꾼";

        item.transform.Find("MoneySteal").GetComponent<Text>().text = currentCrook.richPercentageDown + "% + " + currentCrook.richConstantDown + " 공격";
        item.transform.Find("ReturnPercentage").GetComponent<Text>().text = currentCrook.playerPercentageUp + "% 환급";
        item.transform.Find("Ability").GetComponent<Text>().text = "특수능력 없음";
    }
}
