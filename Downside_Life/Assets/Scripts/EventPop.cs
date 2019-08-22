using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject eventPop;
    int set = 1;
    // Update is called once per frame
    public void showEventPop()
    {
        if(set==1)
        {
            StartCoroutine("showSlowly");
            set = 0;
        }
        else
        {
            StartCoroutine("hideSlowly");
            set = 1;
        }
    }

    private IEnumerator showSlowly()
    {
        for (int i = 0; i <= 10; i++)
        {
            eventPop.transform.position -= new Vector3(50f, 0f, 0f);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator hideSlowly()
    {
        for (int i = 0; i <= 10; i++)
        {
            eventPop.transform.position += new Vector3(50f, 0f, 0f);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
