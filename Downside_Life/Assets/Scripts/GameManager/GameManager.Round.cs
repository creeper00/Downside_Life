using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public void EndTurn()
    {
        ResourceManage();
        EventManage();
        SetStamina((stamina>2) ? 10 : stamina + 8) ;
        crookReroll();
        gangReroll();
        snakeReroll();
        SetupFactories();
        LevelUpFactories();
        Debug.Log("Factory Level Up Cooldown : " + factoryLevelUpCooldown);
        Debug.Log("factories count : " + factories.Count);
        for (int i = 0; i < factories.Count; i++)
        {
            Debug.Log("Factory" + i + " " + factories[i].factoryType);
            Debug.Log(factories[i].health + "/" + factories[i].maxhealth + " " + factories[i].income + " " + factories[i].level + " " + factories[i].Calculate());
        }
    }
}