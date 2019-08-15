using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomething()
    {
        Debug.Log(GameManager.instance.gangs[0].attack);
    }
}
