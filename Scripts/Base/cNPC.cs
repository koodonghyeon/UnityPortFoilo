using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NPC최상단
public class cNPC : MonoBehaviour
{
    //보일 F버튼
    public GameObject _ButtonF;
    //플레이어랑 충돌중인지
    protected bool _isPlayer=false;
    public bool _isNPC = false;
    protected virtual void Awake()
    {
        _ButtonF = transform.GetChild(0).gameObject;
    }
    //플레이어 충돌여부 및 F버튼 활성화 여부체크
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _ButtonF.SetActive(true);
            _isPlayer = true;
        }

    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _ButtonF.SetActive(false);
            _isPlayer = false;
        }
    }
}
