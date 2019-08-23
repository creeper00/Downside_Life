using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichHouseManager : MonoBehaviour
{
    public static RichHouseManager instance;

    [SerializeField]
    public string info;
    /* 확률 순서
     * 돈, 직업별아이템, 아이템1개, 아이템 2개 : 현재 50, 50, 40, 10
     */
    
    void Awake()
    {
        GameManager.instance.firstRichHouse.SetActive(true);
    }
    private void Start()
    {
        instance = this;
    }
}
