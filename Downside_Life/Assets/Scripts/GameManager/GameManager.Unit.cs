using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    List<Crook> crooks;
    List<Crook> addedCrooks;
    List<Snake> snakes;
    List<Snake> addedSnakes;
    List<Gang> gangs;
    List<Gang> addedGangs;

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

    class Crook
    {
        int level;
        int attribute;
        int attack;
        Crook(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
        }
    }

    class Snake
    {
        int level;
        int attribute;
        int attack;
        Snake(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
        }
    }

    class Gang
    {
        int level;
        int attribute;
        int attack;
        Gang(int level, int attribute)
        {
            this.level = level;
            this.attribute = attribute;
            attack = level * attribute;
        }
    }

}
