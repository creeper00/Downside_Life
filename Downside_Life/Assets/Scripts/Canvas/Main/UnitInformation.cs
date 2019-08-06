using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInformation : MonoBehaviour
{
    enum Job
    {
        Crook,
        Snake,
        Thief,
        Gang
    }

    [SerializeField]
    private Job whatJob;
}
