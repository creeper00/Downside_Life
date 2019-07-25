using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainGameButtonScript : MonoBehaviour
{

    public void StartMainGame()
    {
        SceneManager.LoadScene("Main");
    }
}