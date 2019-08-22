using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomethig()
    {
        GameManager.instance.crookItems.Add(new Item(0, 0, 0));
        GameManager.instance.crookItems.Add(new Item(0, 1, 0));
        GameManager.instance.crookItems.Add(new Item(0, 2, 0));
        GameManager.instance.SetStamina(100000);
        Item temp = GameManager.instance.attatchedCrooks[0].GetItem();
        if ( temp != null) Debug.Log(temp.type + " " + temp.grade + " " + temp.itemCode);
    }
}
