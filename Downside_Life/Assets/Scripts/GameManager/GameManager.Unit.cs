using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public List<Crook> crooks;
    public Crook[] addedCrooks = new Crook[3];
    public List<Snake> snakes;
    public Snake[] addedSnake = new Snake[3];
    public List<Gang> gangs;
    public Gang[] addedGang = new Gang[3];

    [Header("전체적인 특성")]
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
        public Crook(int level, int attribute, float richPercentageDown, float playerPercentageUp)
        {
            attatched = false;
            this.level = level;
            richConstantDown = level * attribute;
            this.richPercentageDown = richPercentageDown;
            this.playerPercentageUp = playerPercentageUp;
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
        snakes = new List<Snake>();
        gangs = new List<Gang>();
    }
}
