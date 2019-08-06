using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainManager : MonoBehaviour
{

    public static MainManager instance;
    [SerializeField]
    GameObject information;
    [SerializeField]
    Text crookInfo;
    [SerializeField]
    Text snakeInfo;
    [SerializeField]
    Text thiefInfo;
    [SerializeField]
    Text gangInfo;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showInfo(bool activate)
    {
        information.SetActive(activate);

        crookInfo.text = "LV : \n" + "특성 : \n" + "공격 : \n";
        snakeInfo.text = "LV : \n" + "특성 : \n" + "공격 : \n";
        thiefInfo.text = "LV : \n" + "특성 : \n" + "공격 : \n";
        gangInfo.text = "LV : \n" + "특성 : \n" + "공격 : \n";

    }
}
