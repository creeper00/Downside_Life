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

        //등급 적는 코드 작성
    }
}
