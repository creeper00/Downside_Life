using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector]
    public int unitIndex;
    [SerializeField]
    private GameManager.Job kindOfUnit;

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            else
            {
                return null;
            }
        }
        set { }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if ( ItemDragHandler.itemBeingDragged != null)
        {
            int itemIndex = ItemDragHandler.itemBeingDraggedIndex;
            //데이터상으로 아이템을 이동, 아이템 스크롤 뷰 갱신
            bool succeedAttach = false;
            switch (kindOfUnit)
            {
                case GameManager.Job.crook:
                    if ( GameManager.instance.attatchedCrooks[unitIndex] != null )
                    {
                        succeedAttach = GameManager.instance.attatchedCrooks[unitIndex].putItem(itemIndex);

                    }
                    if (succeedAttach)
                    {
                        GameManager.instance.crookItems.RemoveAt(itemIndex);
                        UnitsManager.instance.ShowCrookItems();
                    }
                    break;
                case GameManager.Job.snake:
                    if (GameManager.instance.attatchedSnakes[unitIndex] != null)
                    {
                        succeedAttach = GameManager.instance.attatchedSnakes[unitIndex].putItem(itemIndex);
                    }
                    if (succeedAttach)
                    {
                        GameManager.instance.snakeItems.RemoveAt(itemIndex);
                        UnitsManager.instance.ShowSnakeItems();
                    }
                    break;
                case GameManager.Job.gang:
                    if (GameManager.instance.attatchedGangs[unitIndex] != null)
                    {
                        succeedAttach = GameManager.instance.attatchedGangs[unitIndex].PutItem(itemIndex);
                    }
                    if (succeedAttach)
                    {
                        GameManager.instance.gangItems.RemoveAt(itemIndex);
                        UnitsManager.instance.ShowGangItems();
                    }
                    break;
            }
            if ( succeedAttach)
            {
                item = Instantiate(ItemDragHandler.itemBeingDragged, transform);
                item.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
