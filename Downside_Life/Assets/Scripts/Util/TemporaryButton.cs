using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryButton : MonoBehaviour
{
    GameObject buyInfo;
    GameManager.Crook crook;

    public void showBuyInfo()
    {
        buyInfo = StoreManager.instance.buyInfo;
        buyInfo.SetActive(true);
        StoreManager.instance.buyYes.crook = crook;
    }
    public void addCrooks()
    {
        GameManager.instance.crooks.Add(new GameManager.Crook(1, 10000));
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

    public void setUnitInformation(int index, GameManager.Crook crook)
    {
        this.crook = crook;
    }
}
