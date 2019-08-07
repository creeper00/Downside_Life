using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technology : MonoBehaviour
{
    [SerializeField]
    public List<Technology> previousTechnologies;
    [HideInInspector]
    public bool isResearched;
    [SerializeField]
    public int neededMoney;
    [SerializeField]
    GameObject Information;
    [SerializeField]
    public GameManager.Job job;

    [SerializeField]
    TechInfoButtons techUpButton;

    int maxLevelUp;
    int averageLevelUp;
    string attributeUnlock;

    private void Start()
    {
        isResearched = false;
    }

    public void TechInfoOpen()
    {
        if (isResearched)
        {
            Debug.Log("이미 연구됨");
            return;
        }
        GameManager.instance.techInfoCanvas.SetActive(true);
        techUpButton.tech = this;
    }
    public void Upgrade()
    {
        GameManager.instance.playerMoney -= neededMoney;
    }
}
