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
    [Header("팝업창")]
    public GameObject notEnoughStaminaCanvas;
    public GameObject itemTypeNotMatchCanvas, alreadyHasItemCanvas;
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
        factories = new Factory[3];

        //각 캔버스 시작 위치 조정
        folderMoveYDistance = folderUpYAxis - folderDownYAxis;
        FolderUI.transform.position = new Vector3(640, -10, 0);
        techTreeCanvas.transform.position = new Vector3(640, 360-folderMoveYDistance, 0);
        unitsCanvas.transform.position = new Vector3(640, 360-folderMoveYDistance, 0);
        storeCanvas.transform.position = new Vector3(640, 360-folderMoveYDistance, 0);
        richHouseCanvas.transform.position = new Vector3(640, 360-folderMoveYDistance, 0);

        crookMinLevel = 0;
        crookMaxLevel = 10;
        snakeMinLevel = 0;
        snakeMaxLevel = 10;
        gangMinLevel = 0;
        gangMaxLevel = 10;
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
        //ResourceManage();
        playerSalary = 100;
        richSalary = -100;
    }

    private void Start()
    {
        ChangeCanvas();

        //GameManager.Units
        crooks = new List<Crook>();
        snakes = new List<Snake>();
        gangs = new List<Gang>();
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

