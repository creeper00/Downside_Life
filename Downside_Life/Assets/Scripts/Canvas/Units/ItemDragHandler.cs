using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static GameObject canvas;
    public static GameObject itemBeingDragged;
    public static int itemBeingDraggedIndex;
    private Color32 halfTransparentColor = new Color32(255, 255, 255, 128);

    void Awake()
    {
        canvas = GameObject.Find("UnitsCanvas");
        itemBeingDragged = null;
        halfTransparentColor = new Color32(255, 255, 255, 128);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = Instantiate(gameObject.transform.Find("Icon").gameObject, canvas.transform);
        itemBeingDragged.GetComponent<Image>().color = halfTransparentColor;
        itemBeingDragged.AddComponent<CanvasGroup>();
        itemBeingDragged.GetComponent<CanvasGroup>().blocksRaycasts = false;

        itemBeingDraggedIndex = gameObject.GetComponent<ItemListItem>().index;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemBeingDragged.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DestroyItem();
    }

    public static void DestroyItem()
    {
        if (itemBeingDragged != null)
        {
            Destroy(itemBeingDragged);
            itemBeingDragged = null;
        }
    }
}
