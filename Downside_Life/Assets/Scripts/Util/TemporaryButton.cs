using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryButton : MonoBehaviour
{
    int index;
    bool isBuyed;
    GameObject buyInfo;
    GameManager.Job job;
    GameManager.Crook crook;
    GameManager.Snake snake;
    GameManager.Gang gang;

    public void showBuyInfo()
    {
        switch(job)
        {
            case GameManager.Job.crook:
                if (StoreManager.instance.isCrookBuyed[index])
                {
                    //이미 구매했다는 팝업창
                    StoreManager.instance.showAlreadyPurchased();
                    break;
                }
                if (crook.unitPrice() > GameManager.instance.playerMoney)
                {
                    //돈이 모자라다는 팝업창
                    StoreManager.instance.showNotEnoughMoney();
                    break;
                }
                buyInfo = StoreManager.instance.buyInfo;
                buyInfo.SetActive(true);
                StoreManager.instance.buyYes.job = GameManager.Job.crook;
                StoreManager.instance.buyYes.crook = crook;
                StoreManager.instance.buyYes.index = index;
                break;
            case GameManager.Job.snake:
                if (StoreManager.instance.isSnakeBuyed[index])
                {
                    //이미 구매했다는 팝업창
                    StoreManager.instance.showAlreadyPurchased();
                    break;
                }
                if (snake.unitPrice() > GameManager.instance.playerMoney)
                {
                    //돈이 모자라다는 팝업창
                    StoreManager.instance.showNotEnoughMoney();
                    break;
                }
                buyInfo = StoreManager.instance.buyInfo;
                buyInfo.SetActive(true);
                StoreManager.instance.buyYes.job = GameManager.Job.snake;
                StoreManager.instance.buyYes.snake = snake;
                StoreManager.instance.buyYes.index = index;
                
                break;
            case GameManager.Job.gang:
                if (StoreManager.instance.isGangBuyed[index])
                {
                    //이미 구매했다는 팝업창
                    StoreManager.instance.showAlreadyPurchased();
                    break;
                }
                if (gang.unitPrice() > GameManager.instance.playerMoney)
                {
                    //돈이 모자라다는 팝업창
                    StoreManager.instance.showNotEnoughMoney();
                    break;
                }
                buyInfo = StoreManager.instance.buyInfo;
                buyInfo.SetActive(true);
                StoreManager.instance.buyYes.job = GameManager.Job.gang;
                StoreManager.instance.buyYes.gang = gang;
                StoreManager.instance.buyYes.index = index;
                break;
        }
    }

    public void addCrooks()
    {
        GameManager.instance.crooks.Add(new GameManager.Crook(1, 0));
        if( UnitsManager.instance.currentTab == UnitsManager.Tabs.crook) UnitsManager.instance.ShowCrooks(); 
    }

    public void addSnakes()
    {
        GameManager.instance.snakes.Add(new GameManager.Snake(1, 3));
    }

    public void addGangs()
    {
        GameManager.instance.gangs.Add(new GameManager.Gang(2, 3));
    }

    public void setCrookUnitInformation(int index, GameManager.Crook crook)
    {
        job = GameManager.Job.crook;
        this.crook = crook;
        this.index = index;
        transform.Find("Level").GetComponent<Text>().text = crook.level.ToString();
        transform.Find("Price").GetComponent<Text>().text = ((int)crook.unitPrice()).ToString() + "만원";
        transform.Find("Type").GetComponent<Text>().text = crook.GetType();
        transform.Find("Character").GetComponent<Image>().sprite = crook.GetSprite();
    }
    public void setSnakeUnitInformation(int index, GameManager.Snake snake)
    {
        job = GameManager.Job.snake;
        this.snake = snake;
        this.index = index;
        transform.Find("Level").GetComponent<Text>().text = snake.level.ToString();
        transform.Find("Price").GetComponent<Text>().text = ((int)snake.unitPrice()).ToString() + "만원";
        transform.Find("Type").GetComponent<Text>().text = snake.GetType();
        transform.Find("Character").GetComponent<Image>().sprite = snake.GetSprite();
    }
    public void setGangUnitInformation(int index, GameManager.Gang gang)
    {
        job = GameManager.Job.gang;
        this.gang = gang;
        this.index = index;
        transform.Find("Level").GetComponent<Text>().text = gang.level.ToString();
        transform.Find("Price").GetComponent<Text>().text = ((int)gang.unitPrice()).ToString() + "만원";
        transform.Find("Type").GetComponent<Text>().text = gang.GetType();
        transform.Find("Character").GetComponent<Image>().sprite = gang.GetSprite();
    }
}