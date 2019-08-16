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
        robber,
        gang,
        snake
    }
    public Screen currentScreen;

    [SerializeField]
    public GameObject notEnoughStaminaCanvas;
    [Header("Canvas")]
    public GameObject CommonCanvas;
    public GameObject mainCanvas, techTreeCanvas, unitsCanvas, storeCanvas, richHouseCanvas,richHouse, firstFloorCanvas, secondFloorCanvas, thirdFloorCanvas, fourthFloorCanvas, fifthFloorCanvas, techInfoCanvas;
    [Header("기타 UI")]
    public GameObject richMoneyBar;
    private GameObject GotoMainButton, GotoTechTreeButton, GotoUnitsButton, GotoStoreButton, GotoRichHouseButton,FirstFloorButton, SecondFloorButton, ThirdFloorButton, FourthFloorButton, FifthFloorButton;

    private void Awake()
    {
        instance = this;
        UnitsManager.instance = unitsCanvas.GetComponent<UnitsManager>();
        StoreManager.instance = storeCanvas.GetComponent<StoreManager>();
        factories = new List<Factory>();
        

        crookAverageLevel = 50;
        crookMaxLevel = 100;
        snakeAverageLevel = 50;
        snakeMaxLevel = 100;
        gangAverageLevel = 50;
        gangMaxLevel = 100;
        currentScreen = Screen.main;
        
        SetStamina(10);
        
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

        GotoMainButton.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
        richMoney = richInitialMoney;
        playerMoney = 0;
        ResourceManage();
        playerSalary = 100;
        richSalary = -100;
    }

    private void Start()
    {
        ChangeCanvas();

        //GameManager.Units
        crooks = new List<Crook>();
        crookAttributes = new List<string>();
        snakes = new List<Snake>();
        gangs = new List<Gang>();
    }


    public void ChangeScreen(Screen screen)
    {
        Color32 noColor = new Color32(255, 255, 255, 255);
        switch (currentScreen)
        {
            case Screen.main:
                GotoMainButton.GetComponent<Image>().color = noColor;
                break;
            case Screen.techtree:
                GotoTechTreeButton.GetComponent<Image>().color = noColor;
                break;
            case Screen.units:
                GotoUnitsButton.GetComponent<Image>().color = noColor;
                break;
            case Screen.store:
                GotoStoreButton.GetComponent<Image>().color = noColor;
                break;
            case Screen.richHouse:
            case Screen.fifthFloor:
            case Screen.fourthFloor:
            case Screen.thirdFloor:
            case Screen.secondFloor:
            case Screen.firstFloor:
                GotoRichHouseButton.GetComponent<Image>().color = noColor;
                break;
        }

        currentScreen = screen;

        Color32 darkColor = new Color32(162, 162, 162, 255);
        switch (screen)
        {
            case Screen.main:
                GotoMainButton.GetComponent<Image>().color = darkColor;
                break;
            case Screen.techtree:
                GotoTechTreeButton.GetComponent<Image>().color = darkColor;
                break;
            case Screen.units:
                GotoUnitsButton.GetComponent<Image>().color = darkColor;
                break;
            case Screen.store:
                GotoStoreButton.GetComponent<Image>().color = darkColor;
                break;
            case Screen.richHouse:
                GotoRichHouseButton.GetComponent<Image>().color = darkColor;
                break;
        }
        ChangeCanvas();
    }

    private void ChangeCanvas()
    {
        mainCanvas.SetActive(currentScreen == Screen.main);
        techTreeCanvas.SetActive(currentScreen == Screen.techtree);
        techInfoCanvas.SetActive(currentScreen != Screen.techtree);

        if (currentScreen == Screen.units )
        {

            unitsCanvas.SetActive(true);
            UnitsManager.instance.ChangeTab(UnitsManager.Tabs.crook);
        }
        else
        {
            unitsCanvas.SetActive(false);
        }

        storeCanvas.SetActive(currentScreen == Screen.store);
        richHouse.SetActive(currentScreen == Screen.richHouse);
        richHouseCanvas.SetActive(currentScreen >= Screen.richHouse);
        firstFloorCanvas.SetActive(currentScreen == Screen.firstFloor);
        secondFloorCanvas.SetActive(currentScreen == Screen.secondFloor);
        thirdFloorCanvas.SetActive(currentScreen == Screen.thirdFloor);
        fourthFloorCanvas.SetActive(currentScreen == Screen.fourthFloor);
        fifthFloorCanvas.SetActive(currentScreen == Screen.fifthFloor);
    }

    public void ChangeCanvasInHouse(Screen newScreen)
    {
        currentScreen = newScreen;
        ChangeCanvas();
    }

    //확률을 넣으면 앞에서부터 차례대로 0, 1, 2, 3 ... 을 할당하여 확률에 맞춰서 숫자를 반환한다.
    public int SelectRandom(int[] values)
    {
        int k = 0;
        for (int i=0; i<values.Length; i++)
        {
            k += values[i];
        }
        int temp = Random.Range(0, k);
        int result = 0;
        int l = 0;
        int r = values[0];
        k = 0;
        while(k < values.Length-1)
        {
            if (l < temp && temp < r)
            {
                break;
            }
            l = values[k];
            r = values[k + 1];
            k++;
        }
        
        return result;
    }
}

