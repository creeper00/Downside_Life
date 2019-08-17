using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreenButtons : MonoBehaviour
{
    public void GotoMain()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.main);
    }

    public void GotoTechTree()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.techtree);
    }

    public void GotoUnits()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.units);
    }

    public void GotoStore()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.store);
    }

    public void GotoRichHouse()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.richHouse);
    }


}
