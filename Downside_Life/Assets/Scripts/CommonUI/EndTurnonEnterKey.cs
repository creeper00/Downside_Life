using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnonEnterKey : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject temporary;
    GameObject EndChecker;
    Button endButton;
    int temp = 0;
    /*void Start()
    {
        temporary = GameObject.Find("EndTurn");
        EndChecker = temporary.transform.Find("EndTurnCheck").gameObject;
        endButton = this.GetComponent<Button>();
        endButton.onClick.AddListener(endClick);
    }

    // Update is called once per frame
    void endClick()
    {
        if(temp==0)
        {
            EndChecker.SetActive(true);
            Debug.Log(EndChecker.activeSelf);
            temp = 1;
        }
        else
        {
            EndTurnButton.instance.EndTurn();
            temp = 0;
            EndChecker.SetActive(false);
        }
    }*/
}
