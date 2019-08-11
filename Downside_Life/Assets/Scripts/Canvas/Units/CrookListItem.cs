using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrookListItem : MonoBehaviour
{
    [HideInInspector]
    public int index;

    /// <summary>스크롤 뷰의 한 슬롯에 유닛 정보들을 표시</summary>
    public void SetUnitInformation(int index, GameManager.Crook crook)
    {
        this.index = index;

        transform.Find("Level").GetComponent<Text>().text = "Lv " + crook.level;
        transform.Find("Stat").GetComponent<Text>().text = crook.richPercentageDown + "% + " + crook.richConstantDown;
        transform.Find("Return").GetComponent<Text>().text = crook.playerPercentageUp + "% 환급";
    }
}
