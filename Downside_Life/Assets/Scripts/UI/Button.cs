using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    public GameManager.Screens screen;
    [SerializeField]
    public bool isEndTurnButton;

    void OnMouseDown()
    {
        if (isEndTurnButton)
        {
            GameManager.instance.ChangePhase();
            return;
        }
        GameManager.instance.GotoScreen(screen);
        Debug.Log("boo");
    }
}
