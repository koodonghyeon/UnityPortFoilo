using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스맵 카메라영역
public class cBossMapBound : MonoBehaviour
{
   public Camera _MinimapCamera;
   public GameObject _CamaraPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cCameramanager.GetInstance.SetBound(this.gameObject.GetComponent<BoxCollider2D>());
            _MinimapCamera.transform.position = _CamaraPosition.transform.position;

        }
    }
}
