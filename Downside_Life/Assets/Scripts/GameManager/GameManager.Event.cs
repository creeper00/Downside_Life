﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    [Header("이벤트 관련 수치들")]
    //[SerializeField]
    private double robberSuccessRateDecrease;
    //[SerializeField]
    private double crookStealMoneyDecrease;
    private int numOfTotalEvents = 0;
    private int nextEventIndex;
    private double nextEventDesperate;
    private int snakeSlotDisableIndex = 2;
    [SerializeField]
    private GameObject[] snakeSlotDisableCurtain = new GameObject[3];
    [Header("이거에 영향을 받는 것들")]
    private int factoryCoolDownDecrease = 0;
    [HideInInspector]
    public double robberSuccessRateMultiply = 1f;

    List<DesperateEvent> events = new List<DesperateEvent>();

    public enum Endings
    {
        success, tooBig, dropped
    }

    public void InitializeEventSettings()
    {
        SetEvents();
        numOfTotalEvents = events.Count;
        nextEventIndex = 0;
        nextEventDesperate = events[nextEventIndex].GetEventDesperate();
    }

    private void SetEvents()                        //이벤트들을 순서대로 넣음
    {
        events.Add(new DesperateEvent(2, 20));
        events.Add(new DesperateEvent(3, 35));
        events.Add(new DesperateEvent(4, 35));
        events.Add(new DesperateEvent(2, 40));
        events.Add(new DesperateEvent(1, 60));
        events.Add(new DesperateEvent(2, 60));
        events.Add(new DesperateEvent(3, 70));
        events.Add(new DesperateEvent(4, 70));
        events.Add(new DesperateEvent(2, 80));
    }


    public void Ending(Endings ending)
    {
        switch (ending)
        {
            case Endings.success:
                //부자 돈 0으로 만들기 성공!
                break;
            case Endings.tooBig:
                //부자가 너무 성장했다
                break;
            case Endings.dropped:
                break;
        }
    }

    void EventManage()
    {
        while(nextEventIndex < numOfTotalEvents && instance.maxRichDesperate >= nextEventDesperate )    //실행해야 되는 이벤트 체크하고 실행
        {
            ExecuteEvent(events[nextEventIndex]);
            ++nextEventIndex;
            nextEventDesperate = events[nextEventIndex].GetEventDesperate();
        }

        if (richMoney <= 0) Ending(Endings.success);
        if (richMoney >= richMoneyBound) Ending(Endings.tooBig);
        if (richDesperate >= richDesperateBound) Ending(Endings.dropped);
    }

    private void ExecuteEvent(DesperateEvent currentEvent)
    {
        switch(currentEvent.GetEventType())
        {
            case 1:         //공장 수리
                ++factoryCoolDownDecrease;
                break;
            case 2:         //도둑질 확률 감소
                robberSuccessRateMultiply -= robberSuccessRateDecrease;
                break;
            case 3:         //꽃뱀 슬롯 삭제
                if ( instance.attatchedSnakes[snakeSlotDisableIndex] != null )
                {
                    instance.RetireUnit(Job.snake, snakeSlotDisableIndex);
                    ConsumeStamina(-unitRetireStaminaDecrease);
                }
                snakeSlotDisableCurtain[snakeSlotDisableIndex].SetActive(true);
                --snakeSlotDisableIndex;
                break;
            case 4:         //사기꾼에게 뜯기는 돈 감소

                break;
        }
    }

}
