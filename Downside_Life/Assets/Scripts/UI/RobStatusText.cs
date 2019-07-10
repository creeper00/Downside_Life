using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobStatusText : MonoBehaviour
{
    [SerializeField]
    private Text ExpectedMoney;
    [SerializeField]
    private Text ExpectedPercentage;

    public void ShowRobStatus(int money, int percentage)
    {
        ExpectedMoney.text = string.Format("예상 확률 : {0}%", percentage);
        ExpectedPercentage.text = string.Format("예상 금액 : {0}원", money);
    }
}
