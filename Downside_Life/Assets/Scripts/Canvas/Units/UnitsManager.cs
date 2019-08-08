using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager instance;

    [SerializeField]
    private GameObject crookTab, snakeTab, gangTab;
    [SerializeField]
    private Text RichMoneyChange;
    [SerializeField]
    Transform prefab;
    [SerializeField]
    GameObject contents;

    public void resetScrollView()
    {
        foreach (Transform child in contents.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
    }
    public void showCrooks()
    {
        contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        Debug.Log("Capacity : " + GameManager.instance.crooks.Count);
        for (int i = 0; i < GameManager.instance.crooks.Count; i++)
        {
            Instantiate(prefab, contents.transform);
            contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, contents.GetComponent<RectTransform>().sizeDelta.y + 120, 0);
        }
    }

    public void showSnakes()
    {
        contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        Debug.Log("Capacity : " + GameManager.instance.snakes.Count);
        for (int i = 0; i < GameManager.instance.snakes.Count; i++)
        {
            Instantiate(prefab, contents.transform);
            contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, contents.GetComponent<RectTransform>().sizeDelta.y + 140, 0);
        }
    }

    public void showGangs()
    {
        contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        Debug.Log("Capacity : " + GameManager.instance.gangs.Count);
        for (int i = 0; i < GameManager.instance.gangs.Count; i++)
        {
            Instantiate(prefab, contents.transform);
            contents.GetComponent<RectTransform>().sizeDelta = new Vector3(0, contents.GetComponent<RectTransform>().sizeDelta.y + 140, 0);
        }
    }

    public enum Tabs
    {
        crook, snake, gang
    }

    private Tabs currentTab;

    void Awake()
    {
        instance = this;
        currentTab = Tabs.crook;
        ShowTab();
    }

    public void ChangeTab(Tabs tab)
    {
        currentTab = tab;
        ShowTab();
    }

    private void ShowTab()
    {
        crookTab.SetActive(currentTab == Tabs.crook);
        snakeTab.SetActive(currentTab == Tabs.snake);
        gangTab.SetActive(currentTab == Tabs.gang);
    }
}
