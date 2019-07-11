using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : MonoBehaviour
{
    [SerializeField]
    private AgreeScreen agreeScreen;

    public void ClickNo()
    {
        agreeScreen.DontDoIt();
    }
}
