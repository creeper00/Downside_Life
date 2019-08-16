using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackButton : MonoBehaviour
{

    public void GoOutRichHouse()    // 행동력이 3~5 일때, 도둑질을 하고 난 뒤 도둑 집 밖으로 보내기 위한 함수.
    {
        if(GameManager.instance.stamina < 3) GameManager.instance.ChangeScreen(GameManager.Screen.richHouse);
    }
}
