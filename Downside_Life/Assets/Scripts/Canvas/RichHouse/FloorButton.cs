using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public void GotoFirstFloor()
    {
        GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.firstFloor);
    }
    public void GotoSecondFloor()
    {
        GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.secondFloor);
    }
    public void GotoThirdFloor()
    {
        GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.thirdFloor);
    }
    public void GotoFourthFloor()
    {
        GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.fourthFloor);
    }
    public void GotoFifthFloor()
    {
        GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.fifthFloor);
    }
}
