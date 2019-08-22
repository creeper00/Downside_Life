using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollMoneyWarn : MonoBehaviour
{
    // Start is called before the first frame update
    Button reroll;
    GameObject MoneyWarn;
    GameObject NoMoney;
    void Start()
    {
        reroll=this.transform.GetComponent<Button>();
        reroll.onClick.AddListener(rerollClick);
        MoneyWarn = GameObject.Find("MoneyWarning");
        NoMoney = MoneyWarn.transform.Find("NotEnoughMoney").gameObject;

    }

    // Update is called once per frame
    void rerollClick()
    {
        if(GameManager.instance.playerMoney<50)
        {
            StartCoroutine("showPopup");
        }
    }
    IEnumerator showPopup()
    {
        NoMoney.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        NoMoney.SetActive(false);
    }
}
