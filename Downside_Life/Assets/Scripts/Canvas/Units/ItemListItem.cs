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

        int type = item.type;
        int grade = item.grade;
        int itemCode = item.itemCode;

        //스프라이트 붙이는 코드 작성
        //icon = XXX 하는 식으로 sprite를 붙여넣을 수 있음

        //등급 적는 코드 작성
    }
}
