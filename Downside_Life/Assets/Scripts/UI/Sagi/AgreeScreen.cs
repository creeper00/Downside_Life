using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgreeScreen : MonoBehaviour
{
    public enum Activity { hire, attatch };

    [HideInInspector]
    public int activity;
    [HideInInspector]
    public int level;

    public Button[] hireButton = new Button[4];
    public Button[] attatchButton = new Button[4];

    public void Behavior(int activity, int level)
    {
        this.activity = activity;
        this.level = level;
    }

    public void DoIt()
    {
        if ( activity == 0 )
        {
            int cost = GameManager.instance.sagiHireCost[level - 1];
            GameManager.instance.playerMoney -= cost;
            GameManager.instance.sagiHired[level - 1] = true;

            hireButton[level-1].GetComponent<Image>().color = new Color32(0, 0, 255, 255);
            hireButton[level-1].transform.GetChild(0).GetComponent<Text>().text = string.Format("고용됨");

            attatchButton[level-1].GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            attatchButton[level-1].transform.GetChild(0).GetComponent<Text>().text = string.Format("붙이기");
        }
        else
        {
            int cost = GameManager.instance.sagiAttatchCost[level - 1];
            GameManager.instance.playerMoney -= cost;
            GameManager.instance.sagiAttatched[level - 1] = true;

            attatchButton[level-1].GetComponent<Image>().color = new Color32(0, 0, 255, 255);
            attatchButton[level-1].transform.GetChild(0).GetComponent<Text>().text = string.Format("붙음");
        }
        gameObject.SetActive(false);
    }

    public void DontDoIt()
    {
        gameObject.SetActive(false);
    }
}
