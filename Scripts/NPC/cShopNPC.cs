using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cShopNPC : cNPC
{
    //상점이 열려있나여부
    public bool _isActiveShop=false;
    //상점
    public GameObject _Shop;
    protected override void Awake()
    {

        base.Awake();
        _Shop.SetActive(false);
    }

    void Update()
    {
        if (_isPlayer)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenShop();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseShop();
            }
        }
    }
    //상점닫기
    public void CloseShop()
    {
        if (_isActiveShop)
        {
 
            cInventory.GetInstance.SetActive();
            cGameManager.GetInstance.DeleteNPC();
            Time.timeScale = 1;
            _isActiveShop = false;
            _Shop.SetActive(false);
            _isNPC = false;
          
        }
    }
    //상점열기
    private void OpenShop()
    {
        if (!_isActiveShop)
        {
      
            cInventory.GetInstance.SetActive();
            cGameManager.GetInstance.SetNPC(this);
            Time.timeScale = 0;
            _isActiveShop = true;
            _Shop.SetActive(true);
            _isNPC = true;
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
