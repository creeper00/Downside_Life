using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggingButton : MonoBehaviour
{
    public void DebugSomethig()
    {
        Item item = new Item();
        GameManager.instance.playerMoney += 500000000;
        GameManager.instance.UpdateResourcesUI();
    }
}
