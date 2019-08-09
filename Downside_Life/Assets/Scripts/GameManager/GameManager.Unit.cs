using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public List<Crook> crooks;
    public Crook[] attatchedCrooks = new Crook[3];
    public List<Crook> sellingCrooks;
    public List<Snake> snakes;
    public Snake[] attatchedSnakes = new Snake[3];
    public List<Gang> gangs;
    public Gang[] attatchedGangs = new Gang[3];

    public List<string> crookAttributes;

    [Header("전체적인 특성")]
    [HideInInspector]
    public int crookAverageLevel, crookMaxLevel, crookStoreSellingNumber;
    List<string> crookAttribute;                //나올 확률은 동일

    [HideInInspector]
    public int snakeAverageLevel, snakeMaxLevel, snakeStoreSellingNumber;
    List<string> snakeAttribute;

    [HideInInspector]
    public int gangAverageLevel, gangMaxLevel, gangStoreSellingNumber;
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
            richPercentageDown = 1;//계산식 필요합니다
            playerPercentageUp = 1;//계산식 필요합니다
        }
    }

    public void moveCrookToSlot(int crookIndex, int slotIndex)
    {
        if (attatchedCrooks[slotIndex] == null)
        {
            Crook movingCrook = crooks[crookIndex];
            crooks.RemoveAt(crookIndex);
            attatchedCrooks[slotIndex] = movingCrook;
        }
        else
        {

        }
        
    }

    public class Snake
    {
        int level;
        int attribute;
        int attack;
        public Snake(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
        }
    }

    public class Gang
    {
        int level;
        int attribute;
        int attack;
        public Gang(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
        }
    }
    private void Start()
    {
        crooks = new List<Crook>();
        
        crookAttributes = new List<string>();
        snakes = new List<Snake>();
        gangs = new List<Gang>();
        crookStoreSellingNumber = 3;
    }

    public void crookReroll()
    {
        sellingCrooks = new List<Crook>();
        for (int i=0; i<crookStoreSellingNumber; i++)
        {
            sellingCrooks.Add(new Crook(Random.Range(2 * crookAverageLevel - crookMaxLevel, crookMaxLevel), Random.Range(0, crookAttributes.Count)));
        }
        StoreManager.instance.showStoreCrooks();
    }
    public void snakeReroll()
    {
        for (int i = 0; i < crookStoreSellingNumber; i++)
        {
            sellingCrooks[i] = new Crook(Random.Range(2 * crookAverageLevel - crookMaxLevel, crookMaxLevel), Random.Range(0, crookAttributes.Count));
        }
    }
    public void gangReroll()
    {
        for (int i = 0; i < crookStoreSellingNumber; i++)
        {
            sellingCrooks[i] = new Crook(Random.Range(2 * crookAverageLevel - crookMaxLevel, crookMaxLevel), Random.Range(0, crookAttributes.Count));
        }
    }

}
