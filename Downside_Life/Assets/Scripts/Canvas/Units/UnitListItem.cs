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

        string statusText = "Lv " + snake.level;
        switch (snake.type)
        {
            case 0: statusText += " 둔감형"; break;
            case 1: statusText += " 낭비형"; break;
            case 2: statusText += " 둔화형"; break;
            case 3: statusText += " 갈취형"; break;
        }
        statusText += " 꽃뱀";
        transform.Find("Status").GetComponent<Text>().text = statusText;

        string attributeText = "";
        switch (snake.type)
        {
            case 0: attributeText += "부자의 절박함 증가량 " + snake.snakeDesperateDown + "% 만큼 감소"; break;
            case 1: attributeText += "부자의 행동 비용 " + snake.snakeActionCostIncrease + "억 원 증가"; break;
            case 2: attributeText += "부자의 행동 주기 " + snake.snakeActionTurnIncrease + "턴 증가"; break;
            case 3: attributeText += snake.snakeExtortProbability + "%의 확률로 환금형 아이템 획득"; break;
        }

        transform.Find("Attribute").GetComponent<Text>().text = attributeText;
    }

    public void SetUnitInformation(int index, GameManager.Gang gang)
    {
        this.index = index;

        string statusText = "Lv " + gang.level;
        switch (gang.type)
        {
            case 0: statusText += " 딜러형"; break;
            case 1: statusText += " 수금형"; break;
            case 2: statusText += " 도벽형"; break;
            case 3: statusText += " 광역형"; break;
        }
        statusText += " 갱단";
        transform.Find("Status").GetComponent<Text>().text = statusText;
        transform.Find("Attack").GetComponent<Text>().text = gang.attack.ToString();
        transform.Find("Return").GetComponent<Text>().text = gang.returnMoney.ToString();
    }
}
