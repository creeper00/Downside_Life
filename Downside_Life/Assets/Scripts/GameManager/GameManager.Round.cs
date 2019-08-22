using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public void EndTurn()
    {
        ResourceManage();
        FactoryAttack();
        FactoryBehavior();
        EventManage();
        SetStamina((stamina > 2) ? 10 : stamina + 8);
        CrookReroll(false);
        GangReroll(false);
        SnakeReroll(false);
        ShowMainScreenUnits();
        Debug.Log("richDesperate : " + richDesperate);
    }
}