using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static GameObject canvas;
    public static GameObject unitBeingDragged;
    public static int itemBeingDraggedIndex;
    private Color32 halfTransparentColor;

    //private Transform initialParent;

    void Awake()
    {
        canvas = GameObject.Find("UnitsCanvas");
        unitBeingDragged = null;
        halfTransparentColor = new Color32(255, 255, 255, 128);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        unitBeingDragged = Instantiate(gameObject.transform.Find("Icon").gameObject, canvas.transform);
        unitBeingDragged.GetComponent<Image>().color = halfTransparentColor;
        unitBeingDragged.AddComponent<CanvasGroup>();
        unitBeingDragged.GetComponent<CanvasGroup>().blocksRaycasts = false;

        itemBeingDraggedIndex = gameObject.GetComponent<UnitListItem>().index;
        
        //initialParent = itemBeingDragged.transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        unitBeingDragged.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*
         * 초기 테스트용 코드
        if (itemBeingDragged.transform.parent == initialParent)
        {
            Debug.Log("same");
            GameObject.Destroy(itemBeingDragged);
        }
        else
        {
            Debug.Log("different");
        }
        */

        if (unitBeingDragged != null)
        {
            Destroy(unitBeingDragged);
            unitBeingDragged = null;
        }

        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
