using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPop : MonoBehaviour
{
    // Start is called before the first frame update\
    [SerializeField]
    GameObject EventPopup;
    int set = 1;

    // Update is called once per frame
    public void showEventPopup()
    {
        if (set == 1)
        {
            EventPopup.transform.position -= new Vector3(500f, 0f, 0f);
            set = 0;
        }
        else
        {
            EventPopup.transform.position += new Vector3(500f, 0f, 0f);
            set = 1;
        }
    }

}
