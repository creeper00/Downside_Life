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
    [HideInInspector]
    public int playerMoney;
    [HideInInspector]
    public int richMoney;
    [HideInInspector]
    public int richSalary;

    [Header("게임 오브젝트")]
    [SerializeField]
    private Camera mainCamera;

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
        
        //richSalary = 
    }

    void Update()
    {

    }

    public enum Phase
    {
        phase1,
        phase2
    }

    public enum Endings
    {
        richTooBig,
        richResurrect,
        success,
        love
    }

    public enum Scenes
    {
        main,
        playerHouse,
        richHouse
    }

    public void GotoScene(Scenes scene)
    {
        switch (scene)
        {
            case Scenes.main:
                mainCamera.transform.position = new Vector3(0, 0, -10);
                break;
            case Scenes.playerHouse:
                mainCamera.transform.position = new Vector3(30, 0, -10);
                break;
            case Scenes.richHouse:
                mainCamera.transform.position = new Vector3(-30, 0, -10);
                break;
        }
    }

    public void TurnOver()
    {
        //부자 월급 변화? 이거는 공장 수입 합이니까 이때 변화하진 않을 것 같고

        //부자 월급만큼 부자 재산에 추가
        richMoney += richSalary;

        //플레이어 돈 변화

        //부자 돈이 일정 수준을 넘어가면 게임 종료
        if ( richMoney >= richMoneyUpperBound)
        {
            Ending(Endings.richTooBig);
        }

        //부자 월급이 0보다 작으면 페이즈 전환
        if ( phase == Phase.phase1 && richSalary < 0)
        {
            StartPhase2();
        }
    }

    private void StartPhase2()
    {
        phase = Phase.phase2;
    }

    public void Ending(Endings ending)
    {

    }
}
