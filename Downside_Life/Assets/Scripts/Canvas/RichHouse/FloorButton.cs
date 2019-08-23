using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    [SerializeField]
    public GameObject GoBackButton;
    public void GotoFirstFloor()
    {
        if (GameManager.instance.CheckStamina(GameManager.instance.StealStaminaDecrease))
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.firstFloor);
            
        }
    }
    public void GotoSecondFloor()
    {
        if (GameManager.instance.CheckStamina(GameManager.instance.StealStaminaDecrease))
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.secondFloor);
            
        }
    }
    public void GotoThirdFloor()
    {
        if (GameManager.instance.CheckStamina(GameManager.instance.StealStaminaDecrease))
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.thirdFloor);
            
        }
    }
    public void GotoFourthFloor()
    {
        if (GameManager.instance.CheckStamina(GameManager.instance.StealStaminaDecrease))
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.fourthFloor);
            
        }
    }
    public void GotoFifthFloor()
    {
        if (GameManager.instance.CheckStamina(GameManager.instance.StealStaminaDecrease))
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.fifthFloor);
            
        }
    }
}
