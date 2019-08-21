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
        // Crook
        desperateControlIncrease, // 절박함 억제력 증가
        richCostIncrease,
        desperateDecrease,
        // Snake
        attackIncrease,
        particularAttributeAttackIncrease,
        attachGangIncrease,
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
        incomeIncrease,
        priceDecrease,
        attributeUnlock,
        expandSlot,
        minLevelIncrease
        //Common

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

    public void Start()
    {
        showTechnology();
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
        skillLevel = temporaryLevel;
        switch (upgrade)
        {
            case Upgrade.attachGangIncrease:
                break;
            case Upgrade.attackIncrease:
                break;
            case Upgrade.attributeUnlock:
                break;
            case Upgrade.calander:
                break;
            case Upgrade.constantIncrease:
                GameManager.instance.crookConstantTech = values[skillLevel];
                break;
            case Upgrade.desperateControlIncrease:
                GameManager.instance.desConTech = values[skillLevel];
                break;
            case Upgrade.expandSlot:
                break;
            case Upgrade.incomeIncrease:
                break;
            case Upgrade.itemFloor:
                break;
            case Upgrade.itemUnlock:
                break;
            case Upgrade.minLevelIncrease:
                break;
            case Upgrade.minMoneyIncrease:
                break;
            case Upgrade.particularAttributeAttackIncrease:
                break;
            case Upgrade.priceDecrease:
                break;
            case Upgrade.ratioIncrease:
                GameManager.instance.crookTechRichPercentageIncrease = values[skillLevel];
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
            case Upgrade.richCostIncrease:
                GameManager.instance.behaviorCostTech = values[skillLevel];
                break;
            case Upgrade.staminaDecrease:
                GameManager.instance.unitAttatchStaminaDecrease = 3 - skillLevel;
                break;
            case Upgrade.stealAgain:
                break;
            case Upgrade.successPercentageIncrease:
                break;
        }
    }
    public void ResetSkillLevel()
    {
        temporaryLevel = skillLevel;
    }
}
