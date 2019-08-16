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
    [HideInInspector]
    public int skillPoint;
    [SerializeField]
    public int thiefSuccessPercentage, thiefGreatSuccessPercentage, stealMoney;
    

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
    private int factoryCoolDown;
    bool isFactoryFix;
    int isAddBangBum;
    int isAddDropSnake;
    int isAddDropCrook;

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
        int tempRichSalary = RichSalary();

        
        richMoney += tempRichSalary;

        desperateGauge.GetComponent<Transform>().localScale = new Vector3((float)richDesperate, 1, 1);
        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);
        //rich

        int tempPlayerSalary = PlayerSalary();
        playerMoney += tempPlayerSalary;
        //player
        UpdateResourcesUI();
    }

    public void ChangeRichMoney(int moneyDecrease, bool isIncreaseDesperate)
    {
        double desperate = 0;
        desperate = richMoney > 10000000 ? 0.5 * (moneyDecrease) / richMoney : 1.5 * (moneyDecrease) / richMoney;
        ChangeDesperate(desperate);
        richMoney -= moneyDecrease;
        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);
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
    int RichSalary()
    {
        int income = 0;
        int factoryIncome = 0;
        int crookIncome = 0;
        float crookRatio = 1;
        for (int i=0; i<factories.Count; i++)
        {
            factoryIncome += (int)factories[i].CalculateIncome();//공장 수입
            crookRatio -= (factories[i].factoryType == Factory.FactoryType.lawyer) ? factories[i].Calculate() : 0;//사기꾼 너프
        }

        int crookConstantIncome = 0;
        float crookPercentageIncome = 0;
        for (int i=0; i<attatchedCrooks.Length; i++)
        {
            if (attatchedCrooks[i] != null)
            {
                crookConstantIncome += attatchedCrooks[i].richConstantDown;
                crookPercentageIncome += attatchedCrooks[i].richPercentageDown;
            }
        }
        ChangeDesperate((double)(-crookIncome) / richMoney);
        crookIncome = (int) ((crookConstantIncome + crookPercentageIncome * richMoney ) * crookRatio);//사기꾼 터는양
        return factoryIncome - crookIncome;
    }

    int PlayerSalary()
    {
        float crookRatio = 0;
        for (int i = 0; i < factories.Count; i++)
        {
            crookRatio -= (factories[i].factoryType == Factory.FactoryType.lawyer) ? factories[i].Calculate() : 0;//사기꾼 너프
        }
        int crookIncome = 0;
        for (int i=0; i<attatchedCrooks.Length; i++)
        {
            int constant = 0; float percentage = 0;
            if (attatchedCrooks[i] != null)
            {
                constant += attatchedCrooks[i].richConstantDown;
                percentage += attatchedCrooks[i].richPercentageDown;
                crookIncome += (int)((constant + richMoney * percentage) * crookRatio * attatchedCrooks[i].playerPercentageUp);
            }
        }
        //사기꾼이 가져오는 돈

        return crookIncome;
    }
}
