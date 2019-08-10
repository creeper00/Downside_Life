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

        item.transform.Find("MoneyStealPercentageText").GetComponent<Text>().text = currentCrook.richPercentageDown + "%";
        item.transform.Find("MoneyStealConstantText").GetComponent<Text>().text = currentCrook.richConstantDown.ToString();
        item.transform.Find("ReturnPercentageText").GetComponent<Text>().text = currentCrook.playerPercentageUp + "%";

        item.transform.Find("RetireButton").GetComponent<RetireButton>().InitializeRetireButton(kindOfUnit, slotIndex);
    }
}
