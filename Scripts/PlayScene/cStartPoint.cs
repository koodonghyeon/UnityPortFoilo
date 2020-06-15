using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//씬이동시 플레이어 시작지점
public class cStartPoint : MonoBehaviour
{
    public int _startPoint;// 맵이동시 캐릭터 이동위치
    Texture2D _CursorTexture;
    Texture2D _ClickTexture;
    
    void Awake()
    {
        _CursorTexture = Resources.Load<Texture2D>("UI/ShootingCursor1");
        _ClickTexture = Resources.Load<Texture2D>("UI/ShootingCursor2");
        Player.GetInstance.gameObject.SetActive(true);
        cCameramanager.GetInstance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cCameramanager.GetInstance.transform.position.z);
        Player.GetInstance._DashCount = 3;
        Player.GetInstance._Dash.AllTrue();
        cUIManager.GetInstance.gameObject.SetActive(true);
        cUIManager.GetInstance.GetWeaPonSlot().SetItem();
        Player.GetInstance.transform.position = this.transform.position;
        Player.GetInstance._isMoveMap = false;
        cGameManager.GetInstance.SetBackGruond(BackGroundSound.Dungeun);
        cGameManager.GetInstance.SetCursor(_CursorTexture, _ClickTexture);

    }

}
