using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public int index;
    public GameManager.Crook crook;
    public void addCrook()
    {
        Debug.Log("add Crook at " + index);
        GameManager.instance.crooks.Add(crook);
        StoreManager.instance.isBuyed[index] = true;
        for (int i=0; i<GameManager.instance.crookStoreSellingNumber; i++)
        {
            Debug.Log(i + " " + StoreManager.instance.isBuyed[i]);
        }
    }
}
