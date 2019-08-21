﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListItem : MonoBehaviour
{
    [HideInInspector]
    public int index;
    [HideInInspector]
    private Sprite crookConstant, crookRate, crookBalanced, crookIdiot; // 순서대로 상수형, 계수형, 밸런스형, 호구형
    [HideInInspector]
    private Sprite snakeDesp, snakeWaste, snakeSlow, snakeMoney; // 순서대로 둔감형, 낭비형, 둔화형, 갈취형

    /// <summary>스크롤 뷰의 한 슬롯에 유닛 정보들을 표시</summary>
    public void SetUnitInformation(int index, GameManager.Crook crook)
    {
        this.index = index;

        string statusText = "Lv " + crook.level;
        Sprite sprite = null;
        Image icon = transform.Find("Icon").GetComponent<Image>();
        switch (crook.type)
        {
            case 0: statusText += " 상수형"; break;
            case 1: statusText += " 계수형"; break;
            case 2: statusText += " 밸런스형"; break;
            case 3: statusText += " 호구형"; break;
        }
        switch (crook.type)
        {
            case 0: sprite = crookConstant; break;
            case 1: sprite = crookRate; break;
            case 2: sprite = crookBalanced; break;
            case 3: sprite = crookIdiot; break;
        }
        statusText += " 사기꾼";
        transform.Find("Status").GetComponent<Text>().text = statusText;
        transform.Find("Attack").GetComponent<Text>().text = crook.GetRichConstantDown() + " + " + crook.GetRichRatioDown() + "%";
        transform.Find("Return").GetComponent<Text>().text = crook.GetMoneyUp() + "%";
        if(sprite != null)
        {
            icon.sprite = sprite;
        }
    }

    public void SetUnitInformation(int index, GameManager.Snake snake)
    {
        this.index = index;

        string statusText = "Lv " + snake.level;
        Sprite sprite = null;
        Image icon = transform.Find("Icon").GetComponent<Image>();
        switch (snake.type)
        {
            case 0: statusText += " 둔감형"; break;
            case 1: statusText += " 갈취형"; break;
            case 2: statusText += " 낭비형"; break;
            case 3: statusText += " 둔화형"; break;
            
        }
        switch (snake.type)
        {
            case 0: sprite = snakeDesp; break;
            case 1: sprite = snakeMoney; break;
            case 2: sprite = snakeWaste; break;
            case 3: sprite = snakeSlow; break;

        }
        statusText += " 꽃뱀";
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
        if(sprite != null)
        {
            icon.sprite = sprite;
        }
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
