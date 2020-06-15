using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//텔레포트용 
public class cWorldMap : MonoBehaviour
{  
    //열린칸
    public List<Transform> _WorldMapOpenList = new List<Transform>();
    //닫힌칸
    public List<Transform> _WorldMapCloseList = new List<Transform>();


    void Awake()
    {
        //첫번째맵 열어주고
        _WorldMapOpenList.Add(transform.GetChild(0));
        //나머지맵 닫힌리스트
        for (int i = 1; i < transform.childCount; ++i)
        {
            _WorldMapCloseList.Add(transform.GetChild(i));
        }
        //닫힌맵 비활성화
        for (int i = 0; i < _WorldMapCloseList.Count; ++i)
        {
            _WorldMapCloseList[i].gameObject.SetActive(false);
        }

    }
    //오픈칸 열어주기
    public void Open()
    {
        for (int i = 0; i < _WorldMapOpenList.Count; ++i)
        {
            _WorldMapOpenList[i].gameObject.SetActive(true);
        }
    }

}
