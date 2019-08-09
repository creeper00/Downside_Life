using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private GameObject slotPrefab;

    public GameObject item
    {
        get
        {
            if ( transform.childCount > 0 )
            {
                return transform.GetChild(0).gameObject;
            }
            else
            {
                return null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("detected drop");
        if ( item == null )
        {
            GameObject slotObject = Instantiate(slotPrefab, transform);
            //UnitDragHandler.itemBeingDragged.transform.SetParent(transform);
        }
    }
}
