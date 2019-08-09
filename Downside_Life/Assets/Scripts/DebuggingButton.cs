using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomething()
    {
        if (GameManager.instance.attatchedCrooks[0] != null) Debug.Log("crook 0 is attatched");
        if (GameManager.instance.attatchedCrooks[1] != null) Debug.Log("crook 1 is attatched");
        if (GameManager.instance.attatchedCrooks[2] != null) Debug.Log("crook 2 is attatched");
    }
}
