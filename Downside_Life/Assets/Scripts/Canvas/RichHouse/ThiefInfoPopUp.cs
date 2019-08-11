using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThiefInfoPopUp : MonoBehaviour
{
    enum Seq
    {
        first,
        second,
        third,
        fourth,
        fifth
    }
    [SerializeField]
    public GameObject thiefInfo;
    [SerializeField]
    public Text thiefInfoText;
    [SerializeField]
    Seq whatFloor;
    [SerializeField]
    Seq whatObject;
    [SerializeField]
    ThiefActionButton thiefAction;

    public void ThiefPopUp()
    {
        thiefInfo.SetActive(true);
        getInformation();
    }
    public void getInformation()
    {
        string info = RichHouseManager.instance.info[(int)whatFloor * 3 + (int)whatObject];//일단 하드코딩함
        string[] strings = info.Split(' ');

        int percentage = int.Parse(strings[2]);
        int money = int.Parse(strings[3]);
        
        thiefAction.money = money;
        thiefAction.percentage = percentage;
        thiefInfoText.text = "확률 : " + percentage + "%\n돈 : " + money + "원";
    }
}
