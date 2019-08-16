using System.Collections;
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

        public int CalculateIncome()
        {
            return (50 + 50 * (health / maxhealth)) * income;
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
        
        public void FactoryLevelup()
        {
            level++;
            health = GameManager.instance.factoryHealthPerLevel[level];
            maxhealth = GameManager.instance.factoryHealthPerLevel[level];
            income = GameManager.instance.factoryIncome[level];
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
    public bool isFirstBuilt = true;

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

    [Header("둔감형 꽃뱀")]
    [SerializeField]
    private float snakeDesperateDownConstant;
    [SerializeField]
    private float snakeDesperateDownCoefficient;

    [Header("낭비형 꽃뱀")]
    [SerializeField]
    private int snakeActionCostIncreaseConstant;
    [SerializeField]
    private int snakeActionCostIncreaseCoefficient;

    [Header("둔화형 꽃뱀")]
    [SerializeField]
    private int snakeActionTurnIncrease;

    [Header("갈취형 꽃뱀")]
    [SerializeField]
    private int snakeExtortProbability;
    [SerializeField]
    private int snakeExtortConstantRangeUpper, snakeExtortConstantRangeLower;
    [SerializeField]
    private int snakeExtortCoefficientRangeUpper, snakeExtortCoefficientRangeLower;

    [Header("갱단 공격력")]
    [SerializeField]
    private int[] gangAttackConstant = new int[4];
    [SerializeField]
    private int[] gangAttackCoefficient = new int[4];
    [SerializeField]
    private int[] gangReturnMonetPerDamage = new int[4];

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
        public int type;                                        //0-둔감형-절박함 억제, 1-낭비형-부자 행동 비용 증가, 2-둔화형-부자 행동 주기 증가, 3-갈취형-돈 아이템 가져옴

        public float snakeDesperateDown
        {
            get
            {
                if ( type == 0 )
                {
                    return instance.snakeDesperateDownConstant + level * instance.snakeDesperateDownCoefficient;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int snakeActionCostIncrease
        {
            get
            {
                if ( type == 1 )
                {
                    return instance.snakeActionCostIncreaseConstant + level * instance.snakeActionCostIncreaseCoefficient;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int snakeActionTurnIncrease
        {
            get
            {
                if ( type == 2 )
                {
                    return instance.snakeActionTurnIncrease;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float snakeExtortProbability
        {
            get
            {
                if ( type == 3 )
                {
                    return instance.snakeExtortProbability;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int snakeExtortCost
        {
            get
            {
                return Random.Range(instance.snakeExtortConstantRangeLower, instance.snakeExtortConstantRangeUpper) + level * Random.Range(instance.snakeExtortCoefficientRangeLower, instance.snakeExtortCoefficientRangeUpper);
            }
        }

        public Snake(int level, int type)
        {
            this.level = level;
            this.type = type;
        }
    }

    public class Gang
    {
        bool attatched;
        public int level;
        public int type;                                        //유형 번호. 0-깡딜형, 1-돈형, 2-도벽형, 3-광역형

        public int attack
        {
            get
            {
                int ret = 0;
                //기본 값
                ret += (instance.gangAttackConstant[type] + instance.gangAttackCoefficient[type] * level);

                return ret;
            }
            set { }
        }

        public int returnMoney
        {
            get
            {
                return instance.gangReturnMonetPerDamage[type];
            }
            set { }
        }

        public Gang(int level, int type)
        {
            attatched = false;
            this.level = level;
            this.type = type;
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
    void FactoryBehavior()
    {
        if (factoryCoolDown > 0)
        {
            factoryCoolDown--;
        }
        else
        {
            if (factories.Count != 3)
            {
                SetupFactories();
            }
            else
            {
                LevelUpFactories();
            }
            factoryCoolDown = FactoryCooldown();
        }
    }
    void LevelUpFactories()
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
            if (factories[i].level == 5)//만렙이면
            {
                factories[i].health += 300;//공장 피 회복
                if (factories[i].health > factories[i].maxhealth) factories[i].health = factories[i].maxhealth;
            }
        }
        //레벨 제일 높은거 고르기
        if (pos > -1)
        {
            if(Random.Range(0, 100) < 40)
            {
                factories[pos].FactoryLevelup();
            }
            else
            {
                int a = -1, b = -1;
                switch(pos)
                {
                    case 0:
                        a = 1; b = 2;
                        break;
                    case 1:
                        a = 0; b = 2;
                        break;
                    case 2:
                        a = 0; b = 1;
                        break;
                }
                if (Random.Range(0, 100) < 50)
                {
                    factories[a].FactoryLevelup();
                }
                else
                {
                    factories[b].FactoryLevelup();
                }
            }
        }
        ChangeRichMoney(1000000000, false);
    }
    void SetupFactories()
    {
        Debug.Log("Setup new Factory");
        int temp = Random.Range(0, 5);
        if (isFirstBuilt)
        {
            temp = 0;
            isFirstBuilt = false;
        }
        Debug.Log(temp);
        factories.Add(new Factory((Factory.FactoryType)temp, factoryValue[temp]));
    }
}
