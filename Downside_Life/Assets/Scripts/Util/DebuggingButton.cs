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
        
        if ( GameManager.instance.attatchedCrooks[0] != null )
        {
            Debug.Log("attached");
        }
    }
}
