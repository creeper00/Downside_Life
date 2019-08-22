using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RetireChecker : MonoBehaviour
{
    [SerializeField]
    GameObject RetireObject;
    GameObject retireCheck;
    GameObject retireParent;
    GameObject retireYes;
    RetireOn retireOn;
    Text retireText;
    public int staminaUse_Retire = 1;
    // Start is called before the first frame update
    Button retire;
    private void Awake()
    {
        retireParent = GameObject.Find("Retire");
        retireCheck = retireParent.transform.Find("RetireCheck").gameObject;
        retireYes = retireCheck.transform.Find("RetireCheck_yes").gameObject;
        retireOn = retireYes.GetComponent<RetireOn>();
        retireText = retireCheck.GetComponentInChildren<Text>();
        retireText.text = "정말 소중한 동료를 다 쓰고 버리시겠습니까?\n" + "행동력 소모 : " + staminaUse_Retire;
    }
    void Start()
    {
        retire = this.transform.GetComponent<Button>();
        retire.onClick.AddListener(fClick);
    }
    void fClick()
    {
        retireCheck.SetActive(true);
        retireOn.retireChecker = this;
    }
    public void doRetire()
    {
        RetireObject.GetComponent<RetireButton>().Retire();
    }
}
