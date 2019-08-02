using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownWardUIButtons : MonoBehaviour
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

    public void GotoFactories()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.factories);
    }

    public void GotoRichHouse()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.richHouse);
    }


}
