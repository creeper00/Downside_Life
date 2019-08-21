using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggingButton : MonoBehaviour
{
    public GameObject slot0;

    public void DebugSomethig()
    {
        GameManager.instance.crookItems.Add(new Item(0, 1, 1));
        //Debug.Log(GameManager.instance.crookItems.Count);

        //Debug.Log()
    }
}
