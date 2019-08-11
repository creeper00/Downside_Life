using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomething()
    {
        UnitsManager.instance.showCrooks();
    }
}
