using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGang : MonoBehaviour
{
    [SerializeField]
    private int gangLevel;

    public void BuyGangster()
    {
        int cost = GameManager.instance.gangsterCost[gangLevel - 1];
        if (GameManager.instance.playerMoney >= cost)
        {
            GameManager.instance.playerMoney -= cost;
            GameManager.instance.gangsterNumber[gangLevel - 1] ++;
            GameManager.instance.gangsterStatusText.showGangsterNumber();
        }
        else
        {
            GameManager.instance.NotEnoughMoney();
        }
    }
}
