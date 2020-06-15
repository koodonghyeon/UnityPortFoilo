using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//음식점 NPC
public class FoodNPC : cNPC
{

    //음식점
    public GameObject _FoodTable;
    protected override void Awake()
    {
        base.Awake();
    }
    
    private void Update()
    {
        if (_isPlayer)
        {

            if (Input.GetKeyDown(KeyCode.F)) {

                SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetActive(false);
            }
        }
    }
    //인벤토리 껏다켰다하는 함수
    public void SetActive(bool Active)
    {
        if (Active) {
            cGameManager.GetInstance.SetNPC(this);
            _FoodTable.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
       else if (!Active)
        {
            Time.timeScale = 1;
            _FoodTable.gameObject.SetActive(false);
            cGameManager.GetInstance.DeleteNPC();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
