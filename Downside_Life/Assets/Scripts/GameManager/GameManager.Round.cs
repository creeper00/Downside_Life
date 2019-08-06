using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public void EndTurn()
    {
        ResourceManage();
        EventManage();
        StaminaManage(stamina-1);
        
    }
}
