using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RetireOn : MonoBehaviour
{
    // Start is called before the first frame update
    Button yes;
    public static RetireOn instance;
    public RetireChecker retireChecker;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        yes = this.transform.GetComponent<Button>();
        yes.onClick.AddListener(yesClick);
    }

    // Update is called once per frame
    public void yesClick()
    {
        retireChecker.doRetire();
    }
}
