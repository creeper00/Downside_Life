using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomethig()
    {
        Item tempItem = new Item(0, 1, 1);
        GameManager.instance.crookItems.Add(tempItem);
        Debug.Log(GameManager.instance.crookItems.Count);
    }
}
