using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListItem : MonoBehaviour
{
    [HideInInspector]
    public int index;

    /// <summary>스크롤 뷰의 한 슬롯에 유닛 정보들을 표시</summary>
    public void SetUnitInformation(int index, GameManager.Crook crook)
    {
        this.index = index;

        string statusText = "Lv " + crook.level;
        statusText += crook.GetType() + " 사기꾼";
        transform.Find("Status").GetComponent<Text>().text = statusText;
        transform.Find("Attack").GetComponent<Text>().text = crook.GetRichConstantDown() + " + " + crook.GetRichRatioDown() + "%";
        transform.Find("Return").GetComponent<Text>().text = crook.GetMoneyUp() + "%";
    }

    public void SetUnitInformation(int index, GameManager.Snake snake)
    {
        this.index = index;

        string statusText = "Lv " + snake.level;
        statusText += snake.GetType() + " 꽃뱀";
        transform.Find("Status").GetComponent<Text>().text = statusText;

        string attributeText = "";
        switch (snake.type)
        {
            case 0: attributeText += "부자의 절박함 증가량 " + snake.GetDesperateControl() + "% 만큼 감소"; break;
            case 1: attributeText += snake.GetLowerBound() + "만원 ~ " + snake.GetUpperBound() + "만원의 환금형 아이템 획득"; break;
            case 2: attributeText += "부자의 행동 비용 " + snake.GetBehaviorCostIncrease() + "억 원 증가"; break;
            case 3: attributeText += "부자의 행동 주기 " + snake.RichCycleIncrease() + "턴 증가"; break;
            
        }

        transform.Find("Attribute").GetComponent<Text>().text = attributeText;
    }

    public void SetUnitInformation(int index, GameManager.Gang gang)
    {
        this.index = index;

        string statusText = "Lv " + gang.level;
        statusText += gang.GetType() + " 갱단";
        transform.Find("Status").GetComponent<Text>().text = statusText;
        transform.Find("Attack").GetComponent<Text>().text = gang.attack().ToString();
        transform.Find("Return").GetComponent<Text>().text = gang.returnMoney().ToString();
    }
}
