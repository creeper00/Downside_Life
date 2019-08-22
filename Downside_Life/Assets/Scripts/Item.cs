using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public int type;                                    //0 - 사기꾼용, 1 - 꽃뱀용, 2 - 갱단용
    public int grade;                                   //등급 0 - 일반, 1 - 레어, 2 - 레전
    public int itemCode;                                //아이템 하는 일 0 - 강화, 1 - 유형 변경, 2 - 기타
    private Sprite icon = null;

    public Item()
    {
        type = Random.Range(0, 3);
        int temp = Random.Range(0, 10);
        if (temp < 6)
        {
            grade = 0;
        }
        else if (temp < 9)
        {
            grade = 1;
        }
        else
        {
            grade = 2;
        }
        switch (type)
        {
            case 0:
                itemCode = GameManager.instance.crookListItems[grade][Random.Range(0, GameManager.instance.crookListItems[grade].Count)];
                break;
            case 1:
                itemCode = GameManager.instance.snakeListItems[grade][Random.Range(0, GameManager.instance.snakeListItems[grade].Count)];
                break;
            case 2:
                itemCode = GameManager.instance.gangListItems[grade][Random.Range(0, GameManager.instance.gangListItems[grade].Count)];
                break;
        }
        Debug.Log(type + " " + grade + " " + itemCode + " " + getItemName());
    }

    public Item(int type)//get random item
    {
        this.type = type;
        int temp = Random.Range(0, 10);
        if (temp < 6)
        {
            grade = 0;
        }
        else if (temp < 9)
        {
            grade = 1;
        }
        else
        {
            grade = 2;
        }
        switch (type)
        {
            case 0:
                itemCode = GameManager.instance.crookListItems[grade][Random.Range(0, GameManager.instance.crookListItems[grade].Count)];
                break;
            case 1:
                itemCode = GameManager.instance.snakeListItems[grade][Random.Range(0, GameManager.instance.snakeListItems[grade].Count)];
                break;
            case 2:
                itemCode = GameManager.instance.gangListItems[grade][Random.Range(0, GameManager.instance.gangListItems[grade].Count)];
                break;
        }
        Debug.Log(type + " " + grade + " " + itemCode + " " + getItemName());
    }
    
    public Sprite getIcon()
    {
        if ( icon == null )
        {
            string itemName = "";

            if (type == 0)
            {
                itemName += "crook";
                if (grade == 0)
                {
                    itemName += "Normal";
                    if (itemCode == 0)
                    {
                        itemName += "Book";
                    }
                    else if (itemCode == 2)
                    {
                        itemName += "Pocket";
                    }
                }
                else if (grade == 1)
                {
                    itemName += "Rare";
                    if (itemCode == 0)
                    {
                        itemName += "Book";
                    }
                    else if (itemCode == 1)
                    {
                        itemName += "Wax";
                    }
                }
                else if (grade == 2)
                {
                    itemName += "Legendary";
                    if (itemCode == 0)
                    {
                        itemName += "Book";
                    }
                }
            }
            else if (type == 1)
            {
                itemName += "snake";
                if (grade == 0)
                {
                    itemName += "Normal";
                    if (itemCode == 0)
                    {
                        itemName += "Cosmetic";
                    }
                }
                else if (grade == 1)
                {
                    itemName += "Rare";
                    if (itemCode == 0)
                    {
                        itemName += "Cosmetic";
                    }
                    else if (itemCode == 1)
                    {
                        itemName += "OnePiece";
                    }
                }
                else if (grade == 2)
                {
                    itemName += "Legendary";
                    if (itemCode == 0)
                    {
                        itemName += "MagazineDesperate";
                    }
                    else if (itemCode == 1)
                    {
                        itemName += "MagazineCostIncrease";
                    }
                    else if (itemCode == 2)
                    {
                        itemName += "MagazineExtort";
                    }
                }
            }
            else if (type == 2)
            {
                itemName += "gang";
                if (grade == 0)
                {
                    itemName += "Normal";
                    if (itemCode == 0)
                    {
                        itemName += "Pistol";
                    }
                    else if (itemCode == 2)
                    {
                        itemName += "MoneyBag";
                    }
                }
                else if (grade == 1)
                {
                    itemName += "Rare";
                    if (itemCode == 0)
                    {
                        itemName += "SMG";
                    }
                    else if (itemCode == 1)
                    {
                        itemName += "Henna";
                    }
                    else if (itemCode == 2)
                    {
                        itemName += "StunGun";
                    }
                }
                else if (grade == 2)
                {
                    itemName += "Legendary";
                    if (itemCode == 0)
                    {
                        itemName += "Sekom";
                    }
                    else if (itemCode == 1)
                    {
                        itemName += "Aggro";
                    }
                    else if (itemCode == 2)
                    {
                        itemName += "Attorny";
                    }
                    else if (itemCode == 3)
                    {
                        itemName += "Stock";
                    }
                    else if (itemCode == 4)
                    {
                        itemName += "Normal";
                    }
                }
            }

            icon = Resources.Load<Sprite>("UIImages/ItemIcons/" + itemName);
        }
        return icon;
    }

    public string getItemName()
    {
        string id = type.ToString() + grade.ToString() + itemCode.ToString();
        Debug.Log(id);
        switch (id)
        {
            case "000":
                return "사기꾼의 정석";
            case "001":
                return "사기꾼 공짜 고용";
            case "002":
                return "돈주머니";
            case "010":
                return "사기꾼의 정석 (파란색)";
            case "011":
                return "왁스";
            case "012":
                return null;
            case "020":
                return "사기꾼의 정석 (보라색)";
            case "021":
                return null;
            case "022":
                return null;
            case "100":
                return "화장품";
            case "101":
                return "꽃뱀 한 명 공짜 고용";
            case "102":
                return null;
            case "110":
                return "고급 화장품";
            case "111":
                return "원피스";
            case "112":
                return null;
            case "120":
                return "성인 잡지";
            case "121":
                return null;
            case "122":
                return null;
            case "200":
                return "권총";
            case "201":
                return "갱단 한 명 공짜 고용";
            case "202":
                return "돈 보따리";
            case "210":
                return "기관단총";
            case "211":
                return "헤나";
            case "212":
                return "과전류 장치";
            case "220":
                return "Sekom 설계도";
            case "221":
                return "갈리오 공장 설계도";
            case "222":
                return "변호사 사무소 설계도";
            case "223":
                return "주식 공장 설계도";
            case "224":
                return "일반 공장 설계도";
            default:
                return null;
        }
    }
    public Item(int type, int grade, int itemCode)
    {
        this.type = type;
        this.grade = grade;
        this.itemCode = itemCode;
    }
}
