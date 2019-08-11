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
        attributeUnlock
    }
    [Header("Required")]
    [SerializeField]
    public List<Technology> previousTechnologies;
    [HideInInspector]
    public bool isResearched;
    [SerializeField]
    public int neededMoney;
    [SerializeField]
    GameObject Information;
    [SerializeField]
    public GameManager.Job job;

    [SerializeField]
    TechInfoButtons techUpButton;

    [Header("기술 정보")]
    [SerializeField]
    public string technologyName;
    [SerializeField]
    public Upgrade upgrade;
    [SerializeField]
    public int maxLevelUp, averageLevelUp, storeNumberUp;
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
                text.text += "최대레벨 " + maxLevelUp + " 증가\n";
                break;
            case Upgrade.averageLevel:
                text.text += "평균레벨 " + averageLevelUp + " 증가\n";
                break;
            case Upgrade.storeNumber:
                text.text += "상점판매 " + storeNumberUp + "명 증가\n";
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
        text.text += "필요 돈 : " + neededMoney;
    }
}
