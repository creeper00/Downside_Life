using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThiefActionButton : MonoBehaviour
{
    [HideInInspector]
    public int floor;
    [HideInInspector]
    public int moneyPercentage, jobItemPercentage, randomItemPercentage, randomDoubleItemPercentage;
    [SerializeField]
    public Text thiefResultText, thiefResultText2;
    [SerializeField]
    public GameObject thiefResultPanel;
    public void doThief()
    {
        Debug.Log("job" + jobItemPercentage + "random" + randomItemPercentage + "randomDouble" + randomDoubleItemPercentage + "money" + moneyPercentage);
        if(GameManager.instance.stamina < 3)
        {
            return;
        }
        thiefResultText.text = "";
        GameManager.instance.stamina -= 3;
        if (Random.Range(0, 100) < GameManager.instance.thiefSuccessPercentage)
        {
            
            int ratio = 1;
            bool isMoney = false;
            if (floor == 0)
            {
                isMoney = true;
            }
            if (floor > 0 && floor < 3)//각 직업 아이템 훔쳐옴
            {
                int percentage = Random.Range(0, 100);
                Debug.Log(percentage + " " + jobItemPercentage);
                if (percentage < jobItemPercentage)
                {
                    //아이템 훔쳐오기
                    switch(floor)
                    {
                        case 1:
                            //사기꾼 아이템 훔쳐옴
                            break;
                        case 2:
                            //꽃뱀형 아이템 훔쳐옴
                            break;
                        case 3:
                            //갱단형 아이템 훔쳐옴
                            break;
                    }
                }
                else
                {
                    isMoney = true;
                }
            }
            else if (floor < 4)
            {
                int percentage = Random.Range(0, 100);
                if (percentage < randomItemPercentage)
                {
                    //랜덤 아이템 1개 훔쳐옴
                }
                if (percentage > randomItemPercentage && percentage < randomItemPercentage + randomDoubleItemPercentage)
                {
                    //랜덤 아이템 두개 훔쳐옴
                }
                else
                {
                    isMoney = true;
                }
            }
            if (isMoney)
            {
                if (Random.Range(0, 100) < GameManager.instance.thiefGreatSuccessPercentage)
                {
                    ratio = 2;
                    thiefResultText.text += "대박!\n";
                }
                int stealMoney = GameManager.instance.stealMoney * ratio;
                GameManager.instance.playerMoney += stealMoney;
                GameManager.instance.ChangeRichMoney(stealMoney, true);
                thiefResultText.text += "도둑질 성공!";
                thiefResultText2.text = stealMoney.ToString() + "원을 훔쳐왔다!";
            }
            else
            {
                thiefResultText.text += "도둑질 성공!";
                thiefResultText2.text = "어쩌구저쩌구 아이템을 훔쳐왔다!";
            }
        }
        else
        {
            thiefResultText.text = "도둑질 실패!";
            thiefResultText2.text = "";
        }

        GameManager.instance.UpdateResourcesUI();
        GameObject.Find("ThiefInfo").SetActive(false);
        thiefResultPanel.SetActive(true);
    }
}
