using System.Collections;
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
    private int nextEventIndex;

    List<DesperateEvent> events = new List<DesperateEvent>();

    public enum Endings
    {
        success, tooBig, dropped
    }

    public void InitializeEventSettings()
    {
        SetEvents();

        nextEventIndex = 0;

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
        
        if (richMoney <= 0) Ending(Endings.success);
        if (richMoney >= richMoneyBound) Ending(Endings.tooBig);
        if (richDesperate >= richDesperateBound) Ending(Endings.dropped);
    }

    private void ExecuteEvent()
    {

    }

}
