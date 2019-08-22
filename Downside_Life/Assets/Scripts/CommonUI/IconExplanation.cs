using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconExplanation : MonoBehaviour
{
    GameObject popUp;
    GameObject unitExplanationCanvas;
    GameObject unitExplanationPanel;
    GameObject unitExplanationText;
    public void Awake()
    {
        popUp = GameObject.Find("Popups");
        unitExplanationCanvas = popUp.transform.Find("UnitExplanationCanvas").gameObject;
        unitExplanationPanel = unitExplanationCanvas.transform.Find("UnitExplanationPanel").gameObject;
        unitExplanationText = unitExplanationPanel.transform.Find("ExplanationText").gameObject;
    }
    public void infopopup()
    {
        unitExplanationCanvas.SetActive(true);
        unitExplanationPanel.transform.position = gameObject.transform.position;
        if(gameObject.name == "AttackIcon" || gameObject.name == "MoneyStealIcon")
        {
            unitExplanationText.GetComponent<Text>().text = "부자에게서 뜯어오는 돈";
        }
        if(gameObject.name == "ReturnIcon" || gameObject.name == "ReturnPercentageIcon")
        {
            unitExplanationText.GetComponent<Text>().text = "플레이어에게 들고오는 돈";
        }
        if(gameObject.name == "AttributeIcon")
        {
            unitExplanationText.GetComponent<Text>().text = "꽃뱀의 특수능력";
        }
    }

    public void infopopdown()
    {
        unitExplanationCanvas.SetActive(false);
    }
}
