using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Technology : MonoBehaviour
{
    public enum Upgrade
    {
        maxLevel,
        averageLevel,
        storeNumber,
        attributeUnlock,
        crookRichPercentageUp,
        crookConstantUp,
        crookMyPercentageUp
    }
    [Header("Required")]
    [SerializeField]
    public List<Technology> previousTechnologies;
    [HideInInspector]
    public bool isResearched;
    [SerializeField]
    GameObject Information;

    [SerializeField]
    TechInfoButtons techUpButton;

    [Header("기술 정보")]
    [SerializeField]
    public string technologyName;
    [SerializeField]
    public GameManager.Job job;
    [SerializeField]
    public Upgrade upgrade;
    [SerializeField]
    public int value;
    [SerializeField]
    public string attribute;
    private void Start()
    {
        isResearched = false;
    }

    public void TechInfoOpen()
    {
        if (isResearched)
        {
            Debug.Log("이미 연구됨");
            return;
        }
        GameManager.instance.techInfoCanvas.SetActive(true);
        techUpButton.tech = this;
        Text text = Information.transform.Find("TechInfoText").GetComponent<Text>();


        text.text = "" + job + "\n";
        switch (upgrade)
        {
            case Upgrade.attributeUnlock:
                text.text += "특성 " + attribute + " 해금\n";
                break;
            case Upgrade.maxLevel:
                text.text += "최대레벨 " + value + " 증가\n";
                break;
            case Upgrade.averageLevel:
                text.text += "평균레벨 " + value + " 증가\n";
                break;
            case Upgrade.storeNumber:
                text.text += "상점판매 " + value + "명 증가\n";
                break;
            case Upgrade.crookConstantUp:
                text.text += "사기꾼 사기치는 돈 " + value + "원 증가\n";
                break;
            case Upgrade.crookRichPercentageUp:
                text.text += "사기꾼 사기치는 돈 " + value * 100 + "% 증가\n";
                break;
            case Upgrade.crookMyPercentageUp:
                text.text += "사기꾼 " + value * 100 + "% 만큼 더 넘김\n";
                break;
        }
        if (previousTechnologies.Count > 0)
        {
            text.text += "필요 기술 : ";
            for (int i = 0; i < previousTechnologies.Count; i++)
            {
                text.text += previousTechnologies[i].technologyName;
                if (i != previousTechnologies.Count - 1)
                {
                    text.text += ", ";
                }
            }
            text.text += "\n";
        }
    }
}
