﻿using System.Collections;
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
                if (!StoreManager.instance.isCrookBuyed[index])
                {
                    buyInfo = StoreManager.instance.buyInfo;
                    buyInfo.SetActive(true);
                    StoreManager.instance.buyYes.job = GameManager.Job.crook;
                    StoreManager.instance.buyYes.crook = crook;
                    StoreManager.instance.buyYes.index = index;
                }
                break;
            case GameManager.Job.snake:
                if (!StoreManager.instance.isSnakeBuyed[index])
                {
                    buyInfo = StoreManager.instance.buyInfo;
                    buyInfo.SetActive(true);
                    StoreManager.instance.buyYes.job = GameManager.Job.snake;
                    StoreManager.instance.buyYes.snake = snake;
                    StoreManager.instance.buyYes.index = index;
                }
                break;
            case GameManager.Job.gang:
                if (!StoreManager.instance.isGangBuyed[index])
                {
                    buyInfo = StoreManager.instance.buyInfo;
                    buyInfo.SetActive(true);
                    StoreManager.instance.buyYes.job = GameManager.Job.gang;
                    StoreManager.instance.buyYes.gang = gang;
                    StoreManager.instance.buyYes.index = index;
                }
                break;
        }
    }

    public void addCrooks()
    {
        GameManager.instance.crooks.Add(new GameManager.Crook(1, 0));
        UnitsManager.instance.showCrooks();
    }

    public void addSnakes()
    {
        GameManager.instance.snakes.Add(new GameManager.Snake(1, 1));
    }

    public void addGangs()
    {
        GameManager.instance.gangs.Add(new GameManager.Gang(1, 1));
    }

    public void setCrookUnitInformation(int index, GameManager.Crook crook)
    {
        job = GameManager.Job.crook;
        this.crook = crook;
        this.index = index;
        transform.Find("Level").GetComponent<Text>().text = "Lv : " + crook.level;
    }
    public void setSnakeUnitInformation(int index, GameManager.Snake snake)
    {
        job = GameManager.Job.snake;
        this.snake = snake;
        this.index = index;
        transform.Find("Level").GetComponent<Text>().text = "Lv : " + snake.level;
    }
    public void setGangUnitInformation(int index, GameManager.Gang gang)
    {
        job = GameManager.Job.gang;
        this.gang = gang;
        this.index = index;
        transform.Find("Level").GetComponent<Text>().text = "Lv : " + gang.level;
    }
}