using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndButton : MonoBehaviour
{
    public void OnClickTurnEndButton()
    {
        GameManager.instance.ChangePhase();
    }
}
