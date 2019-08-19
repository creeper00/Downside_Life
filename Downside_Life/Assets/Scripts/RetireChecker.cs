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
    void Start()
    {
        retire= this.transform.GetComponent<Button>();
        retire.onClick.AddListener(fClick);
    }

    void fClick()
    {
        retireParent = GameObject.Find("Retire");
        retireCheck = retireParent.transform.Find("RetireCheck").gameObject;
        retireCheck.SetActive(true);
    }
}
