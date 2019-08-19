using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechManager : MonoBehaviour
{
    public static TechManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    [SerializeField]
    Text skillPointText;

    public void ShowSkillPoint()
    {
        skillPointText.text = "skillpoint : " + GameManager.instance.skillPoint;
    }
}
