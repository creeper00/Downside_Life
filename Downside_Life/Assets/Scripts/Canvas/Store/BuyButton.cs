using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public GameManager.Crook crook;
    public void addCrook()
    {
        GameManager.instance.crooks.Add(crook);
    }
}
