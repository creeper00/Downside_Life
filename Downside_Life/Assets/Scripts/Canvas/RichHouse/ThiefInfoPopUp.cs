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
        GetInformation();
    }
    public void GetInformation()
    {
        string info = RichHouseManager.instance.info;
        string[] strings = info.Split(' ');
        
        thiefAction.floor = (int)whatFloor;
        thiefAction.moneyPercentage = int.Parse(strings[0]);
        thiefAction.jobItemPercentage = int.Parse(strings[1]);
        thiefAction.randomItemPercentage = int.Parse(strings[2]);
        thiefAction.randomDoubleItemPercentage = int.Parse(strings[3]);

        thiefInfoText.text = "돈과 아이템들을 얻을 수 있습니다.\n 성공확률 : " + GameManager.instance.thiefSuccessPercentage;
    }
}
