using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefActionButton : MonoBehaviour
{
    [HideInInspector]
    public int money;
    [HideInInspector]
    public int percentage;

    public void doThief()
    {
        if(GameManager.instance.stamina < 3)
        {
            return;
        }
        GameManager.instance.stamina -= 3;
        if (Random.Range(0, 100) < percentage)
        {
            GameManager.instance.playerMoney += money;
        }
        GameManager.instance.showResources();
        GameObject.Find("ThiefInfo").SetActive(false);
    }
}
