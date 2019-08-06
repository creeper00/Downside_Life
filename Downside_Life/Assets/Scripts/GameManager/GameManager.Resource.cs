using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    //플레이어의 스탯
    [HideInInspector]
    public int playerMoney;
    [HideInInspector]
    public int playerSalary;
    [HideInInspector]
    public int stamina;

    //부자의 스탯
    public int richInitialMoney;
    private int richMoney, richSalary;
    public double richDesperate;
    [SerializeField]
    private int richMoneyBound;
    [SerializeField]
    private double richDesperateBound;

    [SerializeField]
    GameObject desperateGauge;
    [SerializeField]
    GameObject staminaGauge;
    [SerializeField]
    Text moneyText;
    [SerializeField]
    Text staminaText;

    void ResourceManage()
    {
        int lastRichMoney = richMoney;
        //richSalary = 
        richMoney += richSalary;

        richDesperate += (double)(lastRichMoney - richMoney) / lastRichMoney;

        desperateGauge.GetComponent<Transform>().localScale = new Vector3((float)richDesperate, 1, 1);

        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);
        //rich

        playerMoney += playerSalary;
        moneyText.text = playerMoney + "";
        //player



    }

    void StaminaManage(int currentStamina)
    {
        stamina = currentStamina;
        staminaGauge.GetComponent<Transform>().localScale = new Vector3((float)currentStamina / 10, 1, 1);
        staminaText.text = currentStamina + "/10";

    }
}