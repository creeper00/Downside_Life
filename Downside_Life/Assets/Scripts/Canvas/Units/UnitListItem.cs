using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListItem : MonoBehaviour
{
    [HideInInspector]
    public int index;
    public GameManager.Job job;

    /// <summary>스크롤 뷰의 한 슬롯에 유닛 정보들을 표시</summary>
    public void SetUnitInformation(int index, GameManager.Crook crook)
    {
        this.index = index;

        string statusText = "Lv " + crook.level;
        switch (crook.type)
        {
            case 0: statusText += " 상수형"; break;
            case 1: statusText += " 계수형"; break;
            case 2: statusText += " 밸런스형"; break;
            case 3: statusText += " 호구형"; break;
        }
        statusText += " 사기꾼";
        transform.Find("Level").GetComponent<Text>().text = statusText;
        transform.Find("Stat").GetComponent<Text>().text = crook.richConstantDown + " + " + crook.richPercentageDown + "%";
        transform.Find("Return").GetComponent<Text>().text = crook.playerPercentageUp + "%";
    }

    public void SetUnitInformation(int index, GameManager.Snake snake)
    {
        this.index = index;

        transform.Find("Attribute").GetComponent<Text>().text = "아무것도 안하지";
    }

    public void SetUnitInformation(int index, GameManager.Gang gang)
    {
        this.index = index;

        transform.Find("Attack").GetComponent<Text>().text = "400";
    }
}
