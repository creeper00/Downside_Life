using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThiefActionButton : MonoBehaviour
{
    [HideInInspector]
    public int money;
    [HideInInspector]
    public int percentage;
    [SerializeField]
    public Text thiefResultText, thiefResultText2;
    [SerializeField]
    public GameObject thiefResultPanel;
    public void doThief()
    {
        if(GameManager.instance.stamina < 3)
        {
            return;
        }
        GameManager.instance.stamina -= 3;
        if (Random.Range(0, 100) < percentage)
        {
            GameManager.instance.playerMoney += money;
            thiefResultText.text = "도둑질 성공!";
            thiefResultText2.text = money.ToString() + "원을 훔쳐왔다!";
        }
        else
        {
            thiefResultText.text = "도둑질 실패!";
            thiefResultText2.text = "";
        }

        GameManager.instance.UpdateResourcesUI();
        GameObject.Find("ThiefInfo").SetActive(false);
        thiefResultPanel.SetActive(true);
    }
}
