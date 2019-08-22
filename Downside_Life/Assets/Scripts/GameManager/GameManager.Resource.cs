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
    public int thiefSuccessPercentage, thiefGreatSuccessPercentage, skillPointPrice;
    [Header("꽃뱀")]
    [SerializeField]
    public int snakeItemSuccessPercentage;
    [SerializeField]
    GameObject DespGauge;

    int crookIncome = 0;
    int snakeCost = 0;
    int factoryRichIncome = 0, factoryPlayerIncome = 0;

    [Header("부자의 스탯")]
    public int richInitialMoney;
    public int richMoney, richSalary;
    public double richDesperate;
    private double maxRichDesperate;                //지금까지 있었던 절박함의 최댓값
    [SerializeField]
    private int richMoneyBound;
    [SerializeField]
    private double richDesperateBound;
    [SerializeField]
    private int fasterSetupFactory;
    public int factoryCoolDown = 3;                    //현재 턴 수

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

    public double GetRichDesperate()
    {
        return richDesperate;
    }

    void ResourceManage()
    {
        factoryPlayerIncome = 0;
        factoryRichIncome = 0;
        for (int i = 0; i < factories.Length; i++)
        {
            if (factories[i] != null)
            {
                if (factories[i].isConquered == 0)
                {
                    factoryRichIncome += (int)factories[i].CalculateIncome();//공장 수입
                } else
                {
                    factoryPlayerIncome += (int)factories[i].CalculateIncome();
                }
            }
        }
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
        float snakeBehaviourCost=0;
        int checker = 0;
        for (int i=0; i<attatchedSnakes.Length; i++)
        {
            if(attatchedSnakes[i]!=null)
            {
                if(attatchedSnakes[i].type==2)
                {
                    snakeBehaviourCost += attatchedSnakes[i].GetBehaviorCostIncrease();
                    checker++;
                }
            }
        }
        snakeCost = (int)snakeBehaviourCost;
        int tempRichSalary = RichSalary();
        

        desperateGauge.GetComponent<Transform>().localScale = new Vector3((float)richDesperate / 100, 1, 1);
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
        if(moneyDecrease >= 0) desperate = richMoney > 10000000 ? 0.5 * (moneyDecrease) / richMoney * 100 : 1.5 * (moneyDecrease) / richMoney * 100;
        if (isIncreaseDesperate)
        {
            ChangeDesperate(desperate);
        }
        richMoney -= moneyDecrease;
        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);
    }

    ///<summary>부자의 절박함 수치를 증가시킴 / 입력값은 여러 보정들을 거치기 전</summary>
    public void ChangeDesperate(double desperateIncrease)
    {
        int snakeDes = 0;
        //꽃뱀의 감소
        for ( int i = 0; i < attatchedSnakes.Length; i++ )
        {
            if (attatchedSnakes[i] != null)
            {
                snakeDes += (int)attatchedSnakes[i].GetDesperateControl();
            }
        }
        desperateIncrease *= ((100 - snakeDes) / 100);
        richDesperate += desperateIncrease;
        float despChange = (float)(richDesperate / 100);
        if (despChange > 0) DespGauge.transform.localScale = new Vector3(despChange, 1f, 1f);
        else DespGauge.transform.localScale = new Vector3(0f, 1f, 1f);
        maxRichDesperate = Mathf.Max((float)maxRichDesperate, (float)richDesperate);
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
        moneyText.text = playerMoney + "만 원";
        
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
    int FactoryCooldown()               //공장 주기 정하는 값
    {
        int ret = 3;
        for(int i=0;i<attatchedSnakes.Length;i++)
        {
            if (attatchedSnakes[i] != null && attatchedSnakes[i].type == 3) ret += attatchedSnakes[i].RichCycleIncrease();
        }
        ret += factoryCoolDownDecrease;
        return ret;
    }
    int RichSalary()
    {
        ChangeRichMoney(-factoryRichIncome, false);
        ChangeRichMoney(crookIncome + snakeCost , true);
        return factoryRichIncome - crookIncome - snakeCost;
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
        return (int)(crookIncome * ratio) + factoryPlayerIncome;
    }
}
