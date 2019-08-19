using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int type;                                    //0 - 사기꾼용, 1 - 꽃뱀용, 2 - 갱단용
    public int grade;                                   //등급 0 - 일반, 1 - 레어, 2 - 레전
    public int itemcode;                                //아이템 하는 일 0 - 강화, 1 - 유형 변경, 2 - 기타
}
