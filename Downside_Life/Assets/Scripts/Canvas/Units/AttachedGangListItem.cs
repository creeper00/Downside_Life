using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachedGangListItem : MonoBehaviour
{
    [SerializeField]
    private Sprite strongGang, moneyGang, debuffGang, globalGang, levelMaxGang;

    public void SetGangUnitInformation(GameManager.Gang gang)
    {
        Sprite sprite = gang.GetIcon();
        Image icon = transform.Find("Icon").GetComponent<Image>();
        /*
        switch (gang.type)
        {
            case 0:
                sprite = strongGang;
                break;
            case 1:
                sprite = moneyGang;
                break;
            case 2:
                sprite = debuffGang;
                break;
            case 3:
                sprite = globalGang;
                break;
            case 4:
                sprite = levelMaxGang;
                break;
        }
        */
        icon.sprite = sprite;
    }

}
