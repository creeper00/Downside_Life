using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            MainManager.instance.showInfo(true);

        }
        else
        {
            MainManager.instance.showInfo(false);
        }
    }
}
