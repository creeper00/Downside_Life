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
        main, techtree, units, store, richHouse
    }

    public Screen currentScreen;


    //UI
    [SerializeField]
    private GameObject CommonCanvas, mainCanvas, techTreeCanvas, unitsCanvas, storeCanvas, richHouseCanvas;
    [SerializeField]
    public GameObject richMoneyBar;
    private GameObject GotoMainButton, GotoTechTreeButton, GotoUnitsButton, GotoStoreButton, GotoRichHouseButton;

    private void Awake()
    {
        instance = this;

        currentScreen = Screen.main;
        ChangeCanvas();
        
        GotoMainButton = GameObject.Find("GotoMainButton");
        GotoTechTreeButton = GameObject.Find("GotoTechTreeButton");
        GotoUnitsButton = GameObject.Find("GotoUnitsButton");
        GotoStoreButton = GameObject.Find("GotoStoreButton");
        GotoRichHouseButton = GameObject.Find("GotoRichHouseButton");

        richMoney = richInitialMoney;

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
        unitsCanvas.SetActive(currentScreen == Screen.units);
        storeCanvas.SetActive(currentScreen == Screen.store);
        richHouseCanvas.SetActive(currentScreen == Screen.richHouse);
    }
}

