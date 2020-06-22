using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//지렁이터널 (텔레포트용)
public class WormPassage : cNPC
{
    public bool _isActiveMap;
    public GameObject _Map;
    public bool mobCheck=false;

    protected override void Awake()
    {

        base.Awake();
        _isActiveMap=false;
        if (_Map.GetComponent<RectTransform>().anchoredPosition.y !=  1128)
        {
            _Map.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1128, 0);
        }
    }

    void Update()
    {
        if (_isPlayer && mobCheck)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenMap();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseMap();
            }
        }
    }
    //닫기
    public void CloseMap()
    {
      
            cGameManager.GetInstance.DeleteNPC();
   
            _Map.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1128, 0);

    }
    //열기
    private void OpenMap()
    {
    
            cGameManager.GetInstance.SetNPC(this);
         
            _Map.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
   
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
