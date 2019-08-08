using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private static GameObject canvas;
    private GameObject itemBeingDragged;

    void Awake()
    {
        canvas = GameObject.Find("UnitsCanvas");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = Instantiate(gameObject.transform.Find("Icon").gameObject, canvas.transform);
        itemBeingDragged.GetComponent<Image>().color = new Color32(255, 255, 255, 128);
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemBeingDragged.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject.Destroy(itemBeingDragged);
        itemBeingDragged = null;
    }
}
