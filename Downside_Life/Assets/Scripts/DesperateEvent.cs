using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesperateEvent
{
    //이벤트의 유형
    //1- 공장 수리 주기 감소, 2- 도둑질 확률 감소, 3-꽃뱀 슬롯 삭제, 4-사기꾼이 뜯는 돈 감소
    private int eventType;
    //이 이벤트가 발동하는 절박함 값
    private double eventDesperate;

    public int GetEventType()
    {
        return eventType;
    }

    public double GetEventDesperate()
    {
        return eventDesperate;
    }

    public string GetEventExplanation()
    {
        string ret = "";
        switch(eventType)
        {
            case 1:
                ret = "부자가 공장 관리에 더 집중할 것 같다.";
                break;
            case 2:
                ret = "부자가 자기 집의 관리를 강화할 것 같다.";
                break;
            case 3:
                ret = "부자가 꽃뱀의 행동을 눈치챈 듯하다.";
                break;
            case 4:
                ret = "부자가 사기꾼들을 경계하는 듯하다.";
                break;
        }
        return ret;
    }

    public DesperateEvent(int eventType, int eventDesperate)
    {
        this.eventType = eventType;
        this.eventDesperate = eventDesperate;
    }

}
