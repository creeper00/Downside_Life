using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesButton : MonoBehaviour
{
    [SerializeField]
    private AgreeScreen agreeScreen;

    public void ClickYes()
    {
        agreeScreen.DoIt();
    }
}
