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
    public static GameObject itemBeingDragged;
    private Transform initialParent;

    void Awake()
    {
        canvas = GameObject.Find("UnitsCanvas");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = Instantiate(gameObject.transform.Find("Icon").gameObject, canvas.transform);
        itemBeingDragged.GetComponent<Image>().color = new Color32(255, 255, 255, 128);
        itemBeingDragged.AddComponent<CanvasGroup>();
        itemBeingDragged.GetComponent<CanvasGroup>().blocksRaycasts = false;

        initialParent = itemBeingDragged.transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemBeingDragged.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemBeingDragged.transform.parent == initialParent)
        {
            Debug.Log("same");
            GameObject.Destroy(itemBeingDragged);
        }
        else
        {
            Debug.Log("different");
        }
        itemBeingDragged = null;
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
