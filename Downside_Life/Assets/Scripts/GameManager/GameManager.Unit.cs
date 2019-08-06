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
