using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPopUpCloseButton : MonoBehaviour
{
    [SerializeField]
    public GameObject shouldClose;
    public void GeneralPopUpClose()
    {
        shouldClose.SetActive(false);
    }
}
