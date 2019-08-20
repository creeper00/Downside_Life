using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class GameManager : MonoBehaviour
{
    [HideInInspector]
    public List<Crook> crooks;
    public List<Crook> sellingCrooks;
    public Crook[] attatchedCrooks = new Crook[3];
    public List<Item> crookItems;

    public List<Snake> snakes;
    public List<Snake> sellingSnakes;
    public Snake[] attatchedSnakes = new Snake[3];
    public List<Item> snakeItems;

    public List<Gang> gangs;
    public List<Gang> sellingGangs;
    public Gang[] attatchedGangs = new Gang[3];
    public List<Item> gangItems;

    public float crookTechRichPercentageIncrease;
    public float crookTechConstantIncrease;
    public int crookTechMyPercentageIncrease;

    public List<string> crooktypes, snaketypes, gangtypes;

    [Header("공장")]
    [HideInInspector]
    public Factory[] factories;
    [SerializeField]
    public List<int> factoryHealthPerLevel, factoryValue, factoryIncome;
    bool isFirstBuilt = true;

    [SerializeField]
    List<float> crookConstantInit, crookConstantPerLevel, crookConstantItems, crookRatioInit, crookRatioPerLevel, crookRatioItems, crookMoneyInit, crookMoneyPerLevel, crookMoneyItems;
    [SerializeField]
    float crookConstantTech, crookRatioTech, crookMoneyTech;
    [SerializeField]
    float desConInit, desConPerLevel, desConTech, itemPercentInit, itemPercentPerLevel, itemPercentTech, behaviorCostInit, behaviorCostPerLevel, behaviorCostTech;
    

    [SerializeField]
    int unitAttatchStaminaDecrease, unitRetireStaminaDecrease;

    [SerializeField]
    public int crookStoreSellingNumber, snakeStoreSellingNumber, gangStoreSellingNumber;
    [SerializeField]
    int crookMinLevel, crookMaxLevel, snakeMinLevel, snakeMaxLevel, gangMinLevel, gangMaxLevel;



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
        public int level;
        public int type;
        Item item;

        public float GetRichConstantDown()
        {
            float itemRatio = 0;
            if (item.itemCode == 0)
            {
                itemRatio = instance.crookConstantItems[item.grade];
            }
            return (instance.crookConstantInit[type] + level * instance.crookConstantPerLevel[type]) * instance.crookConstantTech * itemRatio;
        }
        public float GetRichRatioDown()
        {
            float itemRatio = 0;
            if (item.itemCode == 0)
            {
                itemRatio = instance.crookRatioItems[item.grade];
            }
            return (instance.crookConstantInit[type] + level * instance.crookConstantPerLevel[type]) * instance.crookConstantTech * itemRatio;
        }
        public float GetMoneyUp()
        {
            float itemRatio = 0;
            if (item.itemCode == 0)
            {
                itemRatio = instance.crookMoneyItems[item.grade];
            }
            return (instance.crookConstantInit[type] + level * instance.crookConstantPerLevel[type]) * instance.crookConstantTech * itemRatio;
        }
        public bool putItem(int itemIndex)                  //붙었으면 true, 안 붙었으면 false 반환
        {
            Item item = instance.crookItems[itemIndex];
            if (item != null) {
                instance.alreadyHasItem();
                return false;
            } else if (item.type != 0) {
                instance.itemTypeNotMatch();
                return false;
            } else {
                this.item = item;
                return true;
            }
        }

        public Crook(int level, int type)
        {
            this.level = level;
            this.type = type;
        }
    }


    public class Snake
    {
        bool attached;
        bool itemAttached;
        public int level;
        public int type; // 0 = 절박함 증가 억제, 1 = 환금형 아이템, 2 = 행동 비용 증가, 3 = 행동 주기 증가, 4 = 만렙 특성
        Item item;

        public float GetDesperateControl()
        {
            float temp = 0;
            for (int i=0; i<instance.snakeItems.Count; i++)
            {
                if (instance.snakeItems[i].itemCode == 0 && instance.snakeItems[i].grade == 2)
                {
                    temp += 0.3f;
                }
            }
            float ret = (instance.desConInit + instance.desConPerLevel * level + instance.desConTech) * ((item.itemCode == 0) ? (item.grade == 0 ? 1.1f : 1.3f) : 1f) + temp;
            if (type != 0)
            {
                ret = 0;
            }
            return ret;
        }
        public float GetItemPercentage()
        {
            float temp = 0;
            for (int i=0; i<instance.snakeItems.Count; i++)
            {
                if (instance.snakeItems[i].itemCode == 1 && instance.snakeItems[i].grade == 2)
                {
                    temp += 0.3f;
                }
            }
            float ret = (instance.itemPercentInit + instance.itemPercentPerLevel * level + instance.itemPercentTech) * ((item.itemCode == 0) ? (item.grade == 0 ? 1.1f : 1.3f) : 1f) + temp;
            if (type != 1)
            {
                ret = 0;
            }
            return ret;
        }
        public float GetBehaviorCostIncrease()
        {
            float temp = 0;
            for (int i=0; i<instance.snakeItems.Count; i++)
            {
                if (instance.snakeItems[i].itemCode == 2 && instance.snakeItems[i].grade == 2)
                {
                    temp += 0.3f;
                }
            }
            float ret = (instance.behaviorCostInit + instance.behaviorCostPerLevel * level + instance.behaviorCostTech) * ((item.itemCode == 0) ? (item.grade == 0 ? 1.1f : 1.3f) : 1f) + temp;
            if (type != 2)
            {
                ret = 0;
            }
            return ret;
        }

        public int RichCycleIncrease()
        {
            if (type != 3)
            {
                return 0;
            }
            if (type == 4)
            {
                return 4;
            }
            return 1;
        }
        public bool putItem(int itemIndex)                          //붙었으면 true, 안 붙었으면 false 반환
        {
            Item item = instance.snakeItems[itemIndex];
            if (itemAttached)
            {
                GameManager.instance.alreadyHasItem();
                return false; // 이미 붙어 있다는 것에 대한 경고
            }
            if (item.type != 1)
            {
                GameManager.instance.itemTypeNotMatch();
                return false; // 꽃뱀용 아이템이 아니라는 것에 대한 경고
            }
            if (item.itemCode == 1 && item.grade == 1)
            {
                return true; // 꽃뱀 유형 변경 팝업
            }
            return false;
        }
        public Snake(int level, int type)
        {
            this.level = level;
            this.type = type;
        }
    }

    public class Gang
    {
        public int attack, returnMoney;
        public int level, type;
        bool itemAttached;
        public bool PutItem(int itemIndex)                          //붙었으면 true, 안 붙었으면 false 반환
        {
            Item item = instance.gangItems[itemIndex];
            if (itemAttached)
            {
                GameManager.instance.alreadyHasItem();
                return false; // 이미 붙어 있다는 것에 대한 경고
            }
            if (item.type != 2)
            {
                GameManager.instance.itemTypeNotMatch();
                return false; // 갱단용 아이템이 아니라는 것에 대한 경고
            }
            if (item.itemCode == 1 && item.grade == 1)
            {
                return true;
            }
            return false;
        }

        public Gang(int level, int type)
        {
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
            sellingCrooks.Add(new Crook(Random.Range(crookMinLevel, crookMaxLevel), Random.Range(0, crooktypes.Count)));
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
            sellingSnakes.Add(new Snake(Random.Range(snakeMinLevel, snakeMaxLevel), Random.Range(0, snaketypes.Count)));
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
            sellingGangs.Add(new Gang(Random.Range(gangMinLevel, gangMaxLevel), Random.Range(0, gangtypes.Count)));
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