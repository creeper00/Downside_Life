using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technology : MonoBehaviour
{
    [SerializeField]
    List<Technology> previousTechnologies;
    bool isResearched;
    [SerializeField]
    int neededMoney;
    [SerializeField]
    GameObject Information;

    private void Start()
    {
        isResearched = false;
    }
    private void OnMouseDown()
    {
        for (int i=0; i<previousTechnologies.Capacity; i++)
        {
            if (!previousTechnologies[i].isResearched)//선행연구 진행안됨
            {
                return;
            }
        }
        if (GameManager.instance.playerMoney < neededMoney)//돈이 모자람
        {
            return;
        }
        isResearched = true;
        GameManager.instance.playerMoney -= neededMoney;
    }
}
