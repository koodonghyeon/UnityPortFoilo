using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//맵 열리고 닫히는 문
public class cDoor : MonoBehaviour
{
    private Animator _Ani;
    private BoxCollider2D _Box;
    void Start()
    {
        _Ani = GetComponent<Animator>();
        _Box = GetComponent<BoxCollider2D>();
    }
    public void Close()
    {
        _Ani.SetTrigger("Close");
        _Box.enabled = true;
    }
    public void Open()
    {
        _Ani.SetTrigger("Open");
        _Box.enabled = false;
    }
}
