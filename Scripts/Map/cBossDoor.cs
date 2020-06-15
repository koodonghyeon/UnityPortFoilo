using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//보스맵 입장하는 문
public class cBossDoor : cNPC
{

    protected override void Awake()
    {

        base.Awake();
    }

    void Update()
    {
        if (_isPlayer)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                cSceneManager.GetInstance.ChangeScene("BossMap", null, 0);
            }
            
        }
    }
    //플레이어 충돌여부 및 F버튼 활성화 여부체크
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
