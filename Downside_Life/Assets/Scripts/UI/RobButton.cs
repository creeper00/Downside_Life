using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobButton : MonoBehaviour
{
    enum Upgrade
    {
        MoneySkill, SuccessSkill
    }
    [SerializeField]
    private Upgrade upgrade;
    [SerializeField]
    private int floorLevel;
    [SerializeField]
    private GameObject miniWindow;
    
    public void OnClickUpgradeButton()
    {
        switch(upgrade)
        {
            case Upgrade.MoneySkill:
                GameManager.instance.playerRobMoneySkill++;
                GameManager.instance.playerStatusText.showPlayerRobSkillStatus(GameManager.instance.playerRobMoneySkill, GameManager.instance.playerRobSuccessSkill);
                break;
            case Upgrade.SuccessSkill:
                GameManager.instance.playerRobSuccessSkill++;
                GameManager.instance.playerStatusText.showPlayerRobSkillStatus(GameManager.instance.playerRobMoneySkill, GameManager.instance.playerRobSuccessSkill);
                break;
        }
    }

    public void OnClickYesButton()
    {
        if (GameManager.instance.floorSuccessRate[GameManager.instance.floorLevel].successRate[GameManager.instance.playerRobMoneySkill-1] >= Random.value * 100)
        {
            GameManager.instance.playerMoney += GameManager.instance.floorMoney[GameManager.instance.floorLevel];
        }
        else
        {
            GameManager.instance.friendy -= GameManager.instance.failFriendyDecrease;
        }
        miniWindow.SetActive(false);
    }
    public void OnClickNoButton()
    {
        miniWindow.SetActive(false);
    }
    public void OnClickRichHouseButton()
    {
        GameManager.instance.robStatusText.ShowRobStatus(GameManager.instance.floorMoney[floorLevel-1], GameManager.instance.floorSuccessRate[floorLevel-1].successRate[GameManager.instance.playerRobSuccessSkill-1]);
        GameManager.instance.floorLevel = floorLevel-1;
        miniWindow.SetActive(true);
    }
}
