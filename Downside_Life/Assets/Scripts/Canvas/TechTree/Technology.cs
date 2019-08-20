using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Technology : MonoBehaviour
{
    [HideInInspector]
    public bool isResearched;
    [SerializeField]
    public GameManager.Job whatJob;
    [SerializeField]
    public int skillPointNeededNum;
    public int skillLevel;
    [SerializeField]
    int maxSkillLevel;
    [HideInInspector]
    public int temporaryLevel;
    public int tier;

    public void Start()
    {
        showTechnology();
    }
    public bool CanResearch()
    {
        Debug.Log(skillPointNeededNum + " " + TechManager.instance.temporaryJobSkillPoint[(int)whatJob]);
        
        if (skillPointNeededNum > TechManager.instance.temporaryJobSkillPoint[(int)whatJob] || TechManager.instance.temporarySkillPoint < 1)
        {
            return false;
        }
        return true;
    }
    public void ShowTempoaryTechnology()
    {
        transform.Find("LVText").GetComponent<Text>().text = temporaryLevel.ToString();
        showCanTemporarySkillPoint();
    }
    public void showTechnology()
    {
        transform.Find("LVText").GetComponent<Text>().text = skillLevel.ToString();
        showCanSkillPoint();
    }
    void showCanSkillPoint()//스킬포인트 + -를 보여주는 부분
    {
        Debug.Log(CanResearch() + "tier : " + tier);
        transform.Find("Minus").gameObject.SetActive(skillLevel != temporaryLevel);
        transform.Find("Plus").gameObject.SetActive(TechManager.instance.temporarySkillPoint > 0 && skillLevel != maxSkillLevel && (CanResearch() || TechManager.instance.tier[(int)whatJob] == tier));
    }
    void showCanTemporarySkillPoint()
    {
        transform.Find("Minus").gameObject.SetActive(temporaryLevel != skillLevel);
        transform.Find("Plus").gameObject.SetActive(TechManager.instance.temporarySkillPoint > 0 && temporaryLevel != maxSkillLevel && (CanResearch() || TechManager.instance.tier[(int)whatJob] == tier));
    }
    public void confirmSkillLevel()
    {
        skillLevel = temporaryLevel;
    }
    public void ResetSkillLevel()
    {
        temporaryLevel = skillLevel;
    }
}
