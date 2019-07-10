using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private TextMesh playerStatus;

    public void showPlayerStatus(int playerMoney)
    {
        playerStatus.text = string.Format("현재자산 : {0}원", playerMoney);
    }
}
