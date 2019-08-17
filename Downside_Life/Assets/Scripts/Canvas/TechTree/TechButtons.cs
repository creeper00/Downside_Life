using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechButtons : MonoBehaviour
{
    [HideInInspector]
    public Technology technology;

    GameObject techInfo;

    private void Start()
    {
        techInfo = GameObject.Find("TechInfo");
    }
    public void techUp()
    {
        technology.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
        technology.isResearched = true;
        techInfo.SetActive(false);
    }

    public void techInfoClose()
    {
        techInfo.SetActive(false);
    }
}
