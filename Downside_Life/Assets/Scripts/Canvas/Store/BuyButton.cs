using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public int index;
    public GameManager.Job job;
    public GameManager.Crook crook;
    public GameManager.Gang gang;
    public GameManager.Snake snake;
    public void addUnit()
    {
        switch(job)
        {
            case GameManager.Job.crook:
                GameManager.instance.crooks.Add(crook);
                StoreManager.instance.isCrookBuyed[index] = true;
                break;
            case GameManager.Job.snake:
                GameManager.instance.snakes.Add(snake);
                StoreManager.instance.isSnakeBuyed[index] = true;
                break;
            case GameManager.Job.gang:
                GameManager.instance.gangs.Add(gang);
                StoreManager.instance.isGangBuyed[index] = true;
                break;

        }
    }
}
