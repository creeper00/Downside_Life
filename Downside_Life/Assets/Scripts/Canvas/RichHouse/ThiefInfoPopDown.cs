using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefInfoPopDown : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thiefInfo;
    public void Awake()
    {
        thiefInfo = GameObject.Find("ThiefInfo");
        thiefInfo.SetActive(false);
    }

    public void ThiefPopDown()
    {
        thiefInfo.SetActive(false);
    }
}
