using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public void GotoFirstFloor()
    {
        if(GameManager.instance.stamina >= 3) GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.firstFloor);
        else { GameManager.instance.notEnoughStaminaPanel.SetActive(true); }
    }
    public void GotoSecondFloor()
    {
        if (GameManager.instance.stamina >= 3) GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.secondFloor);
        else { GameManager.instance.notEnoughStaminaPanel.SetActive(true); }
    }
    public void GotoThirdFloor()
    {
        if (GameManager.instance.stamina >= 3) GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.thirdFloor);
        else { GameManager.instance.notEnoughStaminaPanel.SetActive(true); }
    }
    public void GotoFourthFloor()
    {
        if (GameManager.instance.stamina >= 3) GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.fourthFloor);
        else { GameManager.instance.notEnoughStaminaPanel.SetActive(true); }
    }
    public void GotoFifthFloor()
    {
        if (GameManager.instance.stamina >= 3) GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.fifthFloor);
        else { GameManager.instance.notEnoughStaminaPanel.SetActive(true); }
    }
}
