using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListItem : MonoBehaviour
{
    [HideInInspector]
    public int index;
    [SerializeField]
    private Sprite crookNormalBook, crookNormalMoneyPocket, crookRareBook, crookRareWax, crookLegendaryBook;
    [SerializeField]
    private Sprite snakeNormalCosmetic, snakeRareCosmetic, snakeRareOnePiece, snakeLegendaryMagazineDesperate, snakeLegendaryMagazineCostIncrease, snakeLegendaryMagazineExtort;
    [SerializeField]
    private Sprite gangNormalPistol, gangNormalMoneyBotarri, gangRareGun, gangRareHena, gangRareDevice, gangLegendarySekom, gangLegendaryAggro, gangLegendaryAttorny, gangLegendaryStock, gangLegendaryNormal;

    public void SetItemInformation(int index, Item item)
    {
        this.index = index;

        Sprite icon = transform.Find("Icon").GetComponent<Image>().sprite;

        int type = item.type;
        int grade = item.grade;
        int itemCode = item.itemCode;

        //스프라이트 붙이는 코드 작성
        //icon = XXX 하는 식으로 sprite를 붙여넣을 수 있음
        if(type == 0)
        {
            if(grade == 0)
            {
                if(itemCode == 0)
                {
                    icon = crookNormalBook;
                }
                else if(itemCode == 2)
                {
                    icon = crookNormalMoneyPocket;
                }
            }
            else if(grade == 1)
            {
                if (itemCode == 0)
                {
                    icon = crookRareBook;
                }
                else if (itemCode == 1)
                {
                    icon = crookRareWax;
                }
            }
            else if(grade == 2)
            {
                if (itemCode == 0)
                {
                    icon = crookLegendaryBook;
                }
            }
        }
        else if(type == 1)
        {
            if (grade == 0)
            {
                if (itemCode == 0)
                {
                    icon = snakeNormalCosmetic;
                }
            }
            else if (grade == 1)
            {
                if (itemCode == 0)
                {
                    icon = snakeRareCosmetic;
                }
                else if (itemCode == 1)
                {
                    icon = snakeRareOnePiece;
                }
            }
            else if (grade == 2)
            {
                if (itemCode == 0)
                {
                    icon = snakeLegendaryMagazineDesperate;
                }
                else if (itemCode == 1)
                {
                    icon = snakeLegendaryMagazineCostIncrease;
                }
                else if (itemCode == 2)
                {
                    icon = snakeLegendaryMagazineExtort;
                }
            }
        }
        else if(type == 2)
        {
            if (grade == 0)
            {
                if (itemCode == 0)
                {
                    icon = gangNormalPistol;
                }
                else if (itemCode == 2)
                {
                    icon = gangNormalMoneyBotarri;
                }
            }
            else if (grade == 1)
            {
                if (itemCode == 0)
                {
                    icon = gangRareGun;
                }
                else if (itemCode == 1)
                {
                    icon = gangRareHena;
                }
                else if (itemCode == 2)
                {
                    icon = gangRareDevice;
                }
            }
            else if (grade == 2)
            {
                if (itemCode == 0)
                {
                    icon = gangLegendarySekom;
                }
                else if (itemCode == 1)
                {
                    icon = gangLegendaryAggro;
                }
                else if (itemCode == 2)
                {
                    icon = gangLegendaryAttorny;
                }
                else if (itemCode == 3)
                {
                    icon = gangLegendaryStock;
                }
                else if (itemCode == 4)
                {
                    icon = gangLegendaryNormal;
                }
            }
        }
        //등급 적는 코드 작성
    }
}
