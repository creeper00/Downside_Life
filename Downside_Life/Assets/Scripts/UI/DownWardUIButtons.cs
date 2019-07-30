using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownWardUIButtons : MonoBehaviour
{
    public void GotoFactories()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.factories);
    }

    public void GotoRichHouse()
    {
        GameManager.instance.ChangeScreen(GameManager.Screen.richHouse);
    }


}
