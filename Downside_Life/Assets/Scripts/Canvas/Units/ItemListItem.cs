using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListItem : MonoBehaviour
{
    [HideInInspector]
    public int index;
    [SerializeField]
    private Sprite crookNormal1, crookNormal2, crookRare1, crookRare2, crookRare3, crookLengendary;
    [SerializeField]
    private Sprite snakeNormal1, snakeNormal2, snakeRare1, snakeRare2, snakeLengendary;
    [SerializeField]
    private Sprite gangNormal1, gangNormal2, gangNormal3, gangRare1, gangRare2, gangRare3, gangLengendary;

    public void SetItemInformation(int index, Item item)
    {
        this.index = index;


        Sprite icon = transform.Find("Icon").GetComponent<Image>().sprite;

        int code = 100 * item.type + 10 * item.grade + item.itemcode;
        switch(code)
        {
            case 000:
                icon = crookNormal1;
                break;
            case 001:
                icon = crookNormal2;
                break;
            case 010:
                icon = crookRare1;
                break;
            case 011:
                icon = crookRare2;
                break;
            case 012:
                icon = crookRare3;
                break;
            case 020:
                icon = crookLengendary;
                break;
            case 100:
                icon = snakeNormal1;
                break;
            case 101:
                icon = snakeNormal2;
                break;
            case 110:
                icon = snakeRare1;
                break;
            case 111:
                icon = snakeRare2;
                break;
            case 120:
                icon = snakeLengendary;
                break;
            case 200:
                icon = gangNormal1;
                break;
            case 201:
                icon = gangNormal2;
                break;
            case 202:
                icon = gangNormal3;
                break;
            case 210:
                icon = gangRare1;
                break;
            case 211:
                icon = gangRare2;
                break;
            case 212:
                icon = gangRare3;
                break;
            case 220:
                icon = gangLengendary;
                break;
        }
    }
}
