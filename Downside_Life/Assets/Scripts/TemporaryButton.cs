using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryButton : MonoBehaviour
{
    [SerializeField]
    Transform prefab;
    [SerializeField]
    GameObject parent;

    public void addCrooks()
    {
        GameManager.instance.crooks.Add(new GameManager.Crook(1, 10000, 0.01f, 0.02f));
    }

    public void addSnakes()
    {
        GameManager.instance.snakes.Add(new GameManager.Snake(1, 1));
    }

    public void addGangs()
    {
        GameManager.instance.gangs.Add(new GameManager.Gang(1, 1));
    }
}
