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

    }
}
