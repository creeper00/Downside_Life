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
    }

}
