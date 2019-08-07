using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichHouseManager : MonoBehaviour
{
    public static RichHouseManager instance;

    [SerializeField]
    public List<string> info;//floor + " " + object + " " + percentage + " " + money

    private void Start()
    {
        instance = this;
    }
}
