using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Technology : MonoBehaviour
{
    enum Upgrade
    {
        ratioIncrease,
        constantIncrease,
        moneyIncrease,
        // Crook
        desperateControlIncrease, // 절박함 억제력 증가
        richCostIncrease,
        sellIncrease,
        desperateDecrease,
        // Snake
        attackIncrease,
        multipleAttackIncrease,
        attachGangIncrease,
        returnMoneyIncrease,
        // Gang
        itemFloor, // 아이템 층에 아이템만 나오게 함
        minMoneyIncrease,
        successPercentageIncrease,
        itemUnlock, // rareItem, Factory...
        staminaDecrease,
        stealAgain,
        calander,
        // Thief
        rerollNumIncrease,
        priceDecrease,
        attributeUnlock,
        expandSlot,
        minLevelIncrease,
        giveAdditionalMoney,
        //Common
        passive,
        vacation,
        tuja
    }
    [HideInInspector]
    public bool isResearched;
    [SerializeField]
    public GameManager.Job whatJob;
    [SerializeField]
    public int skillPointNeededNum;
    public int skillLevel;
    [SerializeField]
    int maxSkillLevel;
    [HideInInspector]
    public int temporaryLevel;
    public int tier;
    [SerializeField]
    Upgrade upgrade;
    [SerializeField]
    List<float> values;

    [SerializeField]
    public bool isPassive;
    [SerializeField]
    public List<int> skillPointNeeded;

    GameObject MoneyWarn;
    GameObject TechPopUp;
    GameObject TechPop;

    public void Start()
    {
        showTechnology();
    }
    public bool SetTemporaryPassive()
    {
        bool temp = false;
        if (skillPointNeeded[temporaryLevel] <= TechManager.instance.temporaryJobSkillPoint[(int)whatJob])
        {
            temporaryLevel++;
            temp = true;
        } else if (temporaryLevel > 0 && skillPointNeeded[temporaryLevel - 1] > TechManager.instance.temporaryJobSkillPoint[(int)whatJob])
        {
            temporaryLevel--;
            temp = false;
        }
        transform.Find("LVText").GetComponent<Text>().text = temporaryLevel.ToString();
        return temp;
    }
    public bool CanResearch()
    {
        if (skillPointNeededNum > TechManager.instance.temporaryJobSkillPoint[(int)whatJob] || TechManager.instance.temporarySkillPoint < 1)
        {
            return false;
        }
        return true;
    }
    public void ShowTempoaryTechnology()
    {
        transform.Find("LVText").GetComponent<Text>().text = temporaryLevel.ToString();
        showCanTemporarySkillPoint();
    }
    public void showTechnology()
    {
        transform.Find("LVText").GetComponent<Text>().text = skillLevel.ToString();
        showCanSkillPoint();
    }
    void showCanSkillPoint()//스킬포인트 + -를 보여주는 부분
    {
        transform.Find("Minus").gameObject.SetActive(skillLevel != temporaryLevel);
        transform.Find("Plus").gameObject.SetActive(TechManager.instance.temporarySkillPoint > 0 && skillLevel != maxSkillLevel && (CanResearch() || TechManager.instance.tier[(int)whatJob] == tier));
    }
    void showCanTemporarySkillPoint()
    {
        transform.Find("Minus").gameObject.SetActive(temporaryLevel != skillLevel);
        transform.Find("Plus").gameObject.SetActive(TechManager.instance.temporarySkillPoint > 0 && temporaryLevel != maxSkillLevel && (CanResearch() || TechManager.instance.tier[(int)whatJob] == tier));
    }
    public void confirmSkillLevel()
    {
        MoneyWarn = GameObject.Find("MoneyWarning");
        TechPop = MoneyWarn.transform.Find("NotEnoughMoney").gameObject;
        TechPopUp = TechPop.transform.Find("Text").gameObject;

        skillLevel = temporaryLevel;
        Debug.Log(upgrade + " " + skillLevel);
        switch (upgrade)
        {
            case Upgrade.ratioIncrease:
                GameManager.instance.crookRatioTech = values[skillLevel];
                break;
            case Upgrade.constantIncrease:
                GameManager.instance.crookConstantTech = values[skillLevel];
                break;
            case Upgrade.moneyIncrease:
                GameManager.instance.crookMoneyTech = values[skillLevel];
                break;
            //crook
            case Upgrade.desperateControlIncrease:
                GameManager.instance.desConTech = values[skillLevel];
                break;
            case Upgrade.richCostIncrease:
                GameManager.instance.behaviorCostTech = values[skillLevel];
                break;
            case Upgrade.sellIncrease:
                GameManager.instance.itemPriceTech = values[skillLevel];
                break;
            case Upgrade.desperateDecrease:
                if (GameManager.instance.doRichDesperateDown)
                {
                    GameManager.instance.ChangeDesperate(values[skillLevel]);
                    if (skillLevel == 1)
                    {
                        GameManager.instance.doRichDesperateDown = false;
                    }
                }
                break;
            //snake
            case Upgrade.attachGangIncrease:
                GameManager.instance.SetFreeGangAttachPossible(skillLevel == 1);
                break;
            case Upgrade.attackIncrease:
                GameManager.instance.gangAttackTech1 = values[skillLevel];
                break;
            case Upgrade.multipleAttackIncrease:
                GameManager.instance.gangAttackTech2 = values[skillLevel];
                break;
            case Upgrade.returnMoneyIncrease:
                GameManager.instance.gangReturnMoneyTech = values[skillLevel];
                break;
            case Upgrade.attributeUnlock:
                switch(whatJob)
                {
                    case GameManager.Job.crook:
                        for (int i=0; i<GameManager.instance.crookType.Count; i++)
                        {
                            if (GameManager.instance.crookType[i] == (int)values[0])
                            {
                                break;
                            }
                            if (i == GameManager.instance.crookType.Count - 1 && skillLevel == 1)
                            {
                                GameManager.instance.crookType.Add((int)values[0]);
                            }
                        }
                        break;
                    case GameManager.Job.gang:
                        for (int i = 0; i < GameManager.instance.gangType.Count; i++)
                        {
                            if (GameManager.instance.gangType[i] == (int)values[0])
                            {
                                break;
                            }
                            if (i == GameManager.instance.gangType.Count - 1 && skillLevel == 1)
                            {
                                GameManager.instance.gangType.Add((int)values[0]);
                            }
                        }
                        break;
                    case GameManager.Job.snake:
                        for (int i = 0; i < GameManager.instance.snakeType.Count; i++)
                        {
                            if (GameManager.instance.snakeType[i] == (int)values[0])
                            {
                                break;
                            }
                            if (i == GameManager.instance.snakeType.Count - 1 && skillLevel == 1)
                            {
                                GameManager.instance.snakeType.Add((int)values[0]);
                            }
                        }
                        break;
                }
                break;
            case Upgrade.calander:
                break;
            case Upgrade.expandSlot:
                break;
            case Upgrade.itemFloor:
                GameManager.instance.isItemFloor = skillLevel == 1 ? true : false;
                break;
            case Upgrade.itemUnlock:
                GameManager.instance.maxGrade = 1 + skillLevel;
                break;
            case Upgrade.minLevelIncrease:
                switch(whatJob)
                {
                    case GameManager.Job.crook:
                        GameManager.instance.crookMinLevel = 1 + 4 * skillLevel;
                        break;
                    case GameManager.Job.gang:
                        GameManager.instance.gangMinLevel = 1 + 4 * skillLevel;
                        break;
                    case GameManager.Job.snake:
                        GameManager.instance.snakeMinLevel = 1 + 4 * skillLevel;
                        break;
                }
                break;
            case Upgrade.minMoneyIncrease:
                GameManager.instance.rangeDecrease = values[skillLevel];
                break;
            case Upgrade.priceDecrease:
                switch(whatJob)
                {
                    case GameManager.Job.crook:
                        GameManager.instance.crookDecreaseUnitCost = values[skillLevel];
                        break;
                    case GameManager.Job.gang:
                        GameManager.instance.gangDecreaseUnitCost = values[skillLevel];
                        break;
                    case GameManager.Job.snake:
                        GameManager.instance.snakeDecreaseUnitCost = values[skillLevel];
                        break;
                }
                break;
            case Upgrade.rerollNumIncrease:
                switch(whatJob)
                {
                    case GameManager.Job.crook:
                        GameManager.instance.crookStoreSellingNumber = 3 + skillLevel;
                        break;
                    case GameManager.Job.gang:
                        GameManager.instance.gangStoreSellingNumber = 3 + skillLevel;
                        break;
                    case GameManager.Job.snake:
                        GameManager.instance.snakeStoreSellingNumber = 3 + skillLevel;
                        break;
                }
                break;
            case Upgrade.staminaDecrease:
                GameManager.instance.StealStaminaDecrease = 3 - skillLevel;
                break;
            case Upgrade.stealAgain:
                GameManager.instance.stealTwicePercentage = (int)(values[skillLevel]);
                break;
            case Upgrade.successPercentageIncrease:
                GameManager.instance.thiefSuccessPercentage = (int)(values[skillLevel]);
                break;
            case Upgrade.giveAdditionalMoney:
                GameManager.instance.additionalMoney = (int)(values[skillLevel]);
                break;
            case Upgrade.passive:
                switch(whatJob)
                {
                    case GameManager.Job.crook:
                        GameManager.instance.crookMinLevel = (int)values[skillLevel * 2];
                        GameManager.instance.crookMaxLevel = (int)values[skillLevel * 2 + 1];
                        break;
                    case GameManager.Job.gang:
                        GameManager.instance.gangMinLevel = (int)values[skillLevel * 2];
                        GameManager.instance.gangMaxLevel = (int)values[skillLevel * 2 + 1];
                        break;
                    case GameManager.Job.robber:
                        GameManager.instance.thiefStealMoneyLowerBound = (int)values[skillLevel * 2];
                        GameManager.instance.thiefStealMoneyUpperBound = (int)values[skillLevel * 2 + 1];
                        break;
                    case GameManager.Job.snake:
                        GameManager.instance.snakeMinLevel = (int)values[skillLevel * 2];
                        GameManager.instance.snakeMaxLevel = (int)values[skillLevel * 2 + 1];
                        break;
                }
                break;
            case Upgrade.vacation:
                if (!GameManager.instance.doVacation)
                {
                    
                    if (skillLevel == 1)
                    {
                        GameManager.instance.factoryCoolDown += skillLevel == 1 ? 4 : 0;
                        TechPopUp.GetComponent<Text>().text = "바캉스!!";
                        Debug.Log("Yeah");
                        StartCoroutine("showPopup");
                        GameManager.instance.doVacation = true;                   
                    }                 

                }
                
                break;
            case Upgrade.tuja:
                if (skillLevel == 1 && !GameManager.instance.doTuja)
                {
                    int richMoney = (int)(GameManager.instance.richMoney * 0.9);
                    int percentage = Random.Range(0, 100);
                    int Change = 0;
                    if (Random.Range(0, 100) < values[0])
                    {
                        GameManager.instance.ChangeRichMoney(richMoney, false);
                        Change = richMoney;
                        Debug.Log("망함 ㅋ");
                        //망함 ㅋ
                    } else if (Random.Range(0, 100) <values[0] + values[1])
                    {
                        GameManager.instance.ChangeRichMoney((int)(richMoney * 0.3), false);
                        Debug.Log("덜 망함 ㅋ");
                        //덜 망함 ㅋ
                        Change = (int) (richMoney * 0.3);
                    } else
                    {
                        GameManager.instance.ChangeRichMoney(-richMoney, false);
                        Debug.Log("대박");
                        Change = -richMoney;
                        //대박
                    }
                    GameManager.instance.doTuja = true;

                    TechPopUp.GetComponent<Text>().fontSize = 16;
                    if (Change < 0)  TechPopUp.GetComponent<Text>().text = "투자 유치!!\n" + "돈 변화: " + Change+" (대박)";
                    else TechPopUp.GetComponent<Text>().text = "투자 유치!!\n" + "돈 변화: " + Change + " (망함)";
                    StartCoroutine("showPopup");
                    Debug.Log("asjfijfo");
                }
                break;
        }
        GameManager.instance.storeCanvas.SetActive(true);
        GameManager.instance.storeCanvas.SetActive(false);
        StoreManager.instance.showStoreCrooks();
        StoreManager.instance.showStoreSnakes();
        StoreManager.instance.showStoreGangs();
    }

    IEnumerator showPopup()
    {
        TechPop.SetActive(true);
        yield return new WaitForSeconds(2f);
        TechPop.SetActive(false);
    }
    public void ResetSkillLevel()
    {
        temporaryLevel = skillLevel;
    }
}
