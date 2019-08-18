using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomething()
    {
        if (GameManager.instance.crooks[0] != null) Debug.Log(GameManager.instance.crooks[0].richConstantDown);

    }
}
