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
                for (int i = 0; i < GameManager.instance.crookStoreSellingNumber; i++)
                {
                    Debug.Log(i + " crook " + StoreManager.instance.isCrookBuyed[i]);
                }
                break;
            case GameManager.Job.snake:
                GameManager.instance.snakes.Add(snake);
                StoreManager.instance.isSnakeBuyed[index] = true;
                for (int i = 0; i < GameManager.instance.snakeStoreSellingNumber; i++)
                {
                    Debug.Log(i + " snake " + StoreManager.instance.isSnakeBuyed[i]);
                }
                break;
            case GameManager.Job.gang:
                GameManager.instance.gangs.Add(gang);
                StoreManager.instance.isGangBuyed[index] = true;
                for (int i = 0; i < GameManager.instance.gangStoreSellingNumber; i++)
                {
                    Debug.Log(i + " gang " + StoreManager.instance.isGangBuyed[i]);
                }
                break;

        }
    }
}
