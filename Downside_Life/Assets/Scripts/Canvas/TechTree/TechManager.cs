using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechManager : MonoBehaviour
{
    public static TechManager instance;
    [HideInInspector]
    public int[] temporaryJobSkillPoint, neededMaxSkillPoint, tier;
    [HideInInspector]
    public int temporarySkillPoint;
    [SerializeField]
    public List<Technology> crookTechnologies, snakeTechnologies, gangTechnologies, thiefTechnologies, passiveTechnologies;
    // Start is called before the first frame update
    [SerializeField]
    GameObject confirmInfo;

    public bool changeCanvas = false;
    public GameManager.Screen screen;

    GameObject techPoint;
    GameObject techPointBuy;
    void Awake()
    {
        instance = this;
        tier = new int[4];
        temporaryJobSkillPoint = new int[4];
        neededMaxSkillPoint = new int[4];
        //튜토리얼
        GameManager.instance.firstTechTree.SetActive(true);
    }

    private void Start()
    {

        ShowSkillPoint();
    }
    [SerializeField]
    Text skillPointText;

    public int CheckMaxNeededSkillPoint(GameManager.Job job) 
    {
        int temp = -1;
        switch(job)
        {
            case GameManager.Job.crook:
                tier[(int)job] = -1;
                for (int i=0; i<crookTechnologies.Count; i++)
                {
                    if (tier[(int)job] < crookTechnologies[i].tier && crookTechnologies[i].temporaryLevel > 0)
                    {
                        tier[(int)job] = crookTechnologies[i].tier;
                        temp = crookTechnologies[i].skillPointNeededNum;
                    }
                }
                for (int i=0; i<crookTechnologies.Count; i++)
                {
                    if (crookTechnologies[i].tier == tier[(int)job])
                    {
                        temp += crookTechnologies[i].temporaryLevel;
                    }
                }
                break;
            case GameManager.Job.gang:
                tier[(int)job] = -1;
                for (int i = 0; i < gangTechnologies.Count; i++)
                {
                    if (tier[(int)job] < gangTechnologies[i].tier && gangTechnologies[i].temporaryLevel > 0)
                    {
                        tier[(int)job] = gangTechnologies[i].tier;
                        temp = gangTechnologies[i].skillPointNeededNum;
                    }
                }
                for (int i = 0; i < gangTechnologies.Count; i++)
                {
                    if (gangTechnologies[i].tier == tier[(int)job])
                    {
                        temp += gangTechnologies[i].temporaryLevel;
                    }
                }
                break;
            case GameManager.Job.robber:
                tier[(int)job] = -1;
                for (int i = 0; i < thiefTechnologies.Count; i++)
                {
                    if (tier[(int)job] < thiefTechnologies[i].tier && thiefTechnologies[i].temporaryLevel > 0)
                    {
                        tier[(int)job] = thiefTechnologies[i].tier;
                        temp = thiefTechnologies[i].skillPointNeededNum;
                    }
                }
                for (int i = 0; i < thiefTechnologies.Count; i++)
                {
                    if (thiefTechnologies[i].tier == tier[(int)job])
                    {
                        temp += thiefTechnologies[i].temporaryLevel;
                    }
                }
                break;
            case GameManager.Job.snake:
                tier[(int)job] = -1;
                for (int i = 0; i < snakeTechnologies.Count; i++)
                {
                    if (tier[(int)job] < snakeTechnologies[i].tier && snakeTechnologies[i].temporaryLevel > 0)
                    {
                        tier[(int)job] = snakeTechnologies[i].tier;
                        temp = snakeTechnologies[i].skillPointNeededNum;
                    }
                }
                for (int i = 0; i < snakeTechnologies.Count; i++)
                {
                    if (snakeTechnologies[i].tier == tier[(int)job])
                    {
                        temp += snakeTechnologies[i].temporaryLevel;
                    }
                }
                break;
        }
        return temp;
    }
    public void ShowSkillPoint()
    {
        int temp = 350 + GameManager.instance.skillPointPrice * (GameManager.instance.totalSkillPoint + 1);
        skillPointText.text = "skillpoint : " + GameManager.instance.skillPoint+"\n가격 : "+temp;
    }
    public void ShowTemporarySkillPoint()
    {
        int temp = 350 + GameManager.instance.skillPointPrice * (GameManager.instance.totalSkillPoint + 1);
        skillPointText.color = new Color32(255, 0, 0, 255);
        skillPointText.text = "skillpoint : " + temporarySkillPoint+ "\n가격 : " + temp;
    }
    public bool CanMinusSkillLevel(Technology technology)
    {
        int temp = CheckMaxNeededSkillPoint(technology.whatJob);
        if (technology.tier == tier[(int)technology.whatJob])
        {
            return true;
        }
        if (temp == temporaryJobSkillPoint[(int)technology.whatJob])
        {
            return false;
        }
        else return true;
    }
    public void ConfirmSkillPoint()
    {
        //crookTechnology랑 다른거의 카운트가 같기때문에 for문 하나로 통일
        for (int i=0; i<crookTechnologies.Count; i++)
        {
            crookTechnologies[i].confirmSkillLevel();
            gangTechnologies[i].confirmSkillLevel();
            snakeTechnologies[i].confirmSkillLevel();
            thiefTechnologies[i].confirmSkillLevel();
            crookTechnologies[i].showTechnology();
            gangTechnologies[i].showTechnology();
            snakeTechnologies[i].showTechnology();
            thiefTechnologies[i].showTechnology();
        }
        for (int i=0; i<4; i++)
        {
            passiveTechnologies[i].confirmSkillLevel();
            GameManager.instance.jobSkillPoint[i] = temporaryJobSkillPoint[i];
        }
        GameManager.instance.skillPoint = temporarySkillPoint;
        skillPointText.color = new Color32(0, 0, 0, 255);
        confirmInfo.SetActive(false);
        if (changeCanvas)
        {
            GameManager.instance.ChangeScreen(screen);
            changeCanvas = false;
        }
    }
    public void ResetSkillPoint()
    {
        for (int i = 0; i < crookTechnologies.Count; i++)
        {
            crookTechnologies[i].ResetSkillLevel();
            gangTechnologies[i].ResetSkillLevel();
            snakeTechnologies[i].ResetSkillLevel();
            thiefTechnologies[i].ResetSkillLevel();
        }
        CheckMaxNeededSkillPoint(GameManager.Job.crook);
        CheckMaxNeededSkillPoint(GameManager.Job.gang);
        CheckMaxNeededSkillPoint(GameManager.Job.snake);
        CheckMaxNeededSkillPoint(GameManager.Job.robber);
        for (int i=0; i<4; i++)
        {
            temporaryJobSkillPoint[i] = GameManager.instance.jobSkillPoint[i];
        }
        temporarySkillPoint = GameManager.instance.skillPoint;
        skillPointText.color = new Color32(0, 0, 0, 255);
        
        ShowSkillPoint();
        ShowCanSkillPoint();
    }
    public void BuySkillPoint()
    {
        int temp = 350 + GameManager.instance.skillPointPrice * (GameManager.instance.totalSkillPoint+1);
        if (GameManager.instance.playerMoney > temp)
        {
            GameManager.instance.totalSkillPoint++;
            GameManager.instance.skillPoint++;
            temporarySkillPoint++;
            ShowTemporarySkillPoint();
            TechManager.instance.ShowCanSkillPoint();
            GameManager.instance.playerMoney -= temp;
            GameManager.instance.UpdateResourcesUI();
        }
        else
        {
            techPoint = GameObject.Find("TechPoint");
            techPointBuy = techPoint.transform.Find("TechCheck").gameObject;
            StartCoroutine("showBuyPointPopup");
        }
    }

    IEnumerator showBuyPointPopup()
    {
        techPointBuy.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        techPointBuy.SetActive(false);
    }
    public void ShowCanSkillPoint()
    {
        for (int i=0; i<crookTechnologies.Count; i++)
        {
            crookTechnologies[i].ShowTempoaryTechnology();
            snakeTechnologies[i].ShowTempoaryTechnology();
            gangTechnologies[i].ShowTempoaryTechnology();
            thiefTechnologies[i].ShowTempoaryTechnology();
        }
        for (int i=0; i<4; i++)
        {
            passiveTechnologies[i].SetTemporaryPassive();
        }
    }

    public bool isDifferent()
    {
        for (int i=0; i<4; i++)
        {
            if (temporaryJobSkillPoint[i] != GameManager.instance.jobSkillPoint[i])
            {
                return true;
            }
        }
        return false;
    }
    public void OpenConfirmInfo()
    {
        if (isDifferent())
        {
            confirmInfo.SetActive(true);
        }
    }
    public void OpenConfirmInfo(bool changeCanvas, GameManager.Screen screen)
    {
        if (isDifferent())
        {
            confirmInfo.SetActive(true);
            this.changeCanvas = changeCanvas;
            this.screen = screen;
        }
    }
}
