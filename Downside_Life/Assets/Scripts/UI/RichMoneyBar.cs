using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RichMoneyBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBar(int current, int max)
    {
        transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(1280f * current / max, 720f);
        transform.GetChild(2).GetComponent<Text>().text = "부자의 재산: " + current + "억 원";
    }
}
