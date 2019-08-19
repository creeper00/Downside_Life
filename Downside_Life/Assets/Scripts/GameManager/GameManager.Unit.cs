using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class GameManager : MonoBehaviour
{
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
    public Factory[] factories;
    [SerializeField]
    public List<int> factoryHealthPerLevel, factoryValue, factoryIncome;
    bool isFirstBuilt = true;

    [Header("사기꾼 상수 값")]
    [SerializeField]
    private int[] crookConstantConstant = new int[4];                   //사기꾼이 가져오는 상수 값에서 레벨이 제곱되는 횟수
    [SerializeField]
    private int[] crookConstantCoefficient = new int[4];                //사기꾼에 곱해지는 값

    [Header("사기꾼 계수 값")]
    [SerializeField]
    private float[] crookRateConstant = new float[4];                   //사기꾼이 가져오는 계수 값에서 레벨이 제곱되는 횟수
    [SerializeField]
    private float[] crookRateCoefficient = new float[4];                //사기꾼이 가져오는 계수 값에서 배율

    [Header("사기꾼 가져오는 비율")]
    [SerializeField]
    private float[] crookReturn = new float[4];                          //사기꾼이 가져온 돈에서 플레이어 돈에 추가하는 비율

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
        public bool isUpgrade;

        public FactoryType factoryType;
        int value;

        public float RateOfOperation()
        {
            return 50 + 50 * (health / maxhealth);
        }
        public int Calculate()
        {
            return value * level;
        }

        public float CalculateIncome()
        {
            return RateOfOperation() * income;
        }
        public Factory(FactoryType factoryType, int value)
        {
            this.level = 0;
            this.health = GameManager.instance.factoryHealthPerLevel[level];
            this.maxhealth = GameManager.instance.factoryHealthPerLevel[level];
            if (factoryType == FactoryType.taunt)
            {
                health += 1000;
                maxhealth += 1000;
            }
            this.income = GameManager.instance.factoryIncome[level];
            this.factoryType = factoryType;
            this.value = value;
        }

        public void FactoryLevelup()
        {
            int temp = maxhealth - health;
            level++;
            health = GameManager.instance.factoryHealthPerLevel[level] - temp;
            maxhealth = GameManager.instance.factoryHealthPerLevel[level];
            if (factoryType == FactoryType.taunt)
            {
                health += 1000;
                maxhealth += 1000;
            }
            income = GameManager.instance.factoryIncome[level];
            isUpgrade = false;
        }
    }

    private IEnumerator showItemTypeNotMatchWindow()
    {
        itemTypeNotMatchCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        itemTypeNotMatchCanvas.SetActive(false);
    }
    public void itemTypeNotMatch()
    {
        StartCoroutine(showItemTypeNotMatchWindow());
    }
    // 테스트
    private IEnumerator showAlreadyHasItemWindow()
    {
        alreadyHasItemCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        alreadyHasItemCanvas.SetActive(false);
    }
    public void alreadyHasItem()
    {
        StartCoroutine(showItemTypeNotMatchWindow());
    }
    // 테스트



    public class Crook
    {
        bool attatched;                             //부자에게 붙어 있는가
        bool itemAttached;                          //아이템이 붙어 있는가
        public int level;                           //사기꾼의 레벨
        public int type;                            //유형 번호. 0-상수형, 1-계수형, 2-밸런스형, 3-호구 돈 가져오는 형
        public float itemRichDown;                  //부자 지출 증가 아이템의 계수
        public float itemPlayerUp;                  //사기꾼 수입 증가 아이템의 계수

        public int richConstantDown                //매 턴 깎는 상수 값
        {
            get
            {

                int ret = 0;
                //기본 수치
                ret += (int)((instance.crookConstantConstant[type] * level + instance.crookConstantCoefficient[type]) * itemRichDown);
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
                ret += (int)((instance.crookRateConstant[type] * level + instance.crookRateCoefficient[type]) * itemRichDown);
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
                float ret = 0;
                //기본 값
                ret += instance.crookReturn[type] * itemPlayerUp;
                //아이템 추가 수치
                ret *= instance.crookTechMyPercentageIncrease;
                //테크트리에서 가져오는 수치

                return ret;
            }
            set { }
        }

        public void putItem(item item)
        {
            if (itemAttached)
            {
                GameManager.instance.alreadyHasItem();
                return; // 이미 붙어 있다는 것에 대한 경고
            }
            if (item.type != 0)
            {
                GameManager.instance.itemTypeNotMatch();
                return; // 사기꾼용 아이템이 아니라는 것에 대한 경고
            }
            if (item.itemcode == 0)
            {
                if (item.grade == 0)
                {
                    itemRichDown = (float)1.1;
                }
                else if (item.grade == 1)
                {
                    itemRichDown = (float)1.2;
                }
                else
                {
                    itemRichDown = (float)1.4;
                }
                return;
            }
            else if (item.itemcode == 1 && item.grade == 1)
            {
                return; // 유형 변경 팝업
            }
            else if (item.itemcode == 2)
            {
                itemPlayerUp = (float)1.1;
                return;
            }
            else
            {
                //장착 불가능 팝업
            }
        }

        public Crook(int level, int type)
        {
            attatched = false;
            itemAttached = false;
            this.level = level;
            this.type = type;
            itemRichDown = 1;
            itemPlayerUp = 1;
        }
    }

    public class Snake
    {
        bool itemAttached;                                          //아이템이 붙어있는가
        public int level;
        public int type;                                            //0-둔감형-절박함 억제, 1-낭비형-부자 행동 비용 증가, 2-둔화형-부자 행동 주기 증가, 3-갈취형-돈 아이템 가져옴
        public float itemSnakeUpgrade;                              //아이템의 꽃뱀 능력 업그레이드 계수
        public static float[] passiveSnakeUpgrade = new float[4];   //특성 강화 패시브 아이템 효과
        public float snakeDesperateDown
        {
            get
            {
                if (type == 0)
                {
                    return (instance.snakeDesperateDownConstant + level * instance.snakeDesperateDownCoefficient) * itemSnakeUpgrade * passiveSnakeUpgrade[0];
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
                if (type == 1)
                {
                    return (int)((instance.snakeActionCostIncreaseConstant + level * instance.snakeActionCostIncreaseCoefficient) * itemSnakeUpgrade * passiveSnakeUpgrade[1]);
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
                if (type == 2)
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
                if (type == 3)
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
                return (int)((Random.Range(instance.snakeExtortConstantRangeLower, instance.snakeExtortConstantRangeUpper) + level * Random.Range(instance.snakeExtortCoefficientRangeLower, instance.snakeExtortCoefficientRangeUpper)) * itemSnakeUpgrade * passiveSnakeUpgrade[3]);
            }
        }

        public void putItem(item item)
        {
            if (itemAttached)
            {
                GameManager.instance.alreadyHasItem();
                return; // 이미 붙어 있다는 것에 대한 경고
            }
            if (item.type != 1)
            {
                GameManager.instance.itemTypeNotMatch();
                return; // 꽃뱀용 아이템이 아니라는 것에 대한 경고
            }
            if (item.itemcode == 0 && item.grade != 2)
            {
                if (item.grade == 0)
                {
                    itemSnakeUpgrade = (float)1.1;
                    itemAttached = true;
                }
                else if (item.grade == 1)
                {
                    itemSnakeUpgrade = (float)1.2;
                    itemAttached = true;
                }
                return;
            }
            else if (item.itemcode == 1 && item.grade == 1)
            {
                return; // 꽃뱀 유형 변경 팝업
            }
            else
            {
                // 장착 불가능 팝업
            }
        }

        public Snake(int level, int type)
        {
            itemAttached = false;
            this.level = level;
            this.type = type;
            itemSnakeUpgrade = 1;

        }
    }

    public class Gang
    {
        bool attatched;
        bool itemAttached;                                      //아이템이 붙어있는가
        public bool itemUpgradeDelay;                           //공장 지연 아이템이 붙어있는가
        public int level;
        public int type;                                        //유형 번호. 0-깡딜형, 1-돈형, 2-도벽형, 3-광역형
        public float itemWeaponUpgrade;                         //공격력 아이템 계수
        public float itemMoneyUpgrade;                          //돈 아이템 계수
        public int attack
        {
            get
            {
                int ret = 0;
                //기본 값
                ret += (int)((instance.gangAttackConstant[type] + instance.gangAttackCoefficient[type] * level) * itemWeaponUpgrade);

                return ret;
            }
            set { }
        }

        public int returnMoney
        {
            get
            {
                return (int)(instance.gangReturnMonetPerDamage[type] * itemMoneyUpgrade);
            }
            set { }
        }
        public void putItem(item item)
        {
            if (itemAttached)
            {
                GameManager.instance.alreadyHasItem();
                return; // 이미 붙어 있다는 것에 대한 경고
            }
            if (item.type != 0)
            {
                GameManager.instance.itemTypeNotMatch();
                return; // 갱단용 아이템이 아니라는 것에 대한 경고
            }
            if (item.itemcode == 0 && item.grade != 2)
            {
                if (item.grade == 0)
                {
                    itemWeaponUpgrade = (float)1.2;
                    itemAttached = true;
                }
                else if (item.grade == 1)
                {
                    itemWeaponUpgrade = (float)1.4;
                    itemAttached = true;
                }
                return;
            }
            else if (item.itemcode == 1 && item.grade == 1)
            {
                return; // 유형 변경 팝업
            }
            else if (item.itemcode == 2)
            {
                if (item.grade == 0)
                {
                    itemMoneyUpgrade = (float)1.2;
                    itemAttached = true;
                }
                else if (item.grade == 1)
                {
                    itemUpgradeDelay = true;
                    itemAttached = true;
                }
                return;
            }
            else
            {
                return; // 장착 불가능 아이템
            }
        }

        public Gang(int level, int type)
        {
            attatched = false;
            itemAttached = false;
            itemUpgradeDelay = false;
            this.level = level;
            this.type = type;
            itemWeaponUpgrade = 1;
            itemMoneyUpgrade = 1;
        }
    }

    public class item
    {
        public int type;                                    //0 - 사기꾼용, 1 - 꽃뱀용, 2 - 갱단용
        public int grade;                                   //등급 0 - 일반, 1 - 레어, 2 - 레전
        public int itemcode;                                //아이템 하는 일 0 - 강화, 1 - 유형 변경, 2 - 기타
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
        switch (kindOfUnit)
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
        switch (kindOfUnit)
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
                UnitsManager.instance.ShowGangs();
                break;
        }
        UnitsManager.instance.UpdateRichMoneyChange();
    }




    public void CrookReroll()
    {
        sellingCrooks = new List<Crook>();
        for (int i = 0; i < crookStoreSellingNumber; i++)
        {
            sellingCrooks.Add(new Crook(Random.Range(2 * crookAverageLevel - crookMaxLevel, crookMaxLevel), Random.Range(0, crookAttributes.Count)));
        }
        StoreManager.instance.showStoreCrooks();
        StoreManager.instance.isCrookBuyed = new List<bool>();
        for (int i = 0; i < crookStoreSellingNumber; i++)
        {
            StoreManager.instance.isCrookBuyed.Add(false);
        }
    }
    public void SnakeReroll()
    {
        sellingSnakes = new List<Snake>();
        for (int i = 0; i < snakeStoreSellingNumber; i++)
        {
            sellingSnakes.Add(new Snake(Random.Range(2 * snakeAverageLevel - snakeMaxLevel, snakeMaxLevel), Random.Range(0, snakeAttributes.Count)));
        }
        StoreManager.instance.showStoreSnakes(); StoreManager.instance.isSnakeBuyed = new List<bool>();
        for (int i = 0; i < snakeStoreSellingNumber; i++)
        {
            StoreManager.instance.isSnakeBuyed.Add(false);
        }
    }
    public void GangReroll()
    {
        sellingGangs = new List<Gang>();
        for (int i = 0; i < gangStoreSellingNumber; i++)
        {
            sellingGangs.Add(new Gang(Random.Range(2 * gangAverageLevel - gangMaxLevel, gangMaxLevel), Random.Range(0, gangAttributes.Count)));
        }
        StoreManager.instance.showStoreGangs(); StoreManager.instance.isGangBuyed = new List<bool>();
        for (int i = 0; i < gangStoreSellingNumber; i++)
        {
            StoreManager.instance.isGangBuyed.Add(false);
        }
    }
    void FactoryBehavior()
    {
        for (int i = 0; i < factories.Length; i++)
        {
            if (factories[i] != null && factories[i].isUpgrade)
            {
                factories[i].FactoryLevelup();
            }
        }
        if (factoryCoolDown > 0)
        {
            factoryCoolDown--;
        }
        else
        {
            int pos = Random.Range(0, 3); // factory중 하나 선택

            if (factories[pos] == null)
            {
                SetupFactories(pos);
            }
            else
            {
                LevelUpFactories(pos);
            }
            factoryCoolDown = FactoryCooldown();
        }
    }
    void LevelUpFactories(int pos)
    {
        if (factories[pos].level < 5)
        {
            if (Random.Range(0, 100) < 40)
            {
                factories[pos].isUpgrade = true;
            }

        }
        else
        {
            factories[pos].health += 500;
            if (factories[pos].health > factories[pos].maxhealth)
            {
                factories[pos].health = factories[pos].maxhealth;
            }
        }
    }
    void SetupFactories(int pos)
    {
        Debug.Log("Setup new Factory");
        int temp = Random.Range(0, 5);
        if (isFirstBuilt)
        {
            temp = 0;
            isFirstBuilt = false;
        }
        Debug.Log(temp);
        factories[pos] = new Factory((Factory.FactoryType)temp, factoryValue[temp]);
    }
}