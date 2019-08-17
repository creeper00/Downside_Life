using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    public void SetFactoryListItem(GameManager.Factory factory)
    {
        transform.Find("Level").GetComponent<Text>().text = factory.level.ToString();
        transform.Find("HealthBar").GetComponent<Transform>().localScale = new Vector3(factory.health / factory.maxhealth, 1, 1);
        transform.Find("HPUI").GetComponent<Text>().text = factory.health + "/" + factory.maxhealth;
        switch(factory.factoryType)
        {
            case GameManager.Factory.FactoryType.bank:
                transform.Find("Type").GetComponent<Text>().text = "은행";
                break;
            case GameManager.Factory.FactoryType.lawyer:
                transform.Find("Type").GetComponent<Text>().text = "변호사 사무소";
                break;
            case GameManager.Factory.FactoryType.normal:
                transform.Find("Type").GetComponent<Text>().text = "일반 공장";
                break;
            case GameManager.Factory.FactoryType.taunt:
                transform.Find("Type").GetComponent<Text>().text = "갈리오 공장";
                break;
            case GameManager.Factory.FactoryType.thief:
                transform.Find("Type").GetComponent<Text>().text = "김연웅 공장";
                break;
        }
        transform.Find("IsUpgrade").GetComponent<Text>().text = factory.isUpgrade ? "업그레이드중" : "";
    }

}
