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
            bool succeedAttach = false;                         //데이터 상으로 아이템 이동 성공 여부
            switch (kindOfUnit)
            {
                case GameManager.Job.crook:
                    if ( GameManager.instance.attatchedCrooks[unitIndex] != null )
                    {
                        succeedAttach = GameManager.instance.attatchedCrooks[unitIndex].putItem(itemIndex, unitIndex);     //만약 가능하면 데이터 상으로 아이템을 이동

                    }
                    if (succeedAttach)
                    {
                        ItemDragHandler.DestroyItem();                  //끌고 움직이던 반투명 아이템 스프라이트 파괴
                        transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.crookItems[itemIndex].getIcon();      //아이템 슬롯에 아이콘 설정
                        GameManager.instance.crookItems.RemoveAt(itemIndex);                        //데이터 상으로 아이템을 파괴
                        GameObject.Find("Popups").transform.Find("UnitExplanationCanvas").gameObject.SetActive(false);      //혹시나 아이템 설명 창이 남아 있으면 지워야 함
                        UnitsManager.instance.ShowCrookItems();                 //아이템 리스트 갱신
                    }
                    break;
                case GameManager.Job.snake:
                    if (GameManager.instance.attatchedSnakes[unitIndex] != null)
                    {
                        succeedAttach = GameManager.instance.attatchedSnakes[unitIndex].putItem(itemIndex, unitIndex);
                    }
                    if (succeedAttach)
                    {
                        ItemDragHandler.DestroyItem();
                        transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.snakeItems[itemIndex].getIcon();
                        GameManager.instance.snakeItems.RemoveAt(itemIndex);
                        GameObject.Find("Popups").transform.Find("UnitExplanationCanvas").gameObject.SetActive(false);
                        UnitsManager.instance.ShowSnakeItems();
                    }
                    break;
                case GameManager.Job.gang:
                    break;
            }
        }
    }
}
