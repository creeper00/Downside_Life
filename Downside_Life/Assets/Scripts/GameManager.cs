using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    //Scrollview.cs 라는 script가 있을 때
    //public Scrollview scrollview;

    //ArrayList는 할당을 해 줘야지 보임

    public static GameManager instance;

    public enum RoundState
    {
        one, two, three
    }

    public enum Endings
    {
        success, tooBig, dropped
    }

    public enum Screen
    {
        main, crook, snake, gang, robber
    }

    public Screen currentScreen;

    //부자의 스탯
    public int richInitialMoney;
    private int richMoney, richSalary;
    public double richDesperate;
    [SerializeField]
    private int richMoneyBound;
    [SerializeField]
    private double richDesperateBound;

    public GameObject richMoneyBar;

    private Vector3 cameraPositionMain, cameraPositionCrook, cameraPositionSnake, cameraPositionGang, cameraPositionRobber;

    private void Awake()
    {
        instance = this;

        currentScreen = Screen.main;

        cameraPositionMain = GameObject.Find("main").GetComponent<Transform>().position;

        richMoney = richInitialMoney;

        richSalary = -100;
    }

    void Start()
    {

    }

    public void EndTurn()
    {
        
        int lastRichMoney = richMoney;
        //richSalary = 
        richMoney += richSalary;


        richDesperate += (double)(lastRichMoney - richMoney) / lastRichMoney;

        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);

        if (richMoney >= richMoneyBound) Ending(Endings.tooBig);
        if (richDesperate >= richDesperateBound) Ending(Endings.dropped);
    }

    public void Ending(Endings ending)
    {
        switch(ending)
        {
            case Endings.success:
                //부자 돈 0으로 만들기 성공!
                break;
            case Endings.tooBig:
                //부자가 너무 성장했다
                break;
            case Endings.dropped:
                break;
        }
    }

}

