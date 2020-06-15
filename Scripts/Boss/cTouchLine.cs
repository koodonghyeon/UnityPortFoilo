using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스맵에 보스등장하는 바운드
public class cTouchLine : MonoBehaviour
{
    //보스가 등장했나안했나여부
    bool _isBoss=false;
    //보스
    public SkelBoss _Boss;
    //보스맵
    public cBossMap _Map;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {//보스가없고
        if (!_isBoss)
        {//플레이어랑 충돌되면 호출
            if(collision.gameObject.CompareTag("Player")){
                _Boss.gameObject.SetActive(true);
                _isBoss = true;
                _Map.SetBoss();
                _Map._isBox = false;
                cGameManager.GetInstance.SetBackGruond(BackGroundSound.Boss);
                cCameramanager.GetInstance.transform.position = this.transform.position;
                _Map._Boss = true;
            }
        }
    }
  
}
