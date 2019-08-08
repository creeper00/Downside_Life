using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    [Header("플레이어의 스탯")]
    [HideInInspector]
    public int playerMoney;
    [HideInInspector]
    public int playerSalary;
    [HideInInspector]
    public int stamina;

    [Header("부자의 스탯")]
    public int richInitialMoney;
    private int richMoney, richSalary;
    public double richDesperate;
    [SerializeField]
    private int richMoneyBound;
    [SerializeField]
    private double richDesperateBound;

    [SerializeField]
    private GameObject desperateGauge;
    [SerializeField]
    private GameObject staminaGauge;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private Text staminaText;

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
        //player
        showResources();
    }

    void StaminaManage(int currentStamina)
    {
        stamina = currentStamina;
        showResources();
    }

    public void showResources()
    {
        staminaGauge.GetComponent<Transform>().localScale = new Vector3((float)stamina / 10, 1, 1);
        staminaText.text = stamina + "/10";
        moneyText.text = playerMoney + "";
    }
}