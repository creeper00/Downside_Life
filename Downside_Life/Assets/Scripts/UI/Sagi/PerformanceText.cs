using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceText : MonoBehaviour
{
    [SerializeField]
    private Text[] performanceText = new Text[4];

    void Start()
    {
        for ( int i = 0; i < 4; ++i )
        {
            performanceText[i].text = string.Format("{0}%", GameManager.instance.sagiPercentage[i]);
        }
    }
}
