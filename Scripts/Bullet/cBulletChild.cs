using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//총알의 이미지가 자식오브젝트에 달려있는경우
public class cBulletChild : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<cBullet>().OnTriggerEnter2D(collision);

    }
    public void CrashBullet()
    {
        this.transform.parent.GetComponent<cBullet>().CrashBullet();
    }
}
