using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    ScrollRect scrollRect;
   
    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    
}
