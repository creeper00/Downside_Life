using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GangIconExplanation : MonoBehaviour
{
    GameObject popUp;
    GameObject unitExplanationCanvas;
    GameObject unitExplanationPanel;
    GameObject unitExplanationText;
    private int thisJob;
    private int thisType;
    public void Awake()
    {
        popUp = GameObject.Find("Popups");
        unitExplanationCanvas = popUp.transform.Find("UnitExplanationCanvas").gameObject;
        unitExplanationPanel = unitExplanationCanvas.transform.Find("UnitExplanationPanel").gameObject;
        unitExplanationText = unitExplanationPanel.transform.Find("ExplanationText").gameObject;
        thisJob = gameObject.GetComponentInParent<UnitListItem>().job;
        thisType = gameObject.GetComponentInParent<UnitListItem>().type;
    }
    public void infopopup()
    {
        unitExplanationCanvas.SetActive(true);
        if (gameObject.name == "AttackIcon")
        {
            if (thisType == 3) unitExplanationText.GetComponent<Text>().text = "모든 공장에게 주는 데미지";
            else unitExplanationText.GetComponent<Text>().text = "모든 공장에게 주는 데미지";
        }
        if (gameObject.name == "ReturnIcon")
        {
            unitExplanationText.GetComponent<Text>().text = "들고오는 돈";
        }
        if (gameObject.name == "DamageIconSmall")
        {
            unitExplanationText.GetComponent<Text>().text = "공장에게 준 데미지";
        }
        if (gameObject.name == "SpecialSkillIcon")
        {
            if (thisType == 2) unitExplanationText.GetComponent<Text>().text = "공격한 공장의 수입을 일정량 감소시킨다.";
            if (thisType == 3) unitExplanationText.GetComponent<Text>().text = "존재하는 공장을 모두 공격한다.";
            if (thisType == 4) unitExplanationText.GetComponent<Text>().text = "공장 하나를 점령하여, 공장 수입의 몇%를 플레이어에게 가져온다.";
        }
    }

    public void infopopdown()
    {
        unitExplanationCanvas.SetActive(false);
    }
}
