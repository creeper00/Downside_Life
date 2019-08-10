using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetireButton : MonoBehaviour
{

    private GameManager.Job kindOfUnit;
    private int slotIndex;

    public void InitializeRetireButton(GameManager.Job kindOfUnit, int slotindex)
    {
        this.kindOfUnit = kindOfUnit;
        this.slotIndex = slotindex;
    }

    public void Retire()
    {
        GameManager.instance.Retire(kindOfUnit, slotIndex);
    }
}
