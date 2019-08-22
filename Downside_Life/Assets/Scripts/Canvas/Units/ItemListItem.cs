using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListItem : MonoBehaviour
{
    [HideInInspector]
    public int index;

    public void SetItemInformation(int index, Item item)
    {
        this.index = index;
        transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

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
        //등급 적는 코드 작성
    }
}
