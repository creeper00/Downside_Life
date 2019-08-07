using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public List<Crook> crooks;
    public List<Crook> addedCrooks;
    public List<Snake> snakes;
    public List<Snake> addedSnakes;
    public List<Gang> gangs;
    public List<Gang> addedGangs;

    //전체적인 특성
    [HideInInspector]
    public int crookAverageLevel, crookMaxLevel;
    List<string> crookAttribute;//나올 확률은 동일

    [HideInInspector]
    public int snakeAverageLevel, snakeMaxLevel;
    List<string> snakeAttribute;

    [HideInInspector]
    public int gangAverageLevel, gangMaxLevel;
    List<string> gangAttribute;
    //

    public class Crook
    {
        int level;
        int attribute;
        int attack;
        public Crook(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
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
