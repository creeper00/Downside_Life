using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{

    public void ApplyFactoryLevel(GameManager.Factory factory)
    {
        switch(factory.factoryType)
        {
            case GameManager.Factory.FactoryType.bank:
                GameObject temp = transform.Find("FactoryBank").gameObject;
                if (factory.level == 0)
                {
                    temp.transform.Find("FactoryBank1").gameObject.SetActive(true);
                    temp.transform.Find("FactoryBank2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank5").gameObject.SetActive(false);
                }
                else if( factory.level ==1)
                {
                    temp.transform.Find("FactoryBank1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank2").gameObject.SetActive(true);
                    temp.transform.Find("FactoryBank3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank5").gameObject.SetActive(false);
                }
                else if (factory.level == 2)
                {
                    temp.transform.Find("FactoryBank1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank3").gameObject.SetActive(true);
                    temp.transform.Find("FactoryBank4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank5").gameObject.SetActive(false);
                }
                else if (factory.level == 3)
                {
                    temp.transform.Find("FactoryBank1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank4").gameObject.SetActive(true);
                    temp.transform.Find("FactoryBank5").gameObject.SetActive(false);
                }
                else if (factory.level == 4)
                {
                    temp.transform.Find("FactoryBank1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryBank5").gameObject.SetActive(true);
                }
                break;
            case GameManager.Factory.FactoryType.lawyer:
                temp = transform.Find("FactoryLawyer").gameObject;
                if (factory.level == 0)
                {
                    temp.transform.Find("FactoryLawyer1").gameObject.SetActive(true);
                    temp.transform.Find("FactoryLawyer2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer5").gameObject.SetActive(false);
                }
                else if (factory.level == 1)
                {
                    temp.transform.Find("FactoryLawyer1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer2").gameObject.SetActive(true);
                    temp.transform.Find("FactoryLawyer3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer5").gameObject.SetActive(false);
                }
                else if (factory.level == 2)
                {
                    temp.transform.Find("FactoryLawyer1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer3").gameObject.SetActive(true);
                    temp.transform.Find("FactoryLawyer4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer5").gameObject.SetActive(false);
                }
                else if (factory.level == 3)
                {
                    temp.transform.Find("FactoryLawyer1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer4").gameObject.SetActive(true);
                    temp.transform.Find("FactoryLawyer5").gameObject.SetActive(false);
                }
                else if (factory.level == 4)
                {
                    temp.transform.Find("FactoryLawyer1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryLawyer5").gameObject.SetActive(true);
                }
                break;
            case GameManager.Factory.FactoryType.normal:
                temp = transform.Find("FactoryNormal").gameObject;
                if (factory.level == 0)
                {
                    temp.transform.Find("FactoryNormal1").gameObject.SetActive(true);
                    temp.transform.Find("FactoryNormal2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal5").gameObject.SetActive(false);
                }
                else if (factory.level == 1)
                {
                    temp.transform.Find("FactoryNormal1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal2").gameObject.SetActive(true);
                    temp.transform.Find("FactoryNormal3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal5").gameObject.SetActive(false);
                }
                else if (factory.level == 2)
                {
                    temp.transform.Find("FactoryNormal1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal3").gameObject.SetActive(true);
                    temp.transform.Find("FactoryNormal4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal5").gameObject.SetActive(false);
                }
                else if (factory.level == 3)
                {
                    temp.transform.Find("FactoryNormal1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal4").gameObject.SetActive(true);
                    temp.transform.Find("FactoryNormal5").gameObject.SetActive(false);
                }
                else if (factory.level == 4)
                {
                    temp.transform.Find("FactoryNormal1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryNormal5").gameObject.SetActive(true);
                }

                break;
            case GameManager.Factory.FactoryType.taunt:
                temp = transform.Find("FactoryTaunt").gameObject;
                if (factory.level == 0)
                {
                    temp.transform.Find("FactoryTaunt1").gameObject.SetActive(true);
                    temp.transform.Find("FactoryTaunt2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt5").gameObject.SetActive(false);
                }
                else if (factory.level == 1)
                {
                    temp.transform.Find("FactoryTaunt1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt2").gameObject.SetActive(true);
                    temp.transform.Find("FactoryTaunt3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt5").gameObject.SetActive(false);
                }
                else if (factory.level == 2)
                {
                    temp.transform.Find("FactoryTaunt1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt3").gameObject.SetActive(true);
                    temp.transform.Find("FactoryTaunt4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt5").gameObject.SetActive(false);
                }
                else if (factory.level == 3)
                {
                    temp.transform.Find("FactoryTaunt1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt4").gameObject.SetActive(true);
                    temp.transform.Find("FactoryTaunt5").gameObject.SetActive(false);
                }
                else if (factory.level == 4)
                {
                    temp.transform.Find("FactoryTaunt1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryTaunt5").gameObject.SetActive(true);
                }

                break;
            case GameManager.Factory.FactoryType.thief:
                temp = transform.Find("FactoryThief").gameObject;
                if (factory.level == 0)
                {
                    temp.transform.Find("FactoryThief1").gameObject.SetActive(true);
                    temp.transform.Find("FactoryThief2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief5").gameObject.SetActive(false);
                }
                else if (factory.level == 1)
                {
                    temp.transform.Find("FactoryThief1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief2").gameObject.SetActive(true);
                    temp.transform.Find("FactoryThief3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief5").gameObject.SetActive(false);
                }
                else if (factory.level == 2)
                {
                    temp.transform.Find("FactoryThief1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief3").gameObject.SetActive(true);
                    temp.transform.Find("FactoryThief4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief5").gameObject.SetActive(false);
                }
                else if (factory.level == 3)
                {
                    temp.transform.Find("FactoryThief1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief4").gameObject.SetActive(true);
                    temp.transform.Find("FactoryThief5").gameObject.SetActive(false);
                }
                else if (factory.level == 4)
                {
                    temp.transform.Find("FactoryThief1").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief2").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief3").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief4").gameObject.SetActive(false);
                    temp.transform.Find("FactoryThief5").gameObject.SetActive(true);
                }

                break;
        }
    }
    public void SetFactoryListItem(GameManager.Factory factory)
    {
        transform.Find("Level").GetComponent<Text>().text = (factory.level+1).ToString();
        transform.Find("HealthBar").GetComponent<Transform>().localScale = new Vector3((float)factory.health / (float)factory.maxhealth, 1, 1);
        transform.Find("HPUI").GetComponent<Text>().text = factory.health + "/" + factory.maxhealth;
        switch(factory.factoryType)
        {
            case GameManager.Factory.FactoryType.bank:
                transform.Find("Type").GetComponent<Text>().text = "은행";
                transform.Find("FactoryBank").gameObject.SetActive(true);
                break;
            case GameManager.Factory.FactoryType.lawyer:
                transform.Find("Type").GetComponent<Text>().text = "변호사 사무소";
                transform.Find("FactoryLawyer").gameObject.SetActive(true);
                break;
            case GameManager.Factory.FactoryType.normal:
                transform.Find("Type").GetComponent<Text>().text = "일반 공장";
                transform.Find("FactoryNormal").gameObject.SetActive(true);
                break;
            case GameManager.Factory.FactoryType.taunt:
                transform.Find("Type").GetComponent<Text>().text = "갈리오 공장";
                transform.Find("FactoryTaunt").gameObject.SetActive(true);
                break;
            case GameManager.Factory.FactoryType.thief:
                transform.Find("Type").GetComponent<Text>().text = "김연웅 공장";
                transform.Find("FactoryThief").gameObject.SetActive(true);
                break;
        }
        ApplyFactoryLevel(factory);
        transform.Find("Status").GetComponent<Text>().text = factory.isUpgrade ? "업그레이드중" : "";
        transform.Find("Status").GetComponent<Text>().text = factory.isConquered > 0 ? "점령중" : transform.Find("Status").GetComponent<Text>().text;
    }

}
