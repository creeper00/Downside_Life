using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;


    [SerializeField]
    GameObject crookStoreViewPort, snakeStoreViewPort, gangStoreViewPort;
    [SerializeField]
    public GameObject buyInfo;
    [SerializeField]
    public BuyButton buyYes;
    [SerializeField]
    Transform prefab;

    [HideInInspector]
    public bool[] isCrookBuyed, isSnakeBuyed, isGangBuyed;
    

    public void Awake()
    {
        instance = this;
        isCrookBuyed = new bool[GameManager.instance.crookStoreSellingNumber];
        isSnakeBuyed = new bool[GameManager.instance.snakeStoreSellingNumber];
        isGangBuyed = new bool[GameManager.instance.gangStoreSellingNumber];
    }
    public void resetScrollView(GameManager.Job job)
    {
        switch (job)
        {
            case GameManager.Job.crook:
                foreach (Transform child in crookStoreViewPort.GetComponent<Transform>())
                {
                    Destroy(child.gameObject);
                }
                break;
            case GameManager.Job.gang:
                foreach (Transform child in gangStoreViewPort.GetComponent<Transform>())
                {
                    Destroy(child.gameObject);
                }
                break;
            case GameManager.Job.snake:
                foreach (Transform child in snakeStoreViewPort.GetComponent<Transform>())
                {
                    Destroy(child.gameObject);
                }
                break;
        }
    }
    public void showStoreCrooks()
    {
        float width = crookStoreViewPort.GetComponent<RectTransform>().rect.width;
        float spacing = crookStoreViewPort.GetComponent<GridLayoutGroup>().spacing.x;
        resetScrollView(GameManager.Job.crook);
        crookStoreViewPort.GetComponent<RectTransform>().sizeDelta = new Vector3(GameManager.instance.crookStoreSellingNumber * 120 - width - spacing, 0, 0);
        int index = 0;
        foreach (var unit in GameManager.instance.sellingCrooks)
        {
            var listStoreItemObject = Instantiate(prefab, crookStoreViewPort.transform);
            var storeItemList = listStoreItemObject.GetComponent<TemporaryButton>();
            storeItemList.setCrookUnitInformation(index, unit);
            index++;
        }
    }
    public void showStoreSnakes()
    {
        float width = snakeStoreViewPort.GetComponent<RectTransform>().rect.width;
        float spacing = snakeStoreViewPort.GetComponent<GridLayoutGroup>().spacing.x;
        resetScrollView(GameManager.Job.snake);
        snakeStoreViewPort.GetComponent<RectTransform>().sizeDelta = new Vector3(GameManager.instance.snakeStoreSellingNumber * 120 - width - spacing, 0, 0);
        int index = 0;
        foreach (var unit in GameManager.instance.sellingSnakes)
        {
            var listStoreItemObject = Instantiate(prefab, snakeStoreViewPort.transform);
            var storeItemList = listStoreItemObject.GetComponent<TemporaryButton>();
            storeItemList.setSnakeUnitInformation(index, unit);
            index++;
        }
    }
    public void showStoreGangs()
    {
        float width = gangStoreViewPort.GetComponent<RectTransform>().rect.width;
        float spacing = gangStoreViewPort.GetComponent<GridLayoutGroup>().spacing.x;
        resetScrollView(GameManager.Job.gang);
        gangStoreViewPort.GetComponent<RectTransform>().sizeDelta = new Vector3(GameManager.instance.gangStoreSellingNumber * 120 - width - spacing, 0, 0);
        int index = 0;
        foreach (var unit in GameManager.instance.sellingGangs)
        {
            var listStoreItemObject = Instantiate(prefab, gangStoreViewPort.transform);
            var storeItemList = listStoreItemObject.GetComponent<TemporaryButton>();
            storeItemList.setGangUnitInformation(index, unit);
            index++;
        }
    }
}
