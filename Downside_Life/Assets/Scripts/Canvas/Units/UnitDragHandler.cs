using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private static GameObject canvas;
    [HideInInspector]
    public static GameObject unitBeingDragged;
    [HideInInspector]
    public static int itemBeingDraggedIndex;
    private Color32 halfTransparentColor = new Color32(255, 255, 255, 128);

    //private Transform initialParent;

    void Awake()
    {
        canvas = GameObject.Find("UnitsCanvas");
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

        if (unitBeingDragged != null )
        {
            Destroy(unitBeingDragged);
            unitBeingDragged = null;
        }

        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
