using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//포탈이동
public class cPortal : MonoBehaviour
{
    //도착지점
    private GameObject _EndPoint;
    private void Awake()
    {
        _EndPoint = transform.GetChild(0).gameObject;
    }

    //충돌시 캐릭터를 엔드지점으로 옮김
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Player")
        {

            other.transform.position = _EndPoint.transform.position;
        }
    }


}
