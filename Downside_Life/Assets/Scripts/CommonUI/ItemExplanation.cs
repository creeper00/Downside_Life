using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemExplanation : MonoBehaviour
{
    GameObject popUp;
    GameObject unitExplanationCanvas;
    GameObject unitExplanationPanel;
    GameObject unitExplanationText;
    int type;
    int grade;
    int itemCode;
    public void Awake()
    {
        popUp = GameObject.Find("Popups");
        unitExplanationCanvas = popUp.transform.Find("UnitExplanationCanvas").gameObject;
        unitExplanationPanel = unitExplanationCanvas.transform.Find("UnitExplanationPanel").gameObject;
        unitExplanationText = unitExplanationPanel.transform.Find("ExplanationText").gameObject;
    }

    public void ItemIconPopUp()
    {
        unitExplanationCanvas.SetActive(true);
        type = gameObject.GetComponent<ItemListItem>().type;
        grade = gameObject.GetComponent<ItemListItem>().grade;
        itemCode = gameObject.GetComponent<ItemListItem>().itemCode;
        unitExplanationPanel.transform.position = gameObject.transform.position;
        unitExplanationPanel.transform.position += new Vector3(0f, 15f, 0f);
        unitExplanationText.GetComponent<Text>().fontSize = 17;
        if (grade == 0)
        {
            unitExplanationText.GetComponent<Text>().text = "Normal\n";
            if (type == 0)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "사기꾼의 정석\n사기꾼의 모든 것! " +
                        "사기꾼의 정석이다. 사기꾼에게 주면 그 사기꾼이 만들어내는 지출이 10% 증가한다.";
                }
                if (itemCode == 2)
                {
                    unitExplanationText.GetComponent<Text>().text += "돈주머니\n돈을 많이 담을 수 있을 것만 같은 " +
                        "돈주머니이다. 사기꾼에게 주면 그 사기꾼이 들고오는 돈이 15% 증가한다";
                }
            }
            if (type == 1)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "화장품\n외모를 가꿀 수 있는 화장품이다. " +
                        "꽃뱀의 특성을 10% 강화한다. (주기증가형 꽃뱀 제외)";
                }
            }
            if (type == 2)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "권총\n부자가 호신용으로 들고있던 권총이다. " +
                        "갱단에게 주면 그 갱단 공격력이 20% 증가한다. ";
                }
                if (itemCode == 2)
                {
                    unitExplanationText.GetComponent<Text>().text += "돈보따리\n돈을 많이 담을 수 있을 것만 같은 돈보따" +
                        "리이다. 갱단에게 주면 그 갱단이 들고오는 돈이 20% 증가한다.";
                }
            }
        }
        if (grade == 1)
        {
            unitExplanationText.GetComponent<Text>().text = "Rare\n";
            if (type == 0)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "사기꾼의 정석\n사기꾼의 모든 것! 사기꾼의 정석" +
                        " 심화판이다. 사기꾼에게 주면 그 사기꾼이 만들어내는 지출이 20% 증가한다.";
                }
                if (itemCode == 1)
                {
                    unitExplanationText.GetComponent<Text>().text += "왁스\n바르면 이미지를 360도 바꿀 수 있는 " +
                        "왁스이다. 사기꾼 한 명의 유형을 변경한다 ";
                }
            }
            if (type == 1)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "고급 화장품\n외모를 더 가꿀 수 있는 화장품이다. " +
                        "꽃뱀의 특성을 20% 강화한다.(주기증가형 꽃뱀 제외)";
                }
                if (itemCode == 1)
                {
                    unitExplanationText.GetComponent<Text>().text += "원피스\n분위기를 바꿀 수 있는 원피스이다. 사용시" +
                        " 꽃뱀 한 명의 유형을 변경한다.";
                }
            }
            if (type == 2)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "기관단총\n살상력이 더 업그레이드 된 돌격소총이다. " +
                        "갱단에게 주면 그 갱단 공격력이 40% 증가한다. ";
                }
                if (itemCode == 1)
                {
                    unitExplanationText.GetComponent<Text>().text += "헤나\n다른 파벌의 타투를 그릴 수 있는 아이템. " +
                        "갱단 한 유닛의 유형을 변경한다.";
                }
                if (itemCode == 2)
                {
                    unitExplanationText.GetComponent<Text>().text += "과전류 장치\n과전류를 흘려 공장의 두꺼비집을" +
                        " 터트린다. 갱단에게 주고 업그레이드 중인 공장 공격시 업그레이드가 한 턴 지연된다. ";
                }
            }
        }
        if (grade == 2)
        {
            unitExplanationText.GetComponent<Text>().text = "Legendary\n";
            if (type == 0)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "사기꾼의 정석 Limited Edition\n사기꾼의" +
                        " 모든 것! 국내에 10권밖에 없는사기꾼의 정석 Limited Edition이다. 사기꾼에게 주면 " +
                        "그 사기꾼이 만들어내는 지출이 40% 증가한다.";
                }
            }
            if (type == 1)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "성인잡지\n부자의 손때가 묻은 성인 잡지이다. " +
                        "이런 취향이었군. 둔감형 꽃뱀의 효과를 30% 증가한다.";
                }
                if (itemCode == 1)
                {
                    unitExplanationText.GetComponent<Text>().text += "성인잡지\n부자의 손때가 묻은 성인 잡지이다. " +
                        "이런 취향이었군. 낭비형 꽃뱀의 효과를 30% 증가한다.";
                }
                if (itemCode == 2)
                {
                    unitExplanationText.GetComponent<Text>().text += "성인잡지\n부자의 손때가 묻은 성인 잡지이다. " +
                        "이런 취향이었군. 갈취형 꽃뱀의 효과를 30% 증가한다.";
                }
            }
            if (type == 2)
            {
                if (itemCode == 0)
                {
                    unitExplanationText.GetComponent<Text>().text += "Sekom 설계도\nSekom의 취약점을 알 수 있는 " +
                        "설계도이다. Sekom에게 주는 데미지가 30% 증가한다. ";
                }
                if (itemCode == 1)
                {
                    unitExplanationText.GetComponent<Text>().text += "어그로 공장 설계도\n어그로 공장의 취약점을 알 수 있는 " +
                        "설계도이다. 어그로 공장에게 주는 데미지가 30% 증가한다. ";
                }
                if (itemCode == 2)
                {
                    unitExplanationText.GetComponent<Text>().text += "변호사 사무소 설계도\n변호사 사무소의 취약점을 알 수 있는 " +
                        "설계도이다. 변호사 사무소에게 주는 데미지가 30% 증가한다. ";
                }
                if (itemCode == 3)
                {
                    unitExplanationText.GetComponent<Text>().text += "주식 연구소 설계도\n주식 연구소의 취약점을 알 수 있는 " +
                        "설계도이다. 주식 연구소에게 주는 데미지가 30% 증가한다. ";
                }
                if (itemCode == 4)
                {
                    unitExplanationText.GetComponent<Text>().text += "일반 공장 설계도\n일반 공장의 취약점을 알 수 있는 " +
                        "설계도이다. 일반 공장에게 주는 데미지가 30% 증가한다. ";
                }
            }
        }

    }
    public void itemIconPopDown()
    {
        unitExplanationCanvas.SetActive(false);
    }
}
