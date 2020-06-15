using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라 영역을 마을박스에 넣는녀석
public class cBilizyBound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cCameramanager.GetInstance.SetBound(this.gameObject.GetComponent<BoxCollider2D>());


        }
    }
}
