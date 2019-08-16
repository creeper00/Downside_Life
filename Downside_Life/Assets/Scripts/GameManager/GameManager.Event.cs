using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    Text eventText;

    [Header("이벤트 시작 수치")]
    [SerializeField]
    int addFactoryFix;
    [SerializeField]
    List<int> addBangBum, addDropSnake, addDropCrook;

    public enum Endings
    {
        success, tooBig, dropped
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
        if (richDesperate > addFactoryFix)
        {
            isFactoryFix = true;
        }
        if (isAddBangBum < addBangBum.Count && richDesperate > addBangBum[isAddBangBum])
        {
            isAddBangBum++;
        }
        if (isAddDropSnake < addDropSnake.Count && richDesperate > addDropSnake[isAddDropSnake])
        {
            isAddDropSnake++;
            //꽃뱀떨구기
        }
        if (isAddDropCrook < addDropCrook.Count && richDesperate > addDropSnake[isAddDropCrook])
        {
            isAddDropCrook++;
            //사기꾼떨구기
        }
        if (richMoney <= 0) Ending(Endings.success);
        if (richMoney >= richMoneyBound) Ending(Endings.tooBig);
        if (richDesperate >= richDesperateBound) Ending(Endings.dropped);
    }
}
