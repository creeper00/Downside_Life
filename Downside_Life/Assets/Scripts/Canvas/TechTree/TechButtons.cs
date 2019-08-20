using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechButtons : MonoBehaviour
{
    public Technology technology;
    Text skillPoint;
    GameObject techInfo;
    [SerializeField]
    GameObject confirmInfo;

    private void Start()
    {
        techInfo = GameObject.Find("TechInfo");
    }
    public void TechUp()
    {
        if (technology.CanResearch())
        {
            technology.temporaryLevel++;
            TechManager.instance.temporaryJobSkillPoint[(int)technology.whatJob]++;
            technology.ShowTempoaryTechnology();
            TechManager.instance.neededMaxSkillPoint[(int)technology.whatJob] = Mathf.Max(technology.skillPointNeededNum + technology.temporaryLevel, TechManager.instance.neededMaxSkillPoint[(int)technology.whatJob]);
            TechManager.instance.temporarySkillPoint--;
            TechManager.instance.tier[(int)technology.whatJob] = technology.tier;
        }
        TechManager.instance.ShowTemporarySkillPoint();
        TechManager.instance.ShowCanSkillPoint();
    }

    public void TechDown()
    {
        if (TechManager.instance.CanMinusSkillLevel(technology) && technology.skillLevel < technology.temporaryLevel)
        {
            technology.temporaryLevel--;
            TechManager.instance.temporaryJobSkillPoint[(int)technology.whatJob]--;
            technology.ShowTempoaryTechnology();
            TechManager.instance.temporarySkillPoint++;
        }
        TechManager.instance.ShowTemporarySkillPoint();
        TechManager.instance.ShowCanSkillPoint();
    }

    public void TechSkillPointBuy()
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

    public void CloseConfirmInfo()
    {
        confirmInfo.SetActive(false);
        TechManager.instance.ResetSkillPoint();
        if (TechManager.instance.changeCanvas)
        {
            GameManager.instance.ChangeScreen(TechManager.instance.screen);
        }
    }
}
