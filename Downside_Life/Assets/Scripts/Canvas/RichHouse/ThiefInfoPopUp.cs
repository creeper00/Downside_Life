using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefInfoPopUp : MonoBehaviour
{
    [SerializeField]
    public GameObject thiefInfo;
    public void Awake()
    {
        thiefInfo = GameObject.Find("ThiefInfo");
        thiefInfo.SetActive(false);
    }
    public void ThiefPopUp()
    {
        thiefInfo.SetActive(true);
    }

}
