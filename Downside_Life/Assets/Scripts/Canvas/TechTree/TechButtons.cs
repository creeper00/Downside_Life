using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechButtons : MonoBehaviour
{
    [HideInInspector]
    public Technology technology;
    Text skillPoint;
    GameObject techInfo;

    private void Start()
    {
        techInfo = GameObject.Find("TechInfo");
    }
    public void techUp()
    {
        
        technology.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
        technology.isResearched = true;
        techInfo.SetActive(false);
    }

    public void techInfoClose()
    {
        techInfo.SetActive(false);
    }

    public void techSkillPointBuy()
    {
        int temp = GameManager.instance.skillPointPrice * GameManager.instance.totalSkillPoint;
        if (GameManager.instance.playerMoney > temp)
        {
            GameManager.instance.playerMoney -= temp;
            GameManager.instance.totalSkillPoint++;
            GameManager.instance.skillPoint++;
            TechManager.instance.ShowSkillPoint();
        } else
        {
            //스킬포인트 구매 돈 모자람
        }
    }
}
