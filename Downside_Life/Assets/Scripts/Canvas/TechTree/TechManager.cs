using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechManager : MonoBehaviour
{
    public static TechManager instance;
    [HideInInspector]
    public int[] temporaryJobSkillPoint, neededMaxSkillPoint, tier;
    [HideInInspector]
    public int temporarySkillPoint;
    [SerializeField]
    public List<Technology> crookTechnologies, snakeTechnologies, gangTechnologies, thiefTechnologies;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        tier = new int[4];
        temporaryJobSkillPoint = new int[4];
        neededMaxSkillPoint = new int[4];
        ShowSkillPoint();
    }
    [SerializeField]
    Text skillPointText;

    public int CheckMaxNeededSkillPoint(GameManager.Job job) 
    {
        int temp = -1;
        switch(job)
        {
            case GameManager.Job.crook:
                for (int i=0; i<crookTechnologies.Count; i++)
                {
                    if (crookTechnologies[i].temporaryLevel != 0)
                    {
                        tier[(int)job] = Mathf.Max(tier[(int)job], crookTechnologies[i].tier);
                        temp = Mathf.Max(temp, crookTechnologies[i].skillPointNeededNum + crookTechnologies[i].temporaryLevel);
                    }
                }
                break;
            case GameManager.Job.gang:
                for (int i = 0; i < gangTechnologies.Count; i++)
                {
                    if (gangTechnologies[i].temporaryLevel != 0)
                    {
                        tier[(int)job] = Mathf.Max(tier[(int)job], gangTechnologies[i].tier);
                        temp = Mathf.Max(temp, gangTechnologies[i].skillPointNeededNum + gangTechnologies[i].temporaryLevel);
                    }
                }
                break;
            case GameManager.Job.robber:
                for (int i = 0; i < thiefTechnologies.Count; i++)
                {
                    if (thiefTechnologies[i].temporaryLevel != 0)
                    {
                        tier[(int)job] = Mathf.Max(tier[(int)job], thiefTechnologies[i].tier);
                        temp = Mathf.Max(temp, thiefTechnologies[i].skillPointNeededNum + thiefTechnologies[i].temporaryLevel);
                    }
                }
                break;
            case GameManager.Job.snake:
                for (int i = 0; i < snakeTechnologies.Count; i++)
                {
                    if (snakeTechnologies[i].temporaryLevel != 0)
                    {
                        tier[(int)job] = Mathf.Max(tier[(int)job], snakeTechnologies[i].tier);
                        temp = Mathf.Max(temp, snakeTechnologies[i].skillPointNeededNum + snakeTechnologies[i].temporaryLevel);
                    }
                }
                break;
        }
        Debug.Log(temp);
        return temp;
    }
    public void ShowSkillPoint()
    {
        skillPointText.text = "skillpoint : " + GameManager.instance.skillPoint;
    }
    public void ShowTemporarySkillPoint()
    {
        skillPointText.color = new Color32(255, 0, 0, 255);
        skillPointText.text = "skillpoint : " + temporarySkillPoint;
    }
    public bool CanMinusSkillLevel(Technology technology)
    {
        if (technology.tier == tier[(int)technology.whatJob])
        {
            return true;
        }
        if (CheckMaxNeededSkillPoint(technology.whatJob) == temporaryJobSkillPoint[(int)technology.whatJob])
        {
            return false;
        }
        else return true;
    }
    public void ConfirmSkillPoint()
    {
        for (int i=0; i<4; i++)
        {
            GameManager.instance.jobSkillPoint[i] = temporaryJobSkillPoint[i];
        }
    }

    public void BuySkillPoint()
    {
        GameManager.instance.skillPoint++;
        temporarySkillPoint++;
        ShowTemporarySkillPoint();
    }
}
