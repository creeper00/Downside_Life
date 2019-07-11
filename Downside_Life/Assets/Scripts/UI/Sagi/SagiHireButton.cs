using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SagiHireButton : MonoBehaviour
{
    public int level;
    [SerializeField]
    private Image currentImage;
    [SerializeField]
    private Text currentText;
    public GameObject hireAgreeScreen;

    public void SagiHire()
    {
        if ( GameManager.instance.sagiHired[level - 1])
        {
            //이미 고용됨!!!
        }
        else if ( level > 1 && !GameManager.instance.sagiHired[level-2] )
        {
            //이전 레벨의 사기꾼을 구매해야 함!!!
        }
        else if (GameManager.instance.playerMoney < GameManager.instance.sagiHireCost[level - 1])
        {
            //돈이 부족해!!!
            GameManager.instance.NotEnoughMoney();
        }
        else
        {
            hireAgreeScreen.SetActive(true);
            hireAgreeScreen.transform.Find("FixedText").gameObject.GetComponent<Text>().text
                = string.Format("정말 고용하시겠습니까?");
            hireAgreeScreen.transform.Find("Text").gameObject.GetComponent<Text>().text
                = string.Format("고용 금액: {0}억 원\n성능: 매 턴 부자 돈의\n          {1}% 를 가져옴", GameManager.instance.sagiHireCost[level-1], GameManager.instance.sagiPercentage[level-1]);
            hireAgreeScreen.GetComponent<AgreeScreen>().activity = 0;
            hireAgreeScreen.GetComponent<AgreeScreen>().level = level;
                
        }
    }
}
