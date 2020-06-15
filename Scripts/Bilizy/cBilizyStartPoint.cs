using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//마을씬에서 마지막에 호출되어서 플레이어 초기화하는녀석
public class cBilizyStartPoint : MonoBehaviour
{
    Texture2D _CursorTexture;
    Texture2D _ClickTexture;

    void Awake()
    {
        _CursorTexture = Resources.Load<Texture2D>("UI/BasicCursor");
        _ClickTexture = Resources.Load<Texture2D>("UI/BasicCursor");

        Player.GetInstance.gameObject.SetActive(true);
        Player.GetInstance._Box2D.isTrigger = true;
        cCameramanager.GetInstance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cCameramanager.GetInstance.transform.position.z);
        cUIManager.GetInstance.gameObject.SetActive(true);
        Player.GetInstance.transform.position = this.transform.position;
        Player.GetInstance._isMoveMap = false;
        cGameManager.GetInstance.PlayerReset();
        cGameManager.GetInstance.SetBackGruond(BackGroundSound.Bilizy);
        cGameManager.GetInstance.SetCursor(_CursorTexture, _ClickTexture);
        cCameramanager.GetInstance.SetTarget(Player.GetInstance.gameObject, 5.0f);
      
        cInventory.GetInstance.InventoryReset();
        for(int i=0; i < 10; ++i)
        {
            cInventory.GetInstance.AddItem(cDataBaseManager.GetInstance._ItemList[i]);
        }
    }
    private void Start()
    {
        cUIManager.GetInstance.GetWeaPonSlot().SetItem();
    }
}
