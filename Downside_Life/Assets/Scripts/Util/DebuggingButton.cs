using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomething()
    {
        GameManager.instance.ConsumeStamina(1);
    }
}
