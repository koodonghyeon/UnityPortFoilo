using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//월드맵 클릭시 호출될 버튼이벤트
public class ButtonEnvet : MonoBehaviour,IPointerUpHandler
{
    //텔레포드 월드맵
    public GameObject WorldMap;

    public void OnPointerUp(PointerEventData eventData)
    {
        Player.GetInstance.transform.position = transform.GetChild(0).position;
        WorldMap.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1128, 0);
    }

}
