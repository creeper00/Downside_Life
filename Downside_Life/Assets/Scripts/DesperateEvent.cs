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

    public DesperateEvent(int eventType, int eventDesperate)
    {
        this.eventType = eventType;
        this.eventDesperate = eventDesperate;
    }

}
