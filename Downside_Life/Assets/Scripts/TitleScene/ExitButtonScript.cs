using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitButtonScript : MonoBehaviour
{
    public void EndGame()
    {
        Application.Quit();
        /*
        주석 부분은 unity editor에서도 종료를 실행하기 위한 코드임
        { 
            #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
            #else
                    Application.Quit();
            #endif
        }
        */
    }
}
