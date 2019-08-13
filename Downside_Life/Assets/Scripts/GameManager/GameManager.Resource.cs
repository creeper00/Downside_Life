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
    private int fasterSetupFactory;
    private int factoryLevelUpCooldown;

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
        //richSalary = 
        int tempRichSalary = richSalaryIncrease(richSalary);

        ChangeDesperate( (double)(-tempRichSalary) / richMoney );
        richMoney += tempRichSalary;

        desperateGauge.GetComponent<Transform>().localScale = new Vector3((float)richDesperate, 1, 1);
        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);
        //rich

        playerMoney += playerSalary;
        //player
        UpdateResourcesUI();
    }

    public void ChangeRichMoney(int moneyDecrease)
    {
        int lastRichMoney = richMoney;
        richMoney -= moneyDecrease;
        ChangeDesperate( (double)(moneyDecrease) / lastRichMoney );
        EventManage();
    }

    private void ChangeDesperate(double desperateIncrease)
    {
        richDesperate += desperateIncrease;
        EventManage();

    }

    ///<summary>UI에 표시된 내 재산과 스태미나를 업데이트</summary>
    public void UpdateResourcesUI()
    {
        UpdateStaminaGauge();
        UpdateMoneyText();
    }

    ///<summary>UI에 표시된 내 스태미나를 업데이트</summary>
    public void UpdateStaminaGauge()
    {
        staminaGauge.GetComponent<Transform>().localScale = new Vector3((float)stamina / 10, 1, 1);
        staminaText.text = stamina + "/10";
    }

    ///<summary>UI에 표시된 내 재산을 업데이트</summary>
    public void UpdateMoneyText()
    {
        moneyText.text = playerMoney + "";
    }

    ///<summary>스태미나 값을 변경하고 UI를 업데이트</summary>
    public void SetStamina(int currentStamina)
    {
        stamina = currentStamina;
        UpdateStaminaGauge();
    }

    ///<summary>스태미나를 minusStamina만큼 소모할 수 있으면 true를 반환, 아니면 부족하다는 창을 띄우고 false를 반환</summary>
    public bool CheckStamina(int minusStamina)
    {
        if (minusStamina <= stamina)
        {
            return true;
        }
        else
        {
            NotEnoughStamina(minusStamina);
            return false;
        }
    }

    ///<summary>스태미나를 minusStamina만큼 소모할 수 있으면 소모하고 아니면 부족하다는 창을 띄움</summary>
    public void ConsumeStamina(int minusStamina)
    {
        bool canConsume = CheckStamina(minusStamina);
        if (canConsume) SetStamina(stamina - minusStamina);
    }

    ///<summary>스태미나가 부족하다는 창을 띄움</summary>
    private void NotEnoughStamina(int requireStamina)
    {
        StartCoroutine(ShowNotEnoughStaminaWindow(requireStamina));
    }

    private IEnumerator ShowNotEnoughStaminaWindow(int requireStamina)
    {
        notEnoughStaminaCanvas.SetActive(true);
        notEnoughStaminaCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "행동력이 부족합니다!\n필요 행동력: " + requireStamina;
        yield return new WaitForSeconds(1f);
        notEnoughStaminaCanvas.SetActive(false);
    }
    int FactoryCooldown()
    {
        if (richDesperate > fasterSetupFactory)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
    int richSalaryIncrease(int richSalary)
    {
        float ratio = 1;
        for (int i = 0; i < factories.Count; i++)
        {
            if (factories[i].factoryType == Factory.FactoryType.bank)
            {
                ratio += factories[i].Calculate();
            }
        }

        return (int)(richSalary * ratio);
    }
}