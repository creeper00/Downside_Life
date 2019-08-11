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

    public List<string> crookAttributes, snakeAttributes, gangAttributes;

    [Header("유닛 관리 관련 상수")]
    [SerializeField]
    private int unitAttatchStaminaDecrease;
    [SerializeField]
    private int unitRetireStaminaDecrease;


    [Header("전체적인 특성")]
    [SerializeField]
    public int crookStoreSellingNumber, gangStoreSellingNumber, snakeStoreSellingNumber;
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
        public int richConstantDown;                //매 턴 깎는 상수 값
        public float richPercentageDown;            //매 턴 깎는 비율 값
        public float playerPercentageUp;            //깎은 돈 중 가져오는 비율
        public int attribute;
        public Crook(int level, int attribute)
        {
            attatched = false;
            this.level = level;
            richConstantDown = level * attribute;
            this.attribute = attribute;
            richPercentageDown = 1;         //계산식 필요합니다
            playerPercentageUp = 1;         //계산식 필요합니다
        }
    }

    /// <summary>데이터 상에서 유닛을 이동</summary>
    public void AttatchUnit(Job kindOfUnit, int unitIndex, int slotIndex)
    {
        switch(kindOfUnit)
        {
            case Job.crook:
                if (attatchedCrooks[slotIndex] == null)
                {
                    Crook movingCrook = crooks[unitIndex];
                    crooks.RemoveAt(unitIndex);
                    attatchedCrooks[slotIndex] = movingCrook;
                }
                else
                {
                    Debug.Log("Crook in the slot must retire first!!!");
                }
                break;
            case Job.snake:
                if (attatchedSnakes[slotIndex] == null)
                {
                    Snake movingSnake = snakes[unitIndex];
                    snakes.RemoveAt(unitIndex);
                    attatchedSnakes[slotIndex] = movingSnake;
                }
                else
                {
                    Debug.Log("Snake in the slot must retire first!!!");
                }
                break;
            case Job.gang:
                if (attatchedGangs[slotIndex] == null)
                {
                    Gang movingGang = gangs[unitIndex];
                    gangs.RemoveAt(unitIndex);
                    attatchedGangs[slotIndex] = movingGang;
                }
                else
                {
                    Debug.Log("Gang in the slot must retire first!!!");
                }
                break;
        }

        
        
    }

    public void Retire(GameManager.Job kindOfUnit, int index)
    {
        switch(kindOfUnit)
        {
            case Job.crook:
                if (attatchedCrooks[index] != null)
                {
                    attatchedCrooks[index] = null;
                    UnitsManager.instance.DeleteSlot(UnitsManager.Tabs.crook, index);
                }
                break;
            case Job.snake:
                if (attatchedSnakes[index] != null)
                {
                    attatchedSnakes[index] = null;
                    UnitsManager.instance.DeleteSlot(UnitsManager.Tabs.snake, index);
                }
                break;
            case Job.robber:
                Debug.Log("Robber can't be attatched!!!");
                break;
            case Job.gang:
                Debug.Log("Gang can't Retire!!!");
                break;
        }
        UnitsManager.instance.UpdateRichMoneyChange();
    }

    public class Snake
    {
        public int level;
        public int attribute;
        public int attack;
        public Snake(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
        }
    }

    public class Gang
    {
        public int level;
        public int attribute;
        public int attack;
        public Gang(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
        }
    }
    

    public void crookReroll()
    {
        sellingCrooks = new List<Crook>();
        for (int i=0; i<crookStoreSellingNumber; i++)
        {
            sellingCrooks.Add(new Crook(Random.Range(2 * crookAverageLevel - crookMaxLevel, crookMaxLevel), Random.Range(0, crookAttributes.Count)));
        }
        StoreManager.instance.showStoreCrooks();
        for (int i=0; i<crookStoreSellingNumber; i++)
        {
            StoreManager.instance.isCrookBuyed[i] = false;
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
            StoreManager.instance.isSnakeBuyed[i] = false;
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
            StoreManager.instance.isGangBuyed[i] = false;
        }
    }

}
