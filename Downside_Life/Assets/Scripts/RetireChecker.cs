using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RetireChecker : MonoBehaviour
{
    GameObject retireCheck;
    GameObject retireParent;
    // Start is called before the first frame update
    Button retire;
    int temp = 0;
    void Start()
    {
        retire= this.transform.GetComponent<Button>();
        retire.onClick.AddListener(fClick);
    }
    void fClick()
    {
        if (temp == 0)
        {
            retireParent = GameObject.Find("Retire");
            retireCheck = retireParent.transform.Find("RetireCheck").gameObject;
            retireCheck.SetActive(true);
            temp = 1;
        }
        else
        {
            Debug.Log("okay");
            RetireOn.instance.yesClick();
            GameObject.Find("RetireCheck").SetActive(false);
            temp = 0;
        }
    }
}
