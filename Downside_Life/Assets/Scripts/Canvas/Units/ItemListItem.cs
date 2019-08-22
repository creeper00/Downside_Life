using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListItem : MonoBehaviour
{
    [HideInInspector]
    public int index;
    public int type;
    public int grade;
    public int itemCode;

    private GameObject GangItemAsk;
    private GameObject GangItemCheck;

    public void SetItemInformation(int index, Item item)
    {
        GangItemAsk = GameObject.Find("GangItemAsk");
        GangItemCheck = GangItemAsk.transform.GetChild(0).gameObject;

        this.index = index;
        type = item.type;
        grade = item.grade;
        itemCode = item.itemCode;
        transform.Find("Icon").GetComponent<Image>().sprite = item.getIcon();

        if(item.grade == 0)
        {
            gameObject.transform.Find("Grade").GetComponent<Text>().text = "Normal";
            gameObject.transform.Find("Grade").GetComponent<Text>().color = new Color32(128,128,128, 255);
        }
        else if(item.grade == 1)
        {
            gameObject.transform.Find("Grade").GetComponent<Text>().text = "Rare";
            gameObject.transform.Find("Grade").GetComponent<Text>().color = new Color32(0, 0, 205, 255);
        }
        else if(item.grade == 2)
        {
            gameObject.transform.Find("Grade").GetComponent<Text>().text = "Legendary";
            gameObject.transform.Find("Grade").GetComponent<Text>().color = new Color32(127, 255, 0, 255);
        }
    }

    public void ClickForGangItem()
    {
        if ( GangItemCheck.activeSelf)
        {
            var pointer = GameManager.instance.attachedGangs[Slot.currentActiveSlotIndex];
            pointer[pointer.Count - 1].PutItem(index, Slot.currentActiveSlotIndex);
            GameManager.instance.gangItems.RemoveAt(index);
            GameObject.Find("Popups").transform.Find("UnitExplanationCanvas").gameObject.SetActive(false);
            UnitsManager.instance.ShowGangItems();
            GangItemCheck.SetActive(false);
        }
    }

}
