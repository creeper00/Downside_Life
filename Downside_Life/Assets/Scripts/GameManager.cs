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
        main, techtree, units, store, richHouse
    }

    //카메라 관련
    public GameObject mainCamera;
    private Vector3 cameraPositionMain, cameraPositionTechTree, cameraPositionUnits, cameraPositionStore, cameraPositionRichHouse;
    public Screen currentScreen;

    //부자의 스탯
    public int richInitialMoney;
    private int richMoney, richSalary;
    public double richDesperate;
    [SerializeField]
    private int richMoneyBound;
    [SerializeField]
    private double richDesperateBound;

    //UI
    [SerializeField]
    public GameObject richMoneyBar;
    [HideInInspector]
    public int playerMoney;
    private int turn;
    private GameObject GotoMainButton, GotoTechTreeButton, GotoUnitsButton, GotoStoreButton, GotoRichHouseButton;

    private void Awake()
    {
        instance = this;
        turn = 0;

        currentScreen = Screen.main;

        cameraPositionMain = GameObject.Find("Main").GetComponent<Transform>().position;
        cameraPositionTechTree = GameObject.Find("TechTree").GetComponent<Transform>().position;
        cameraPositionUnits = GameObject.Find("Units").GetComponent<Transform>().position;
        cameraPositionStore = GameObject.Find("Store").GetComponent<Transform>().position;
        cameraPositionRichHouse = GameObject.Find("RichHouse").GetComponent<Transform>().position;

        GotoMainButton = GameObject.Find("GotoMainButton");
        GotoTechTreeButton = GameObject.Find("GotoTechTreeButton");
        GotoUnitsButton = GameObject.Find("GotoUnitsButton");
        GotoStoreButton = GameObject.Find("GotoStoreButton");
        GotoRichHouseButton = GameObject.Find("GotoRichHouseButton");

        richMoney = richInitialMoney;

        richSalary = -100;
    }

    public void EndTurn()
    {
        turn++;
        int lastRichMoney = richMoney;
        //richSalary = 
        richMoney += richSalary;


        richDesperate += (double)(lastRichMoney - richMoney) / lastRichMoney;

        richMoneyBar.GetComponent<RichMoneyBar>().ChangeBar(richMoney, richInitialMoney);

        if (richMoney >= richMoneyBound) Ending(Endings.tooBig);
        if (richDesperate >= richDesperateBound) Ending(Endings.dropped);
    }

    public void ChangeScreen(Screen screen)
    {
        switch(currentScreen)
        {
            case Screen.main:
                GotoMainButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case Screen.techtree:
                GotoTechTreeButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case Screen.units:
                GotoUnitsButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case Screen.store:
                GotoStoreButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case Screen.richHouse:
                GotoRichHouseButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
        }
        currentScreen = screen;
        switch (screen)
        {
            case Screen.main:
                mainCamera.transform.position = cameraPositionMain;
                GotoMainButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
            case Screen.techtree:
                mainCamera.transform.position = cameraPositionTechTree;
                GotoTechTreeButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
            case Screen.units:
                mainCamera.transform.position = cameraPositionUnits;
                GotoUnitsButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
            case Screen.store:
                mainCamera.transform.position = cameraPositionStore;
                GotoStoreButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
            case Screen.richHouse:
                mainCamera.transform.position = cameraPositionRichHouse;
                GotoRichHouseButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
        }
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

