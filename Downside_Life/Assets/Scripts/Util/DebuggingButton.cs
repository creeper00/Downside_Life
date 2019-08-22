using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomethig()
    {
        GameManager.instance.richDesperate += 5;
        Debug.Log("현재 절박함: " + GameManager.instance.richDesperate);
    }
}
