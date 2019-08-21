using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour
{
    public int index;
    public GameManager.Job job;
    public GameManager.Crook crook;
    public GameManager.Gang gang;
    public GameManager.Snake snake;
    public TemporaryButton temporary;
    public void addUnit()
    {
        switch(job)
        {
            case GameManager.Job.crook:
                Debug.Log(crook.unitPrice());
                GameManager.instance.crooks.Add(crook);
                StoreManager.instance.isCrookBuyed[index] = true;
                GameManager.instance.playerMoney -= (int)crook.unitPrice();
                GameManager.instance.UpdateResourcesUI();
                break;
            case GameManager.Job.snake:
                Debug.Log(snake.unitPrice());
                GameManager.instance.snakes.Add(snake);
                StoreManager.instance.isSnakeBuyed[index] = true;
                GameManager.instance.playerMoney -= (int)snake.unitPrice();
                GameManager.instance.UpdateResourcesUI();
                break;
            case GameManager.Job.gang:
                GameManager.instance.gangs.Add(gang);
                StoreManager.instance.isGangBuyed[index] = true;
                GameManager.instance.playerMoney -= (int)gang.unitPrice();
                GameManager.instance.UpdateResourcesUI();
                break;

        }
    }
}
