using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomethig()
    {
        GameManager.instance.crookItems.Add(new Item(0, 0, 0));
        int k = Slot.currentActiveSlotIndex;
        Debug.Log("number of attached gangs in " + k + ":" + GameManager.instance.attachedGangs[k].Count );
        if ( GameManager.instance.attachedGangs[0].Count >= 2 )
        {
            float z1 = GameManager.instance.attachedGangs[0][0].attack();
            float z2 = GameManager.instance.attachedGangs[0][1].attack();
            Debug.Log("공격력 :" + z1 + " " + z2 );
        }
    }
}
