using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public GameObject text;
    public GameObject button;
    void Awake()
    {
        panel = GameObject.Find("NotEnoughStaminaPanel");
        panel.SetActive(true);
    }
}
