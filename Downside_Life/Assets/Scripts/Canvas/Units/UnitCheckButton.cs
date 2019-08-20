using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCheckButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Slot slot;
    Button yesUnit;
    public static bool showSlot = false;
    void Start()
    {
        yesUnit=this.transform.GetComponent<Button>();
        yesUnit.onClick.AddListener(yesUnitClick);
    }

    // Update is called once per frame
    void yesUnitClick()
    {
        showSlot = true;
        slot.B();
    }
}
