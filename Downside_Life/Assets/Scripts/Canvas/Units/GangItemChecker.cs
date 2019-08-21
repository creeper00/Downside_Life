using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GangItemChecker : MonoBehaviour
{
    // Start is called before the first frame update
    Button Finish;
    GameObject ItemList;
    void Start()
    {
        ItemList = GameObject.Find("ItemList");
        Finish =this.GetComponent<Button>();
        Finish.onClick.AddListener(ButtonFinish);
    }

    // Update is called once per frame
    void ButtonFinish()
    {
        ItemList.GetComponent<Image>().color = new Color32(50,255,255,255);
    }
}
