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
            if (Random.Range(0, 100) < GameManager.instance.thiefSuccessPercentage * GameManager.instance.robberSuccessRateMultiplyByEvent)
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
                    Item item;
                    if (GameManager.instance.isItemFloor || Random.Range(0, 100) < jobItemPercentage) { // 아이템을 털었을 때
                        switch(floor)
                        {
                            case 1:
                                item = new Item(0);
                                if (item.grade == 0 && item.itemCode == 1)
                                {
                                    //사기꾼 공짜 고용
                                } else if (item.getItemName() != null)
                                {
                                    items.Add(item);
                                }
                                Debug.Log("steal crook item");
                                //사기꾼 아이템
                                break;
                            case 2:
                                item = new Item(1);
                                if (item.grade == 0 && item.itemCode == 1)
                                {
                                    //꽃뱀 공짜 고용
                                } else if (item.getItemName() != null)
                                {
                                    items.Add(item);
                                }
                                Debug.Log("steal snake item");
                                //꽃뱀 아이템
                                break;
                            case 3:
                                item = new Item(2);
                                if (item.grade == 0 && item.itemCode == 1)
                                {
                                    //갱단 공짜 고용
                                } else if (item.getItemName() != null)
                                {
                                    items.Add(item);
                                }
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
                    int percentage = Random.Range(0, GameManager.instance.isItemFloor? randomItemPercentage + randomDoubleItemPercentage : 100);
                    if (percentage < randomItemPercentage)
                    {
                        Debug.Log("steal item");
                        Item item = new Item();
                        if ((item.grade == 0 && item.itemCode == 1))
                        {
                            //공짜 고용
                        } else if (item.getItemName() != null)
                        {
                            items.Add(item);
                        }
                        
                        //아이템 하나 털음
                    }
                    else if (percentage < randomItemPercentage + randomDoubleItemPercentage)
                    {
                        Debug.Log("steal double item");
                        Item item1 = new Item();
                        Item item2 = new Item();
                        if ((item1.grade == 0 && item1.itemCode == 1))
                        {
                            //공짜 고용
                        } else if (item1.getItemName() != null)
                        {
                            items.Add(item1);
                        }
                        if ((item2.grade == 0 && item2.itemCode == 1))
                        {
                            //공짜 고용
                        } else if (item2.getItemName() != null)
                        {
                            items.Add(item2);
                        }
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
            thiefResultText2.text = tempMoney.ToString() + "만 원을 훔쳐왔다!";
            if (items.Count != 0)
            {
                thiefResultText2.text += "\n 그리고";
                for (int i = 0; i < items.Count; i++)
                {
                    thiefResultText2.text += items[i].getItemName() + " ";
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
                    thiefResultText2.text += items[i].getItemName() + " ";
                }
                thiefResultText2.text += "를 훔쳐왔다!";
            }
        }

        //실제 적용하는 부분
        GameManager.instance.playerMoney += tempMoney;
        GameManager.instance.ChangeRichMoney(tempMoney,true);
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
        GameManager.instance.SetStamina(GameManager.instance.stamina - GameManager.instance.StealStaminaDecrease);
        GameManager.instance.UpdateResourcesUI();
        GameObject.Find("ThiefInfo").SetActive(false);
        thiefResultPanel.SetActive(true);
    }
}