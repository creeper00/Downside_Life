using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetireButton : MonoBehaviour
{

    private GameManager.Job kindOfUnit;
    public int slotIndex;

    public void InitializeRetireButton(GameManager.Job kindOfUnit, int slotIndex)
    {
        this.kindOfUnit = kindOfUnit;
        this.slotIndex = slotIndex;
    }

    public void Retire()
    {
        if (GameManager.instance.CanRetire(kindOfUnit, slotIndex))
        {
            GameManager.instance.RetireUnit(kindOfUnit, slotIndex);
        }
    }
}
