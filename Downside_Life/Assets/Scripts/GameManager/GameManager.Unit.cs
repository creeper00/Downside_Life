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
    public List<Gang>[] attachedGangs = new List<Gang>[3];
    public List<Item> gangItems;
    

    public List<string> crooktypes, snaketypes, gangtypes;

    [Header("공장")]
    [HideInInspector]
    public Factory[] factories = new Factory[3];
    [SerializeField]
    public List<int> factoryHealthPerLevel, factoryValue, factoryIncome;
    bool isFirstBuilt = true;

    [Header("사기꾼")]

    [SerializeField]
    public List<float> crookConstantInit, crookConstantPerLevel, crookAttackItems, crookRatioInit, crookRatioPerLevel, crookMoneyInit, crookMoneyItems, crookUnitCostInit, crookUnitCostPerLevel;
    [SerializeField]
    public float crookConstantTech, crookRatioTech, crookMoneyTech;
    [Header("꽃뱀")]

    [SerializeField]
    public float desConInit, desConPerLevel, desConTech, itemPriceTech, itemPercent, behaviorCostInit, behaviorCostPerLevel, behaviorCostTech, itemPriceItemsLowerBound, itemPriceItemsLowerBoundPerLevel, itemPriceItemsUpperBound, itemPriceItemsUpperBoundPerLevel;
    public List<float> itemPriceItems, snakeUnitCostInit, snakeUnitCostPerLevel;

    [Header("갱단")]
    [SerializeField]
    public List<float> gangAttackInit, gangAttackPerLevel, gangAttackItem, gangReturnMoneyPerType, gangReturnMoneyItem, gangUnitCostInit, gangUnitCostPerLevel;
    public float gangAttackTech1, gangAttackTech2, gangReturnMoneyTech;

    [Header("전체적인 특성")]

    [SerializeField]
    public int unitAttatchStaminaDecrease, unitRetireStaminaDecrease;

    [SerializeField]
    public int crookStoreSellingNumber, snakeStoreSellingNumber, gangStoreSellingNumber;
    [SerializeField]
    public int crookMinLevel, crookMaxLevel, snakeMinLevel, snakeMaxLevel, gangMinLevel, gangMaxLevel;



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
        [HideInInspector]
        private Sprite crookConstant, crookRate, crookBalanced, crookIdiot; // 순서대로 상수형, 계수형, 밸런스형, 호구형
        public string GetType()
        {
            switch(type)
            {
                case 0:
                    return "상수형";
                case 1:
                    return "계수형";
                case 2:
                    return "밸런스형";
                case 3:
                    return "호구형";
            }
            return "";
        }

        public Sprite GetSprite()
        {
            switch(type)
            {
                case 0:
                    return crookConstant;
                case 1:
                    return crookRate;
                case 2:
                    return crookBalanced;
                case 3:
                    return crookIdiot;   
            }
            return null;
        }
        public float unitPrice()
        {
            return instance.crookUnitCostInit[type] + level * instance.crookUnitCostPerLevel[type];
        }
        public float GetRichConstantDown()
        {
            float itemRatio = 1;
            if (item != null && item.itemCode == 0)
            {
                itemRatio = instance.crookAttackItems[item.grade];
            }
            return (instance.crookConstantInit[type] + level * instance.crookConstantPerLevel[type]) * instance.crookConstantTech * itemRatio;
        }
        public float GetRichRatioDown()
        {
            float itemRatio = 1;
            if (item != null && item.itemCode == 0)
            {
                itemRatio = instance.crookAttackItems[item.grade];
            }
            return (instance.crookRatioInit[type] + level * instance.crookRatioPerLevel[type]) * instance.crookRatioTech * itemRatio;
        }
        public float GetMoneyUp()
        {
            float itemRatio = 1;
            if (item != null && item.itemCode == 2)
            {
                itemRatio = instance.crookMoneyItems[item.grade];
            }
            return (instance.crookMoneyInit[type]) * instance.crookMoneyTech * itemRatio;//가져오는 비율
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
        [HideInInspector]
        private Sprite snakeDesp, snakeWaste, snakeSlow, snakeMoney; // 순서대로 둔감형, 낭비형, 둔화형, 갈취형
        public string GetType()
        {
            switch (type)
            {
                case 0:
                    return "둔감형";
                case 1:
                    return "갈취형";
                case 2:
                    return "낭비형";
                case 3:
                    return "둔화형";
            }
            return "";
        }
        public Sprite GetSprite()
        {
            switch (type)
            {
                case 0:
                    return snakeDesp;
                case 1:
                    return snakeMoney;
                case 2:
                    return snakeWaste;
                case 3:
                    return snakeSlow;
            }
            return null;
        }
        public float unitPrice()
        {
            return instance.snakeUnitCostInit[type] + level * instance.snakeUnitCostPerLevel[type];
        }
        public float GetDesperateControl()
        {
            float temp = 1;
            for (int i=0; i<instance.snakeItems.Count; i++)
            {
                if (instance.snakeItems[i].itemCode == 0 && instance.snakeItems[i].grade == 2)
                {
                    temp += 0.3f;
                }
            }
            float ret = (instance.desConInit + instance.desConPerLevel * level + instance.desConTech) * ((item != null && item.itemCode == 0) ? (item.grade == 0 ? 1.1f : 1.3f) : 1f) * temp;
            if (type != 0)
            {
                ret = 0;
            }
            return ret;
        }
        public float GetLowerBound()
        {
            return instance.itemPriceItemsLowerBound + instance.itemPriceItemsLowerBoundPerLevel * level;
        }
        public float GetUpperBound()
        {
            return instance.itemPriceItemsUpperBound + instance.itemPriceItemsUpperBoundPerLevel * level;
        }
        public float GetItemPrice()
        {
            float temp = 1;
            float equippedItemRatio = 1;
            for (int i=0; i<instance.snakeItems.Count; i++)
            {
                if (instance.snakeItems[i].itemCode == 1 && instance.snakeItems[i].grade == 2)
                {
                    temp += 0.3f;
                }
            }
            if (item != null && item.itemCode == 0)
            {
                equippedItemRatio = instance.itemPriceItems[item.grade];
            }
            float ret = Random.Range(GetLowerBound() , GetUpperBound()) * instance.itemPriceTech * equippedItemRatio * temp;
            if (type != 1)
            {
                ret = 0;
            }
            return ret;
        }
        public float GetBehaviorCostIncrease()
        {
            float temp = 1;
            for (int i=0; i<instance.snakeItems.Count; i++)
            {
                if (instance.snakeItems[i].itemCode == 2 && instance.snakeItems[i].grade == 2)
                {
                    temp += 0.3f;
                }
            }
            float ret = (instance.behaviorCostInit + instance.behaviorCostPerLevel * level * instance.behaviorCostTech) * ((item != null && item.itemCode == 0) ? (item.grade == 0 ? 1.1f : 1.3f) : 1f) * temp;
            if (type != 2)
            {
                ret = 0;
            }
            return ret;
        }   

        public int RichCycleIncrease()
        {
            if (type == 4)
            {
                return 4;
            }
            if (type == 3)
            {
                return 1;
            }
            return 0;
        }
        public bool putItem(int itemIndex)                          //붙었으면 true, 안 붙었으면 false 반환
        {
            Item item = instance.snakeItems[itemIndex];
            if (item != null)
            {
                instance.alreadyHasItem();
                return false;
            }
            else if (item.type != 1)
            {
                instance.itemTypeNotMatch();
                return false;
            }
            else
            {
                this.item = item;
                return true;
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
        public string GetType()
        {
            switch (type)
            {
                case 0:
                    return "딜러형";
                case 1:
                    return "수금형";
                case 2:
                    return "도벽형";
                case 3:
                    return "광역형";
                case 4:
                    return "점령";
            }
            return "";
        }
        public int level, type;
        Item item;

        public float unitPrice()
        {
            return instance.gangUnitCostInit[type] + level * instance.gangUnitCostPerLevel[type];
        }
        public float attack()
        {
            float equippedItemRatio = 1;
            if (item != null && item.itemCode == 0)
            {
                equippedItemRatio = instance.gangAttackItem[item.grade];
            }
            return (instance.gangAttackInit[type] + instance.gangAttackPerLevel[type] * level) * (1 + instance.gangAttackTech1 + instance.gangAttackTech2) * equippedItemRatio;
        }

        public float returnMoney()
        {
            float equippedItemRatio = 1;
            if (item != null && item.itemCode == 2)
            {
                equippedItemRatio = instance.gangReturnMoneyItem[item.grade];
            }
            return attack() * instance.gangReturnMoneyPerType[type] * equippedItemRatio + instance.gangReturnMoneyTech;
        }
        public bool PutItem(int itemIndex)                          //붙었으면 true, 안 붙었으면 false 반환
        {
            Item item = instance.snakeItems[itemIndex];
            if (item != null)
            {
                instance.alreadyHasItem();
                return false;
            }
            else if (item.type != 2)
            {
                instance.itemTypeNotMatch();
                return false;
            }
            else
            {
                this.item = item;
                return true;
            }
        }

        public Gang(int level, int type)
        {
            this.level = level;
            this.type = type;
        }
    }



    /// <summary>유닛을 붙일 수 있는지 확인. 붙일 수 있는지 여부를 boolean으로 반환</summary>
    public bool CanAttatchUnit(Job kindOfUnit, int slotIndex)
    {
        switch (kindOfUnit)
        {
            case Job.crook:
                if (attatchedCrooks[slotIndex] == null)             //사기꾼이 이 슬롯에 안 붙어 있어야 하니까
                {
                    if (!CheckStamina(unitAttatchStaminaDecrease)) return false;
                    return true;
                }
                else
                {
                    Debug.Log("Crook in the slot must retire first!!!");
                    return false;
                }
            case Job.snake:
                if (attatchedSnakes[slotIndex] == null)             //꽃뱀이 이 슬롯에 안 붙어 있어야 하니까
                {
                    if (!CheckStamina(unitAttatchStaminaDecrease)) return false;
                    return true;
                }
                else
                {
                    Debug.Log("Snake in the slot must retire first!!!");
                    return false;
                }
            case Job.gang:
                if ( factories[slotIndex] == null)                  //갱단은 여러 개 붙일 수 있지만, 공장이 일단 있어야 함
                {
                    if (!CheckStamina(unitAttatchStaminaDecrease)) return false;
                    return true;
                }
                else
                {
                    Debug.Log("The factory is not built yet");
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
                attachedGangs[slotIndex].Add(movingGang);
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
                if (attachedGangs[index] != null)
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
                /*
                Gang movingGang = attachedGangs[index];
                attachedGangs[index] = null;
                gangs.Add(movingGang);
                UnitsManager.instance.DeleteSlot(UnitsManager.Tabs.gang, index);
                UnitsManager.instance.ShowGangs();
                */
                break;
        }
        UnitsManager.instance.UpdateRichMoneyChange();
    }




    public void CrookReroll()
    {
        sellingCrooks = new List<Crook>();
        for (int i = 0; i < crookStoreSellingNumber; i++)
        {
            sellingCrooks.Add(new Crook(Random.Range(crookMinLevel, crookMaxLevel), Random.Range(0, 4)));
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
            sellingSnakes.Add(new Snake(Random.Range(snakeMinLevel, snakeMaxLevel), Random.Range(0, 4)));
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
            sellingGangs.Add(new Gang(Random.Range(gangMinLevel, gangMaxLevel), Random.Range(0, 3)));
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