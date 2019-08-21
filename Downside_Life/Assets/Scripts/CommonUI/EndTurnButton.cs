using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    public static EndTurnButton instance;
    private void Awake()
    {
        instance = this;
    }
    public void EndTurn()
    {
        GameManager.instance.EndTurn();
        GameManager.instance.ChangeScreen(GameManager.Screen.main);
    }
}
