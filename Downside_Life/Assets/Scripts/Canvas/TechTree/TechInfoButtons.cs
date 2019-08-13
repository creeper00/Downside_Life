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

        if (GameManager.instance.playerMoney < tech.neededMoney)//돈 모자람
        {
            Debug.Log("돈 모자람");
            return;
        }
        //연구 조건 완료

        tech.isResearched = true;
        GameManager.instance.playerMoney -= tech.neededMoney;

        switch (tech.job)
        {
            case GameManager.Job.crook:
                switch (tech.upgrade)
                {
                    case Technology.Upgrade.maxLevel:
                        GameManager.instance.crookMaxLevel += tech.value;
                        break;
                    case Technology.Upgrade.averageLevel:
                        GameManager.instance.crookAverageLevel += tech.value;
                        break;
                    case Technology.Upgrade.storeNumber:
                        GameManager.instance.crookStoreSellingNumber += tech.value;
                        break;
                    case Technology.Upgrade.crookConstantUp:
                        GameManager.instance.crookTechConstantIncrease += tech.value;
                        break;
                    case Technology.Upgrade.crookRichPercentageUp:
                        GameManager.instance.crookTechRichPercentageIncrease += tech.value;
                        break;
                    case Technology.Upgrade.crookMyPercentageUp:
                        GameManager.instance.crookTechMyPercentageIncrease += tech.value;
                        break;
                    default:
                        Debug.Log("사기꾼에 잘못된 테크트리가 기입되었습니다");
                        break;
                }
                break;
            case GameManager.Job.gang:
                switch (tech.upgrade)
                {
                    case Technology.Upgrade.maxLevel:
                        GameManager.instance.gangMaxLevel += tech.value;
                        break;
                    case Technology.Upgrade.attributeUnlock:
                        GameManager.instance.gangAttributes.Add(tech.attribute);
                        break;
                    case Technology.Upgrade.averageLevel:
                        GameManager.instance.gangAverageLevel += tech.value;
                        break;
                    case Technology.Upgrade.storeNumber:
                        GameManager.instance.gangStoreSellingNumber += tech.value;
                        break;
                    default:
                        Debug.Log("갱단에 잘못된 테크트리가 기입되었습니다");
                        break;
                }
                break;
            case GameManager.Job.snake:
                switch (tech.upgrade)
                {
                    case Technology.Upgrade.maxLevel:
                        GameManager.instance.snakeMaxLevel += tech.value;
                        break;
                    case Technology.Upgrade.attributeUnlock:
                        GameManager.instance.snakeAttributes.Add(tech.attribute);
                        break;
                    case Technology.Upgrade.averageLevel:
                        GameManager.instance.snakeAverageLevel += tech.value;
                        break;
                    case Technology.Upgrade.storeNumber:
                        GameManager.instance.snakeStoreSellingNumber += tech.value;
                        break;
                    default:
                        Debug.Log("꽃뱀에 잘못된 테크트리가 기입되었습니다");
                        break;
                }
                break;
            case GameManager.Job.robber:
                break;
        }
        
        GameManager.instance.UpdateResourcesUI();
        tech.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
        tech = null;
        techInfo.SetActive(false);
    }
}
