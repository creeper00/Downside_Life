using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    public void EndTurn()
    {
        GameManager.instance.EndTurn();
    }
}
