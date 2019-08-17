using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Technology : MonoBehaviour
{
    [HideInInspector]
    public bool isResearched;
    [SerializeField]
    GameManager.Job whatJob;
    [SerializeField]
    int skillPointNeededNum;
    [SerializeField]
    TechButtons yesButton, noButton;
    [SerializeField]
    GameObject techInfo;
    int skillLevel;
    int maxSkillLevel;

    public bool canResearch()
    {
        switch(whatJob)
        {
            case GameManager.Job.crook:
                if (skillPointNeededNum > GameManager.instance.crookSkillPoint)
                {
                    return false;
                }
                break;
            case GameManager.Job.gang:
                if (skillPointNeededNum > GameManager.instance.gangSkillPoint)
                {
                    return false;
                }
                break;
            case GameManager.Job.snake:
                if (skillPointNeededNum > GameManager.instance.snakeSkillPoint)
                {
                    return false;
                }
                break;
            case GameManager.Job.robber:
                if (skillPointNeededNum > GameManager.instance.robberSkillPoint)
                {
                    return false;
                }
                break;
        }
        return true;
    }

    public void openTechInfo()
    {
        if (canResearch())
        {

            techInfo.SetActive(true);
            yesButton.technology = this;
        }
    }
}
