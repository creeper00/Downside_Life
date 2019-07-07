using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyHouseButtonScript : MonoBehaviour {

    void OnMouseDown()
    {
        GameManager.instance.GotoScene(GameManager.Scenes.playerHouse);
    }
}