﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class GameManager : MonoBehaviour
{
    public class Factory
    {
        public enum FactoryType
        {
            normal,
            taunt,
            thief,
            lawyer,
            bank
        }
        public int level;
        public int health;
        public int maxhealth;
        public int income;

        public FactoryType factoryType;
        int value;

        public int Calculate()
        {
            return value * level;
        }

        public Factory(FactoryType factoryType, int value)
        {
            this.level = 0;
            this.health = GameManager.instance.factoryHealthPerLevel[level];
            this.maxhealth = GameManager.instance.factoryHealthPerLevel[level];
            this.income = GameManager.instance.factoryIncome[level];
            this.factoryType = factoryType;
            this.value = value;
        }
    }
    public List<Crook> crooks;
    public List<Crook> sellingCrooks;
    public Crook[] attatchedCrooks = new Crook[3];
    public List<Snake> snakes;
    public List<Snake> sellingSnakes;
    public Snake[] attatchedSnakes = new Snake[3];
    public List<Gang> gangs;
    public List<Gang> sellingGangs;
    public Gang[] attatchedGangs = new Gang[3];

    public int crookTechRichPercentageIncrease;
    public int crookTechConstantIncrease;
    public int crookTechMyPercentageIncrease;

    public List<string> crookAttributes, snakeAttributes, gangAttributes;

    [Header("공장")]

    [HideInInspector]
    public List<Factory> factories;
    [SerializeField]
    public List<int> factoryHealthPerLevel, factoryValue, factoryIncome;

    [Header("사기꾼 상수 값")]
    [SerializeField]
    private float[] crookConstantInvolution = new float[4];              //사기꾼이 가져오는 상수 값에서 레벨이 제곱되는 횟수
    [SerializeField]
    private float[] crookConstantCoefficient = new float[4];             //사기꾼이 가져오는 상수 값에서 배율
    [SerializeField]
    private int[] crookConstant = new int[4];                            //사기꾼에 곱해지는 값

    [Header("사기꾼 계수 값")]
    [SerializeField]
    private float[] crookRateInvolution = new float[4];              //사기꾼이 가져오는 계수 값에서 레벨이 제곱되는 횟수
    [SerializeField]
    private float[] crookRateCoefficient = new float[4];             //사기꾼이 가져오는 계수 값에서 배율
    [SerializeField]
    private float[] crookRate = new float[4];                        //사기꾼에 곱해지는 값

    [Header("사기꾼 가져오는 비율")]
    [SerializeField]
    private int[] crookReturn = new int[4];                          //사기꾼이 가져온 돈에서 플레이어 돈에 추가하는 비율

    [Header("기타 유닛 관리 관련 값")]
    [SerializeField]
    private int unitAttatchStaminaDecrease;
    [SerializeField]
    private int unitRetireStaminaDecrease;


    [Header("전체적인 특성")]
    [SerializeField]
    public int crookStoreSellingNumber;
    [SerializeField]
    public int gangStoreSellingNumber;
    [SerializeField]
    public int snakeStoreSellingNumber;
    [HideInInspector]
    public int crookAverageLevel, crookMaxLevel;
    List<string> crookAttribute;                //나올 확률은 동일

    [HideInInspector]
    public int snakeAverageLevel, snakeMaxLevel;
    List<string> snakeAttribute;

    [HideInInspector]
    public int gangAverageLevel, gangMaxLevel;
    List<string> gangAttribute;

    public class Crook
    {
        bool attatched;                             //부자에게 붙어 있는가
        public int level;                           //사기꾼의 레벨
        public int type;                            //유형 번호. 0-상수형, 1-계수형, 2-밸런스형, 3-호구 돈 가져오는 형
        
        public int richConstantDown                //매 턴 깎는 상수 값
        {
            get
            {
                
                int ret = 0;
                //기본 수치
                ret += (int)(System.Math.Pow(level, instance.crookConstantInvolution[type]) * instance.crookConstantCoefficient[type]) * instance.crookConstant[type];
                //아이템 추가 수치
                ret *= instance.crookTechConstantIncrease;
                //테크트리에서 가져오는 수치

                return ret;
            }
            set { }
        }
        public float richPercentageDown                             //매 턴 깎는 비율 값
        {
            get
            {
                float ret = 0f;
                //기본 수치
                ret += (int)(System.Math.Pow(level, instance.crookRateInvolution[type]) * instance.crookRateCoefficient[type]) * instance.crookRate[type];
                //아이템 추가 수치
                ret *= instance.crookTechRichPercentageIncrease;
                //테크트리에서 가져오는 수치

                return ret;
            }
            set { }
        }
        public float playerPercentageUp                             //깎은 돈 중 가져오는 비율
        {
            get
            {
                int ret = 0;
                //기본 값
                ret += instance.crookReturn[type];
                //아이템 추가 수치
                ret *= instance.crookTechMyPercentageIncrease;
                //테크트리에서 가져오는 수치

                return ret;
            }
            set { }
        }
        
        public Crook(int level, int type)
        {
            attatched = false;
            this.level = level;
            this.type = type;
        }
    }
    public class Snake
    {
        public int level;
        public int attribute;
        public Snake(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
        }
    }

    public class Gang
    {
        public int level;
        public int attack;
        public int attribute;
        
        public Gang(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
        }
    }

    /// <summary>유닛을 붙일 수 있는지 확인</summary>
    public bool CanAttatchUnit(Job kindOfUnit, int slotIndex)
    {
        if (!CheckStamina(unitAttatchStaminaDecrease)) return false;
        switch (kindOfUnit)
        {
            case Job.crook:
                if (attatchedCrooks[slotIndex] == null)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Crook in the slot must retire first!!!");
                    return false;
                }
            case Job.snake:
                if (attatchedSnakes[slotIndex] == null)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Snake in the slot must retire first!!!");
                    return false;
                }
            case Job.gang:
                if (attatchedGangs[slotIndex] == null)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Gang in the slot must retire first!!!");
                    return false;
                }
            default:
                return true;
        }
    }

    /// <summary>데이터 상에서 유닛을 이동</summary>
    public void AttatchUnit(Job kindOfUnit, int unitIndex, int slotIndex)
    {
        switch(kindOfUnit)
        {
            case Job.crook:
                ConsumeStamina(unitAttatchStaminaDecrease);
                Crook movingCrook = crooks[unitIndex];
                crooks.RemoveAt(unitIndex);
                attatchedCrooks[slotIndex] = movingCrook;
                break;
            case Job.snake:
                ConsumeStamina(unitAttatchStaminaDecrease);
                Snake movingSnake = snakes[unitIndex];
                snakes.RemoveAt(unitIndex);
                attatchedSnakes[slotIndex] = movingSnake;
                break;
            case Job.gang:
                Gang movingGang = gangs[unitIndex];
                gangs.RemoveAt(unitIndex);
                attatchedGangs[slotIndex] = movingGang;
                break;
        }
    }

    /// <summary>붙인 유닛을 뗄 수 있는지 확인</summary>
    public bool CanRetire(Job kindOfUnit, int index)
    {
        switch (kindOfUnit)
        {
            case Job.crook:
                if (!CheckStamina(unitRetireStaminaDecrease)) return false;
                if (attatchedCrooks[index] != null)
                {
                    return true;
                }
                else
                {
                    Debug.Log("That crook is not attatched!");
                    return false;
                }
            case Job.snake:
                if (!CheckStamina(unitRetireStaminaDecrease)) return false;
                if (attatchedSnakes[index] != null)
                {
                    return true;
                }
                else
                {
                    Debug.Log("That snake is not attatched!");
                    return false;
                }
            case Job.gang:
                if (attatchedGangs[index] != null)
                {
                    return true;
                }
                else
                {
                    Debug.Log("That gang is not attatched!");
                    return false;
                }
            case Job.robber:
                Debug.Log("Robber can't Retire!!!");
                return false;
            default:
                return false;
        }
    }

    /// <summary>붙인 유닛을 뗌</summary>
    public void RetireUnit(Job kindOfUnit, int index)
    {
        switch(kindOfUnit)
        {
            case Job.crook:
                ConsumeStamina(unitRetireStaminaDecrease);
                attatchedCrooks[index] = null;
                UnitsManager.instance.DeleteSlot(UnitsManager.Tabs.crook, index);
                break;
            case Job.snake:
                ConsumeStamina(unitRetireStaminaDecrease);
                attatchedSnakes[index] = null;
                UnitsManager.instance.DeleteSlot(UnitsManager.Tabs.snake, index);
                break;
            case Job.gang:
                Gang movingGang = attatchedGangs[index];
                attatchedGangs[index] = null;
                gangs.Add(movingGang);
                UnitsManager.instance.DeleteSlot(UnitsManager.Tabs.gang, index);
                UnitsManager.instance.showGangs();
                break;
        }
        UnitsManager.instance.UpdateRichMoneyChange();
    }

    
    

    public void crookReroll()
    {
        Debug.Log(crookStoreSellingNumber);
        sellingCrooks = new List<Crook>();
        for (int i=0; i<crookStoreSellingNumber; i++)
        {
            sellingCrooks.Add(new Crook(Random.Range(2 * crookAverageLevel - crookMaxLevel, crookMaxLevel), Random.Range(0, crookAttributes.Count)));
        }
        StoreManager.instance.showStoreCrooks();
        for (int i=0; i<crookStoreSellingNumber; i++)
        {
            StoreManager.instance.isCrookBuyed.Add(false);
        }
    }
    public void snakeReroll()
    {
        sellingSnakes = new List<Snake>();
        for (int i = 0; i < snakeStoreSellingNumber; i++)
        {
            sellingSnakes.Add(new Snake(Random.Range(2 * snakeAverageLevel - snakeMaxLevel, snakeMaxLevel), Random.Range(0, snakeAttributes.Count)));
        }
        StoreManager.instance.showStoreSnakes();
        for (int i = 0; i < snakeStoreSellingNumber; i++)
        {
            StoreManager.instance.isSnakeBuyed.Add(false);
        }
    }
    public void gangReroll()
    {
        sellingGangs = new List<Gang>();
        for (int i = 0; i < gangStoreSellingNumber; i++)
        {
            sellingGangs.Add(new Gang(Random.Range(2 * gangAverageLevel - gangMaxLevel, gangMaxLevel), Random.Range(0, gangAttributes.Count)));
        }
        StoreManager.instance.showStoreGangs();
        for (int i = 0; i < gangStoreSellingNumber; i++)
        {
            StoreManager.instance.isGangBuyed.Add(false);
        }
    }

    void LevelUpFactories()
    {
        if (factoryLevelUpCooldown > 0)
        {
            factoryLevelUpCooldown--;
        }
        else
        {
            int pos = -1;
            int tempLevel = -1;
            for (int i = 0; i < factories.Count; i++)
            {
                if (tempLevel < factories[i].level && factories[i].level < 5)
                {
                    tempLevel = factories[i].level;
                    pos = i;
                }
            }
            //레벨 제일 높은거 고르기
            if (pos > -1)
            {
                factories[pos].level++;
            }
            factoryLevelUpCooldown = FactoryCooldown();
        }
    }
    void SetupFactories()
    {
        if (Random.Range(0, 100) < 20)
        {
            if (factories.Count > 2)
            {
                return;
            }
            Debug.Log("Setup new Factory");
            int temp = Random.Range(0, 5);
            Debug.Log(temp);
            factories.Add(new Factory((Factory.FactoryType)temp, factoryValue[temp]));

        }
    }
}
