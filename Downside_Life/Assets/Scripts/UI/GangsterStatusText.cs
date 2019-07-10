using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GangsterStatusText : MonoBehaviour
{
    [SerializeField]
    private Text gang1Text;
    [SerializeField]
    private Text gang2Text;
    [SerializeField]
    private Text gang3Text;
    [SerializeField]
    private Text gang4Text;

    public void showGangsterNumber()
    {
        gang1Text.text = string.Format("X{0}", GameManager.instance.gangsterNumber[0]);
        gang2Text.text = string.Format("X{0}", GameManager.instance.gangsterNumber[1]);
        gang3Text.text = string.Format("X{0}", GameManager.instance.gangsterNumber[2]);
        gang4Text.text = string.Format("X{0}", GameManager.instance.gangsterNumber[3]);
    }
}
