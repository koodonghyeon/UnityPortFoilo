using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//무기 회전용 스크립트
public class cWeaPonMove : MonoBehaviour
{

    cWeaPon _WeaPon;
    private void Start()
    {
        _WeaPon = transform.GetChild(1).GetComponent<cWeaPon>();
    }
    void Update()
    {
        if (Player.GetInstance._state != State.Die)
        {
            if (Time.timeScale != 0)
            {
                if (_WeaPon._isAttack == false)
                {
                    Vector3 _mousePos = Input.mousePosition; //마우스 좌표 저장
                    Vector3 _oPosition = transform.position;
                    Vector3 target = Camera.main.ScreenToWorldPoint(_mousePos);
                    float dy = target.y - _oPosition.y;
                    float dx = target.x - _oPosition.x;
                    float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);


                }
            }
        }
     }
}
