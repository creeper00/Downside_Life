using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitInformation : MonoBehaviour
{
    public static UnitInformation instance;
    enum Job
    {
        Crook,
        Snake,
        Thief,
        Gang
    }


    [SerializeField]
    private Job whatJob;

    private void Start()
    {
        instance = this;
    }
    
}
