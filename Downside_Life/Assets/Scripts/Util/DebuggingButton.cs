using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomethig()
    {
        Debug.Log(GameManager.instance.attachedGangs[0].Count + " " + GameManager.instance.attachedGangs[1].Count + " " + GameManager.instance.attachedGangs[2].Count);
    }
}
