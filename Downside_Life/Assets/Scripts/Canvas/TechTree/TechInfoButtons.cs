using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechInfoButtons : MonoBehaviour
{
    GameObject techInfo;
    
    public Technology tech;
    
    enum buttons
    {
        techUpButton,
        techInfoCloseButton,
    }

    public void CloseInfo()
    {
        tech = null;
        GameManager.instance.techInfoCanvas.SetActive(false);
    }

    public void techUp()
    {
        techInfo = GameObject.Find("TechInfo");
        for (int i = 0; i < tech.previousTechnologies.Capacity; i++)
        {
            if (!tech.previousTechnologies[i].isResearched)//선행연구 진행안됨
            {
                Debug.Log("선행연구 진행안됨");
                return;
            }
        }

        if (GameManager.instance.playerMoney < tech.neededMoney)
        {
            return;
        }
        //연구 조건 완료

        tech.isResearched = true;
        GameManager.instance.playerMoney -= tech.neededMoney;

        switch (tech.job)
        {
            case GameManager.Job.crook:
                Debug.Log(tech.job);
                break;
            case GameManager.Job.gang:
                Debug.Log(tech.job);
                break;
            case GameManager.Job.snake:
                Debug.Log(tech.job);
                break;
            case GameManager.Job.robber:
                Debug.Log(tech.job);
                break;
        }
        
        GameManager.instance.showResources();
        tech.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
        tech = null;
        techInfo.SetActive(false);
    }
}
