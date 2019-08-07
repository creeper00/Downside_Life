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

    public enum Screen
    {
        main, techtree, units, store, richHouse, fifthFloor, fourthFloor, thirdFloor, secondFloor, firstFloor
    }

    public enum Job
    {
        crook,
        thief,
        gang,
        snake
    }
    public Screen currentScreen;


    //UI
    [SerializeField]
    public GameObject CommonCanvas, mainCanvas, techTreeCanvas, unitsCanvas, storeCanvas, richHouseCanvas,richHouse, firstFloorCanvas, secondFloorCanvas, thirdFloorCanvas, fourthFloorCanvas, fifthFloorCanvas, techInfoCanvas;
    [SerializeField]
    public GameObject richMoneyBar;
    private GameObject GotoMainButton, GotoTechTreeButton, GotoUnitsButton, GotoStoreButton, GotoRichHouseButton,FirstFloorButton, SecondFloorButton, ThirdFloorButton, FourthFloorButton, FifthFloorButton;

    private void Awake()
    {
        instance = this;
        crookAverageLevel = 1;
        crookMaxLevel = 1;
        snakeAverageLevel = 1;
        snakeMaxLevel = 1;
        gangAverageLevel = 1;
        gangMaxLevel = 1;
        currentScreen = Screen.main;
        ChangeCanvas();
        StaminaManage(10);
        GotoMainButton = GameObject.Find("GotoMainButton");
        GotoTechTreeButton = GameObject.Find("GotoTechTreeButton");
        GotoUnitsButton = GameObject.Find("GotoUnitsButton");
        GotoStoreButton = GameObject.Find("GotoStoreButton");
        GotoRichHouseButton = GameObject.Find("GotoRichHouseButton");
        FirstFloorButton = GameObject.Find("FirstFloorButton");
        SecondFloorButton = GameObject.Find("SecondFloorButton");
        ThirdFloorButton = GameObject.Find("ThirdFloorButton");
        FourthFloorButton = GameObject.Find("FourthFloorButton");
        FifthFloorButton = GameObject.Find("FifthFloorButton");

        richMoney = richInitialMoney;
        playerMoney = 0;
        ResourceManage();
        playerSalary = 100;
        richSalary = -100;
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
            case Screen.fifthFloor:
            case Screen.fourthFloor:
            case Screen.thirdFloor:
            case Screen.secondFloor:
            case Screen.firstFloor:
                GotoRichHouseButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
        }
        currentScreen = screen;
        switch (screen)
        {
            case Screen.main:
                GotoMainButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
            case Screen.techtree:
                GotoTechTreeButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
            case Screen.units:
                GotoUnitsButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
            case Screen.store:
                GotoStoreButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
            case Screen.richHouse:
                GotoRichHouseButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
                break;
        }
        ChangeCanvas();
    }

    private void ChangeCanvas()
    {
        mainCanvas.SetActive(currentScreen == Screen.main);
        techTreeCanvas.SetActive(currentScreen == Screen.techtree);
        techInfoCanvas.SetActive(currentScreen != Screen.techtree);
        unitsCanvas.SetActive(currentScreen == Screen.units);
        storeCanvas.SetActive(currentScreen == Screen.store);
        richHouse.SetActive(currentScreen == Screen.richHouse);
        richHouseCanvas.SetActive(currentScreen >= Screen.richHouse);
        firstFloorCanvas.SetActive(currentScreen == Screen.firstFloor);
        secondFloorCanvas.SetActive(currentScreen == Screen.secondFloor);
        thirdFloorCanvas.SetActive(currentScreen == Screen.thirdFloor);
        fourthFloorCanvas.SetActive(currentScreen == Screen.fourthFloor);
        fifthFloorCanvas.SetActive(currentScreen == Screen.fifthFloor);
        UnitsManager.instance.resetScrollView();
    }

    public void ChangeCanvasInHouse(Screen newScreen)
    {
        currentScreen = newScreen;
        ChangeCanvas();
    }
}

