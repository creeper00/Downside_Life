using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public int type;                                    //0 - 사기꾼용, 1 - 꽃뱀용, 2 - 갱단용
    public int grade;                                   //등급 0 - 일반, 1 - 레어, 2 - 레전
    public int itemCode;                                //아이템 하는 일 0 - 강화, 1 - 유형 변경, 2 - 기타
    public Sprite icon;

    public Item(int type, int grade, int itemCode)
    {
        this.type = type;
        this.grade = grade;
        this.itemCode = itemCode;

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
}
