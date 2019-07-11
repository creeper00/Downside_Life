using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusText : MonoBehaviour
{
    [SerializeField]
    private Text playerMoneyText;
    [SerializeField]
    private Text MoneyLevelText;
    [SerializeField]
    private Text SuccessLevelText;

    public void showPlayerStatus(int playerMoney)
    {
        this.playerMoneyText.text = string.Format("현재자산 : {0}억 원", playerMoney);
    }
    public void showPlayerRobSkillStatus(int playerRobMoneySkill, int playerRobSuccessSkill)
    {
        MoneyLevelText.text = string.Format("LV{0}", playerRobMoneySkill);
        SuccessLevelText.text = string.Format("LV{0}", playerRobSuccessSkill);
    }
}
