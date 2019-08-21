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
    public int totalSkillPoint, skillPoint;
    public int[] jobSkillPoint = new int[4];
    [HideInInspector]
    public int crookTemporarySkillPoint, robberTemporarySkillPoint, snakeTemporarySkillPoint, gangTemporarySkillPoint;
    [SerializeField]
    public int thiefSuccessPercentage, thiefGreatSuccessPercentage, stealMoney, skillPointPrice;
    [Header("꽃뱀")]
    [SerializeField]
    public int snakeItemSuccessPercentage;

    int crookIncome = 0;

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
    [SerializeField]
    private Text snakeMoneyText;
    void ResourceManage()
    {

        float crookConstantIncome = 0;
        float crookPercentageIncome = 0;
        for (int i = 0; i < attatchedCrooks.Length; i++)
        {
            if (attatchedCrooks[i] != null)
            {
                crookConstantIncome += attatchedCrooks[i].GetRichConstantDown();
                crookPercentageIncome += attatchedCrooks[i].GetRichRatioDown();
            }
        }
        crookIncome = (int)((crookConstantIncome + crookPercentageIncome * richMoney / 100));//사기꾼 터는양

        int tempRichSalary = RichSalary();

        
        richMoney += tempRichSalary;

        desperateGauge.GetComponent<Transform>().localScale = new Vector3((float)richDesperate, 1, 1);
        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);

        int tempPlayerSalary = PlayerSalary();
        playerMoney += tempPlayerSalary;


        UpdateResourcesUI();
    }
    // 아래 SnakeIncome 함수 쓰기 위한 코드.
    private IEnumerator showSnakeStealSuccessWindow()
    {
        snakeSteallSuccessCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        snakeSteallSuccessCanvas.SetActive(false);
    }

    public void ChangeRichMoney(int moneyDecrease, bool isIncreaseDesperate)
    {
        double desperate = 0;
        desperate = richMoney > 10000000 ? 0.5 * (moneyDecrease) / richMoney : 1.5 * (moneyDecrease) / richMoney;
        ChangeDesperate(desperate);
        richMoney -= moneyDecrease;
        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);
    }

    ///<summary>부자의 절박함 수치를 증가시킴 / 입력값은 여러 보정들을 거치기 전</summary>
    private void ChangeDesperate(double desperateIncrease)
    {
        //꽃뱀의 감소
        for ( int i = 0; i < GameManager.instance.snakes.Count; ++i )
        {
            desperateIncrease *= ((100 - snakes[i].GetDesperateControl()) / 100);
        }
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
        int factoryIncome = 0;
        for (int i=0; i<factories.Length; i++)
        {
            if (factories[i] != null)
            {
                factoryIncome += (int)factories[i].CalculateIncome();//공장 수입
            }
        }
        ChangeDesperate((double)(-crookIncome) / richMoney);
        return factoryIncome - crookIncome;
    }

    int PlayerSalary()
    {
        float ratio = 0;
        for (int i=0; i<attatchedCrooks.Length; i++)
        {
            if (attatchedCrooks[i] != null)
            {
                ratio += attatchedCrooks[i].GetMoneyUp();
            }
        }

        int snakeStealMoney = 0;
        for (int i = 0; i < attatchedSnakes.Length; i++)
        {
            if (attatchedSnakes[i] != null)
            {
                if (Random.Range(0, 100) < snakeItemSuccessPercentage) snakeStealMoney += (int)attatchedSnakes[i].GetItemPrice();
            }
        }
        if (snakeStealMoney != 0)
        {
            // 창 1초간 띄우기
            snakeMoneyText.text = "꽃뱀이 " + snakeStealMoney.ToString() + "만원을 부자에게서 얻었습니다!";
            StartCoroutine(showSnakeStealSuccessWindow());
            playerMoney += snakeStealMoney;
            UpdateResourcesUI();
        }
        return (int)(crookIncome * ratio);
    }
}
