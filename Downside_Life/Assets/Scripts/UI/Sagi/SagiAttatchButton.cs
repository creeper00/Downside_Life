using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SagiAttatchButton : MonoBehaviour
{
    public int level;
    [SerializeField]
    private Image currentImage;
    [SerializeField]
    private Text currentText;
    public GameObject hireAgreeScreen;

    public void SagiAttatch()
    {
        if (GameManager.instance.sagiAttatched[level - 1])
        {
            //이미 붙음!!!
        }
        else if ( !GameManager.instance.sagiHired[level - 1])
        {
            //일단 고용해야 함!!!
        }
        else if (level > 1 && !GameManager.instance.sagiAttatched[level - 2])
        {
            //이전 레벨의 사기꾼을 붙여야 함!!!
        }
        else if (GameManager.instance.playerMoney < GameManager.instance.sagiAttatchCost[level - 1])
        {
            //돈이 부족해!!!
            GameManager.instance.NotEnoughMoney();
        }
        else
        {
            hireAgreeScreen.SetActive(true);
            hireAgreeScreen.transform.Find("FixedText").gameObject.GetComponent<Text>().text
                = string.Format("사기꾼을 붙이겠습니까?");
            hireAgreeScreen.transform.Find("Text").gameObject.GetComponent<Text>().text
                = string.Format("요구 금액: {0}억 원", GameManager.instance.sagiAttatchCost[level - 1]);
            hireAgreeScreen.GetComponent<AgreeScreen>().activity = 1;
            hireAgreeScreen.GetComponent<AgreeScreen>().level = level;

        }
    }
}
