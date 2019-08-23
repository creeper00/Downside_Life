using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{

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
    private GameObject FolderUI;
    private float folderUpYAxis = 160, folderDownYAxis = -370, folderMoveYDistance;
    [Header("튜토리얼")]
    [SerializeField]
    private GameObject mainScreenExplain;
    public GameObject firstTechTree, firstUnit, firstStore, firstRichHouse;
    private bool mainScreenExplainDone = false, firstTechTreeDone = false, firstUnitDone = false, firstStoreDone = false, firstRichHouseDone = false;
    [Header("팝업창")]
    public GameObject notEnoughStaminaCanvas;
    public GameObject itemTypeNotMatchCanvas, alreadyHasItemCanvas, snakeSteallSuccessCanvas;
    [Header("메인 화면 유닛들")]
    [SerializeField]
    private GameObject showAttachedCrooks;
    [SerializeField]
    private GameObject showAttachedSnakes;
    [Header("Canvas")]
    public GameObject CommonCanvas;
    public GameObject techTreeCanvas, unitsCanvas, storeCanvas, richHouseCanvas,richHouse, firstFloorCanvas, secondFloorCanvas, thirdFloorCanvas, fourthFloorCanvas, fifthFloorCanvas, techInfoCanvas;
    [Header("기타 UI")]
    public GameObject richMoneyBar;
    private GameObject GotoMainButton, GotoTechTreeButton, GotoUnitsButton, GotoStoreButton, GotoRichHouseButton,FirstFloorButton, SecondFloorButton, ThirdFloorButton, FourthFloorButton, FifthFloorButton;

    private void Awake()
    {
        //각 스크립트의 static instance 넣어 주기
        instance = this;
        UnitsManager.instance = unitsCanvas.GetComponent<UnitsManager>();
        StoreManager.instance = storeCanvas.GetComponent<StoreManager>();

        //각 캔버스 시작 위치 조정
        folderMoveYDistance = folderUpYAxis - folderDownYAxis;
        FolderUI.transform.position = new Vector3(640, -10, 0);
        techTreeCanvas.transform.position = new Vector3(640, 360-folderMoveYDistance, 0);
        unitsCanvas.transform.position = new Vector3(640, 360-folderMoveYDistance, 0);
        storeCanvas.transform.position = new Vector3(640, 360-folderMoveYDistance, 0);
        richHouseCanvas.transform.position = new Vector3(640, 360-folderMoveYDistance, 0);
        
        currentScreen = Screen.main;

        //이벤트 설정
        InitializeEventSettings();

        //시작 화면 유닛 표시
        ShowMainScreenUnits();

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
        //ResourceManage();
        playerSalary = 100;
        richSalary = -100;

        for ( int i = 0; i < 3; ++i )
        {
            attachedGangs[i] = new List<Gang>();
        }
        storeCanvas.SetActive(true);

        //튜토리얼 표시
        mainScreenExplain.SetActive(true);
    }

    private void Start()
    {
        storeCanvas.SetActive(false);
        ChangeCanvas();

        //GameManager.Units
        crooks = new List<Crook>();
        snakes = new List<Snake>();
        gangs = new List<Gang>();
        crookType.Add(1);
        crookType.Add(2);
        snakeType.Add(0);
        snakeType.Add(2);
        gangType.Add(0);
        gangType.Add(2);
        crookListItems = new List<List<int>>();
        snakeListItems = new List<List<int>>();
        gangListItems = new List<List<int>>();
        List<int> crookNormal = new List<int>();
        crookNormal.Add(0); crookNormal.Add(1); crookNormal.Add(2);
        crookListItems.Add(crookNormal);

        List<int> crookRare = new List<int>();
        crookRare.Add(0); crookRare.Add(1);
        crookListItems.Add(crookRare);

        List<int> crookLegend = new List<int>();
        crookLegend.Add(0);
        crookListItems.Add(crookLegend);

        List<int> snakeNormal = new List<int>();
        snakeNormal.Add(0); snakeNormal.Add(1);
        snakeListItems.Add(snakeNormal);

        List<int> snakeRare = new List<int>();
        snakeRare.Add(0); snakeRare.Add(1);
        snakeListItems.Add(snakeRare);

        List<int> snakeLegend = new List<int>();
        snakeLegend.Add(0);
        snakeListItems.Add(snakeLegend);

        List<int> gangNormal = new List<int>();
        gangNormal.Add(0); gangNormal.Add(1); gangNormal.Add(2);
        gangListItems.Add(gangNormal);

        List<int> gangRare = new List<int>();
        gangRare.Add(0); gangRare.Add(1); gangRare.Add(2);
        gangListItems.Add(gangRare);

        List<int> gangLegend = new List<int>();
        gangLegend.Add(0);
        gangListItems.Add(gangLegend);
        ResourceManage();
    }


    public void ChangeScreen(Screen screen)
    {
        if (currentScreen == Screen.techtree && TechManager.instance.isDifferent())
        {
            TechManager.instance.OpenConfirmInfo(true, screen);
            return;
        }
        //폴더 창 위아래로 옮이기
        if ( currentScreen == Screen.main && screen != Screen.main )            //위로 들기
        {
            StartCoroutine(showFolderSlowly());
        }
        else if (currentScreen != Screen.main && screen == Screen.main)         //아래로 내리기
        {
            StartCoroutine(endFolderSlowly());
        }

        //폴더 북마크 조정하기
        GotoTechTreeButton.transform.position += new Vector3(0, (currentScreen == Screen.techtree) ? 4 : 0, 0);
        GotoUnitsButton.transform.position += new Vector3(0, (currentScreen == Screen.units) ? 4 : 0, 0);
        GotoStoreButton.transform.position += new Vector3(0, (currentScreen == Screen.store) ? 4 : 0, 0);
        GotoRichHouseButton.transform.position += new Vector3(0, (currentScreen == Screen.richHouse || currentScreen == Screen.firstFloor || currentScreen == Screen.secondFloor || currentScreen == Screen.thirdFloor || currentScreen == Screen.fourthFloor || currentScreen == Screen.fifthFloor) ? 4 : 0, 0);

        GotoTechTreeButton.transform.position += new Vector3(0, (screen == Screen.techtree) ? -4 : 0, 0);
        GotoUnitsButton.transform.position += new Vector3(0, (screen == Screen.units) ? -4 : 0, 0);
        GotoStoreButton.transform.position += new Vector3(0, (screen == Screen.store) ? -4 : 0, 0);
        GotoRichHouseButton.transform.position += new Vector3(0, (screen == Screen.richHouse) ? -4 : 0, 0);

        currentScreen = screen;

        ChangeCanvas();
    }
    private IEnumerator showFolderSlowly()
    {
        for(int i = 0; i <= 10; i++)
        {
            FolderUI.transform.position = new Vector3(640, -170 + folderUpYAxis + 53 *i, 0);
            techTreeCanvas.transform.position = new Vector3(640, -170 + 53*i, 0);
            unitsCanvas.transform.position = new Vector3(640, -170 + 53 * i, 0);
            storeCanvas.transform.position = new Vector3(640, -170 + 53 * i, 0);
            richHouseCanvas.transform.position = new Vector3(640, -170 + 53 * i, 0);
            yield return new WaitForSeconds(0.02f);
        }
    }
    
    private IEnumerator endFolderSlowly()
    {
        for(int i = 0; i <= 10; i++)
        {
            FolderUI.transform.position = new Vector3(640, 520 - folderMoveYDistance * i / 10, 0);
            techTreeCanvas.transform.position = new Vector3(640, 360 - folderMoveYDistance * i / 10, 0);
            unitsCanvas.transform.position = new Vector3(640, 360 - folderMoveYDistance * i / 10, 0);
            storeCanvas.transform.position = new Vector3(640, 360 - folderMoveYDistance * i / 10, 0);
            richHouseCanvas.transform.position = new Vector3(640, 360 - folderMoveYDistance * i / 10, 0);
            yield return new WaitForSeconds(0.02f);
        }
    }
    private void ChangeCanvas()
    {

        if (currentScreen == Screen.units)
        {
            unitsCanvas.SetActive(true);
            UnitsManager.instance.ChangeTab(UnitsManager.Tabs.crook);
        }
        else
        {
            unitsCanvas.SetActive(false);
        }

        techTreeCanvas.SetActive(currentScreen == Screen.techtree);
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

}

