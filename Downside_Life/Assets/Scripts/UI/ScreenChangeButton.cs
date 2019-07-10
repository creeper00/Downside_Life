using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChangeButton : MonoBehaviour
{
    [SerializeField]
    public GameManager.Screens screen;

    public void ChangeScreen()
    {
        GameManager.instance.GotoScreen(screen);
    }
}
