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
        int tempMoney = 0;
        bool greatSuccess = false;
        List<Item> items = new List<Item>();
        bool thiefTwice = Random.Range(0, 100) < GameManager.instance.stealTwicePercentage;

        for (int i=0; i<(thiefTwice ? 2 : 1); i++)
        {
            int ratio = 1;
            int lowerBound = GameManager.instance.thiefStealMoneyLowerBound + (int)((GameManager.instance.thiefStealMoneyUpperBound - GameManager.instance.thiefStealMoneyLowerBound) * (GameManager.instance.rangeDecrease)) + GameManager.instance.additionalMoney;
            int upperBound = GameManager.instance.thiefStealMoneyUpperBound + GameManager.instance.additionalMoney;
            Debug.Log(lowerBound + " " + upperBound);
            if (Random.Range(0, 100) < GameManager.instance.thiefSuccessPercentage)
            {
                //터는데 성공함
                if (floor == 0)
                { // 돈만 있는 floor
                    if (Random.Range(0, 100) < GameManager.instance.thiefGreatSuccessPercentage)
                    {
                        greatSuccess = true;
                        ratio = 2;
                    }
                    tempMoney += Random.Range(lowerBound, upperBound) * ratio;
                }
                else if (floor > 0 && floor < 4)
                {
                    if (GameManager.instance.isItemFloor || Random.Range(0, 100) < jobItemPercentage) { // 아이템을 털었을 때
                        switch(floor)
                        {
                            case 1:
                                Debug.Log("steal crook item");
                                //사기꾼 아이템
                                break;
                            case 2:
                                Debug.Log("steal snake item");
                                //꽃뱀 아이템
                                break;
                            case 3:
                                Debug.Log("steal gang item");
                                //갱단 아이템
                                break;
                        }
                    }
                    else {
                        if (Random.Range(0, 100) < GameManager.instance.thiefGreatSuccessPercentage)
                        {
                            greatSuccess = true;
                            ratio = 2;
                        }
                        tempMoney += Random.Range(lowerBound, upperBound) * ratio;
                    }
                }
                else if (floor == 4)
                {
                    int percentage = Random.Range(0, 100);
                    if (percentage < randomItemPercentage)
                    {
                        Debug.Log("steal item");
                        //아이템 하나 털음
                    }
                    else if (percentage < randomItemPercentage + randomDoubleItemPercentage)
                    {
                        Debug.Log("steal double item");
                        //아이템 두개 털음
                    }
                    else
                    {
                        if (Random.Range(0, 100) < GameManager.instance.thiefGreatSuccessPercentage)
                        {
                            greatSuccess = true;
                            ratio = 2;
                        }
                        tempMoney += Random.Range(lowerBound, upperBound) * ratio;
                    }
                }
            }
        }

        //출력하는 부분
        thiefResultText.text = "";
        thiefResultText2.text = "";
        thiefResultText.text += " 도둑질";
        if (thiefTwice)
        {
            thiefResultText.text += " 두 번";
        }
        if (greatSuccess)
        {
            thiefResultText.text += " 대박";
        }
        if (tempMoney > 0 || items.Count > 0)
        {
            thiefResultText.text += " 성공!";
        }
        else
        {
            thiefResultText.text += " 실패!";
        }

        if (tempMoney > 0)
        {
            thiefResultText2.text = tempMoney.ToString() + "원을 훔쳐왔다!";
            if (items.Count != 0)
            {
                thiefResultText2.text += "\n 그리고";
                for (int i = 0; i < items.Count; i++)
                {
                    thiefResultText2.text += items[i].name + " ";
                }
                thiefResultText2.text += "를 훔쳐왔다!";
            }
        }
        else
        {
            if (items.Count != 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    thiefResultText2.text += items[i].name + " ";
                }
                thiefResultText2.text += "를 훔쳐왔다!";
            }
        }

        //실제 적용하는 부분
        GameManager.instance.playerMoney += tempMoney;
        for (int i=0; i<items.Count; i++)
        {
            switch(items[i].type)
            {
                case 0:
                    GameManager.instance.crookItems.Add(items[i]);
                    break;
                case 1:
                    GameManager.instance.snakeItems.Add(items[i]);
                    break;
                case 2:
                    GameManager.instance.gangItems.Add(items[i]);
                    break;
            }
        }
        GameManager.instance.UpdateResourcesUI();
        GameObject.Find("ThiefInfo").SetActive(false);
        thiefResultPanel.SetActive(true);
    }
}