using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetireButton : MonoBehaviour
{

    private GameManager.Job kindOfUnit;
    private int slotIndex;

    public void InitializeRetireButton(GameManager.Job kindOfUnit, int slotIndex)
    {
        this.kindOfUnit = kindOfUnit;
        this.slotIndex = slotIndex;
    }

    public void Retire()
    {
        GameManager.instance.Retire(kindOfUnit, slotIndex);
    }
}
