using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RichMoneyBar : MonoBehaviour
{
    [SerializeField]
    private int barWidth;
    public void ChangeBar(int current, int max)
    {
        transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(1000f * current / max, 720f);
        transform.GetChild(2).GetComponent<Text>().text = "부자의 재산: " + current + "만 원";
    }
}
