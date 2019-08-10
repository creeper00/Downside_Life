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

    public bool[] isBuyed;

    public void Start()
    {
        instance = this;
        isBuyed = new bool[GameManager.instance.crookStoreSellingNumber];
    }
    public void resetScrollView()
    {
        foreach (Transform child in crookStoreViewPort.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in snakeStoreViewPort.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in gangStoreViewPort.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
    }
    public void showStoreCrooks()
    {
        resetScrollView();
        crookStoreViewPort.GetComponent<RectTransform>().sizeDelta = new Vector3(GameManager.instance.crookStoreSellingNumber * 270, 0, 0);
        int index = 0;
        foreach (var unit in GameManager.instance.sellingCrooks)
        {
            var listStoreItemObject = Instantiate(prefab, crookStoreViewPort.transform);
            var storeItemList = listStoreItemObject.GetComponent<TemporaryButton>();
            storeItemList.setUnitInformation(index, unit);
            index++;
        }
    }
}
