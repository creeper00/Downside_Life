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
    private Sprite gangNormalPistol, gangNormalMoneyBag, gangRareSMG, gangRareHenna, gangRareStunGun, gangLegendarySekom, gangLegendaryAggro, gangLegendaryAttorny, gangLegendaryStock, gangLegendaryNormal;

    public void SetItemInformation(int index, Item item)
    {
        this.index = index;

        Sprite sprite = null;
        Image icon = transform.Find("Icon").GetComponent<Image>();

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
                    sprite = crookNormalBook;
                }
                else if(itemCode == 2)
                {
                    sprite = crookNormalMoneyPocket;
                }
            }
            else if(grade == 1)
            {
                if (itemCode == 0)
                {
                    sprite = crookRareBook;
                }
                else if (itemCode == 1)
                {
                    sprite = crookRareWax;
                }
            }
            else if(grade == 2)
            {
                if (itemCode == 0)
                {
                    sprite = crookLegendaryBook;
                }
            }
        }
        else if(type == 1)
        {
            if (grade == 0)
            {
                if (itemCode == 0)
                {
                    sprite = snakeNormalCosmetic;
                }
            }
            else if (grade == 1)
            {
                if (itemCode == 0)
                {
                    sprite = snakeRareCosmetic;
                }
                else if (itemCode == 1)
                {
                    sprite = snakeRareOnePiece;
                }
            }
            else if (grade == 2)
            {
                if (itemCode == 0)
                {
                    sprite = snakeLegendaryMagazineDesperate;
                }
                else if (itemCode == 1)
                {
                    sprite = snakeLegendaryMagazineCostIncrease;
                }
                else if (itemCode == 2)
                {
                    sprite = snakeLegendaryMagazineExtort;
                }
            }
        }
        else if(type == 2)
        {
            if (grade == 0)
            {
                if (itemCode == 0)
                {
                    sprite = gangNormalPistol;
                }
                else if (itemCode == 2)
                {
                    sprite = gangNormalMoneyBag;
                }
            }
            else if (grade == 1)
            {
                if (itemCode == 0)
                {
                    sprite = gangRareSMG;
                }
                else if (itemCode == 1)
                {
                    sprite = gangRareHenna;
                }
                else if (itemCode == 2)
                {
                    sprite = gangRareStunGun;
                }
            }
            else if (grade == 2)
            {
                if (itemCode == 0)
                {
                    sprite = gangLegendarySekom;
                }
                else if (itemCode == 1)
                {
                    sprite = gangLegendaryAggro;
                }
                else if (itemCode == 2)
                {
                    sprite = gangLegendaryAttorny;
                }
                else if (itemCode == 3)
                {
                    sprite = gangLegendaryStock;
                }
                else if (itemCode == 4)
                {
                    sprite = gangLegendaryNormal;
                }
            }
        }

        if ( sprite != null )
        {
            icon.sprite = sprite;
        }
        //등급 적는 코드 작성
    }
}
