using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    [SerializeField]
    public GameObject GoBackButton;
    public void GotoFirstFloor()
    {
        if (GameManager.instance.stamina >= 3)
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.firstFloor);
            
        } 
        else { GameManager.instance.NotEnoughStamina(); }
    }
    public void GotoSecondFloor()
    {
        if (GameManager.instance.stamina >= 3)
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.secondFloor);
            
        }
        else { GameManager.instance.NotEnoughStamina(); }
    }
    public void GotoThirdFloor()
    {
        if (GameManager.instance.stamina >= 3)
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.thirdFloor);
            
        }
        else { GameManager.instance.NotEnoughStamina(); }
    }
    public void GotoFourthFloor()
    {
        if (GameManager.instance.stamina >= 3)
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.fourthFloor);
            
        }
        else { GameManager.instance.NotEnoughStamina(); }
    }
    public void GotoFifthFloor()
    {
        if (GameManager.instance.stamina >= 3)
        {
            GameManager.instance.ChangeCanvasInHouse(GameManager.Screen.fifthFloor);
            
        }
        else { GameManager.instance.NotEnoughStamina(); }
    }
}
