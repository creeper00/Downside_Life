using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    //Scrollview.cs 라는 script가 있을 때
    //public Scrollview scrollview;

    //ArrayList는 할당을 해 줘야지 보임

    [Header("초기화 값")]
    [SerializeField]
    private int richMoneyInitialize;                    //부자의 초기 재산
    [SerializeField]
    private int richMoneyUpperBound;                    //부자의 재산이 이 값을 넘어가면 배드 엔딩

    [Header("게임 데이터")]
    [HideInInspector]
    public Phase phase;                                 //현재 페이즈
    [HideInInspector]
    public int playerMoney, playerSalary;               //현재 플레이어의 재산, 플레이어가 이번 턴이 끝날 때 얻을 재산
    [HideInInspector]
    public int richMoney, richSalary;                   //부자의 재산, 부자가 이번 턴이 끝날 때 얻을 재산
    [HideInInspector]
    public int friendy;                                 //친밀도
    

    [Header("게임 오브젝트")]
    [SerializeField]
    private Camera mainCamera;

    [Header("Screen")]
    [SerializeField]
    private GameObject mainScreen, gangScreen, robScreen, sagiScreen, snakeScreen;      //각 스크린들 지정

    [Header("status")]
    public PlayerStatusText playerStatusText;
    public GangsterStatusText gangsterStatusText;
    public RobStatusText robStatusText;

    public static GameManager instance;
    
    private void Awake()
    {
        instance = this;
        mainScreen.SetActive(true);
        gangScreen.SetActive(false);
        robScreen.SetActive(false);
        sagiScreen.SetActive(false);
        snakeScreen.SetActive(false);
    }

    void Start()
    {
        phase = Phase.phase1;
        richMoney = richMoneyInitialize;
        playerMoney = 0;
                                                playerSalary = 10000;
        playerRobMoneySkill = 1;
        playerRobSuccessSkill = 1;
        playerStatusText.showPlayerStatus(playerMoney);
        friendy = 100;

        gangsterNumber = new int[4] { 0, 0, 0, 0 };

        sagiHired = new bool[4] { false, false, false, false };
        sagiAttatched = new bool[4] { false, false, false, false };
    }

    public enum Phase
    {
        phase1, phase2
    }

    public enum RoundState
    {
        RichPhase, PlayerPhase
    }

    public enum Screens
    {
        main, sagi, gang, rob, snake
    }

    public RoundState CurrentState
    {
        get;
        private set;
    }

    public void ChangePhase()
    {
        switch(CurrentState)
        {
            case RoundState.RichPhase:
                PlayerPhase();
                break;
            case RoundState.PlayerPhase:
                RichPhase();
                break;
        }
    }

    private void RichPhase()
    {
        CurrentState = RoundState.RichPhase;
        int sagiMoney = 0;
        for (int i = 0; i < 4; ++i)
        {
            if (sagiAttatched[i]) sagiMoney += (int)((double)richMoney * sagiPercentage[i] * 0.01);
        }
        int snakeMoney = 0;
        for ( int i = 0; i < 4; ++i)
        {

        }
        richSalary -= (sagiMoney + snakeMoney);
        playerSalary += (sagiMoney + snakeMoney);
        richMoney += richSalary;
        ChangePhase();
    }

    private void PlayerPhase()
    {
        playerMoney += playerSalary;
        playerStatusText.showPlayerStatus(playerMoney);
        CurrentState = RoundState.PlayerPhase;
    }

    public void GotoScreen(Screens screen)
    {
        ChangeScreen(screen);
    }

    private void ChangeScreen(Screens screen)
    {
        mainScreen.SetActive(false);
        gangScreen.SetActive(false);
        sagiScreen.SetActive(false);
        robScreen.SetActive(false);
        snakeScreen.SetActive(false);
        switch (screen)
        {
            case Screens.main:
                mainScreen.SetActive(true);
                playerStatusText.showPlayerStatus(playerMoney);
                break;
            case Screens.gang:
                gangScreen.SetActive(true);
                gangsterStatusText.showGangsterNumber();
                break;
            case Screens.sagi:
                sagiScreen.SetActive(true);
                break;
            case Screens.rob:
                robScreen.SetActive(true);
                playerStatusText.showPlayerRobSkillStatus(playerRobMoneySkill, playerRobSuccessSkill);
                break;
            case Screens.snake:
                snakeScreen.SetActive(true);
                break;
        }
    }

    [Header("갱단 관리")]
    public int[] gangsterCost = new int[4];
    [HideInInspector]
    public int[] gangsterNumber;

    [System.Serializable]
    public class FloorSuccessRate { public int[] successRate = new int[4]; }

    [Header("도둑질 관련")]
    public FloorSuccessRate[] floorSuccessRate = new FloorSuccessRate[5];
    public int[] floorMoney = new int[5];           //각 층에서 얻을 수 있는 돈의 양
    public int moneyPercentage;                     //섬세한 손길 1레벨당 늘어나는 돈 회수 비율
    public int failFriendyDecrease;
    [HideInInspector]
    public int floorLevel;
    [HideInInspector]
    public int playerRobMoneySkill, playerRobSuccessSkill;
    public int playerRobMoneySkillUpperBound, playerRobSuccessSkillUpperBound;

    [Header("사기꾼 관리")]
    public int[] sagiHireCost = new int[4];
    public int[] sagiAttatchCost = new int[4];
    public int[] sagiPercentage = new int[4];
    [HideInInspector]
    public bool[] sagiHired = new bool[4], sagiAttatched = new bool[4];

    /*   기타 함수   */

    public void NotEnoughMoney()
    {
        
    }

    private void StartPhase2()
    {
        phase = Phase.phase2;
    }
}

