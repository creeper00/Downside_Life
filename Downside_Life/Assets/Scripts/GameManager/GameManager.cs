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
    private int richMoneyInitialize;        //부자의 초기 재산
    [SerializeField]
    private int richMoneyUpperBound;            //부자의 재산이 이 값을 넘어가면 배드 엔딩

    [Header("게임 데이터")]
    [HideInInspector]
    public Phase phase;
    private int playerMoney;
    private int playerSalary = 10;
    private int richMoney;
    private int richSalary;

    [Header("게임 오브젝트")]
    [SerializeField]
    private Camera mainCamera;
    [Header("Screen")]
    [SerializeField]
    private GameObject mainScreen;
    [SerializeField]
    private GameObject gangScreen;
    [SerializeField]
    private GameObject robScreen;
    [SerializeField]
    private GameObject sagiScreen;
    [SerializeField]
    private GameObject snakeScreen;
    [SerializeField]
    private PlayerStatus PlayerStatus;

    //이거 필요한가?
    public static GameManager instance;

    
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        phase = Phase.phase1;
        richMoney = richMoneyInitialize;
        playerMoney = 0;
        PlayerStatus.showPlayerStatus(playerMoney);
    }
    public enum Phase
    {
        phase1,
        phase2
    }

    public enum RoundState
    {
        RichPhase,
        PlayerPhase
    }
    public RoundState CurrentState
    {
        get;
        private set;
    }

    public enum Screens
    {
        main,
        sagi,
        gang,
        rob,
        snake
    }
    public void ChangePhase()
    {
        switch((int)CurrentState)
        {
            case 0:
                PlayerPhase();
                break;
            case 1:
                RichPhase();
                break;
        }
    }
    private void RichPhase()
    {
        CurrentState = RoundState.RichPhase;
        richMoney += richSalary;
        ChangePhase();
    }
    private void PlayerPhase()
    {
        playerMoney += playerSalary;
        PlayerStatus.showPlayerStatus(playerMoney);
        CurrentState = RoundState.PlayerPhase;
    }
    public void GotoScreen(Screens screen)
    {
        switch (screen)
        {
            case Screens.main:
                mainCamera.transform.position = mainScreen.transform.position;
                break;
            case Screens.gang:
                mainCamera.transform.position = gangScreen.transform.position;
                break;
            case Screens.sagi:
                mainCamera.transform.position = sagiScreen.transform.position;
                break;
            case Screens.rob:
                mainCamera.transform.position = robScreen.transform.position;
                break;
            case Screens.snake:
                mainCamera.transform.position = snakeScreen.transform.position;
                break;
        }
        mainCamera.transform.position += new Vector3(0, 0, -10);
    }

    private void StartPhase2()
    {
        phase = Phase.phase2;
    }
}
