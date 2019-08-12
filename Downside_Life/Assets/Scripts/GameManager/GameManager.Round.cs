using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public void EndTurn()
    {
        ResourceManage();
        EventManage();
        SetStamina((stamina>2) ? 10 : stamina + 8) ;
        crookReroll();
        gangReroll();
        snakeReroll();
    }
}