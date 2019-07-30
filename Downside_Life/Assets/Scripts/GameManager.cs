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
        main, factories, richHouse
    }

    //카메라 관련
    public GameObject mainCamera;
    private Vector3 cameraPositionMain, cameraPositionFactories, cameraPositionRichHouse;
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
    private GameObject ManageButtons;
    public GameObject richMoneyBar;
    [SerializeField]
    private GameObject gotoFactoriesButton, gotoRichHouseButton, openGangManageButton, openRobberManageButton;

    

    private void Awake()
    {
        instance = this;

        currentScreen = Screen.main;

        cameraPositionMain = GameObject.Find("Main").GetComponent<Transform>().position;
        cameraPositionFactories = GameObject.Find("Factories").GetComponent<Transform>().position;
        cameraPositionRichHouse = GameObject.Find("RichHouse").GetComponent<Transform>().position;

        richMoney = richInitialMoney;

        richSalary = -100;
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

    public void ChangeScreen(Screen screen)
    {
        currentScreen = screen;
        switch (screen)
        {
            case Screen.main:
                mainCamera.transform.position = cameraPositionMain;
                SetUIButtons(true, true, false, false);
                ManageButtons.SetActive(true);
                break;
            case Screen.factories:
                mainCamera.transform.position = cameraPositionFactories;
                SetUIButtons(false, true, true, false);
                ManageButtons.SetActive(false);
                break;
            case Screen.richHouse:
                mainCamera.transform.position = cameraPositionRichHouse;
                SetUIButtons(true, false, false, true);
                ManageButtons.SetActive(false);
                break;
        }
    }

    private void SetUIButtons(bool gotoFactories, bool gotoRichHouse, bool openGangManage, bool openRobberManage)
    {
        gotoFactoriesButton.SetActive(gotoFactories);
        gotoRichHouseButton.SetActive(gotoRichHouse);
        openGangManageButton.SetActive(openGangManage);
        openRobberManageButton.SetActive(openRobberManage);
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

