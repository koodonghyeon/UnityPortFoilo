using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//현제 재생될 백그라운드
public enum BackGroundSound
{
    Bilizy,
    Dungeun,
    Shop,
    FoodShop,
    Boss,
    GameStart

}

//게임매니저
public class cGameManager : cSingleton<cGameManager>
{
    //현제 장착중인무기
    public cWeaPon _WeaPon;
    //옵션창
    public cOptionPanel _Option;
    //일시정지창
    public GameObject _StopPanel;
    //죽었을떄 호출되는창
    public GameObject _Dead;
    //엔피씨
    public cNPC NPC;
    public cStat _Stat;
    bool _isDie=false;
    //골드
    private static float _Gold = 0;
   public float Gold { set{ _Gold = value; } get { return _Gold; }}

    //골드 딜리게이트
    public delegate void Gold_Del();
    public Gold_Del _DeleGateGold;
    //배경음악재생용 오디오소스
    AudioSource _BackGround;
    //클립리스트
    List<AudioClip> _BackGroundClip = new List<AudioClip>();

    //마우스커서
    private Texture2D _CursorTexture;
    private Texture2D _ClickTexture;
    private CursorMode _CursorMode = CursorMode.Auto;
    protected override void Awake()
    {
        base.Awake();
        _BackGround = GetComponent<AudioSource>();
        _BackGround.loop = true;
        _BackGroundClip.Add(Resources.Load<AudioClip>("Sound/0.Town"));
        _BackGroundClip.Add(Resources.Load<AudioClip>("Sound/1.JailField"));
        _BackGroundClip.Add(Resources.Load<AudioClip>("Sound/Shop"));
        _BackGroundClip.Add(Resources.Load<AudioClip>("Sound/Foodshop"));
        _BackGroundClip.Add(Resources.Load<AudioClip>("Sound/dead"));
        _BackGroundClip.Add(Resources.Load<AudioClip>("Sound/1.JailBoss"));
        _BackGroundClip.Add(Resources.Load<AudioClip>("Sound/dodoo"));
        _CursorTexture = Resources.Load<Texture2D>("UI/BasicCursor");
        _ClickTexture = Resources.Load<Texture2D>("UI/BasicCursor");
        Cursor.SetCursor(_CursorTexture, Vector2.zero, _CursorMode);
        _StopPanel.SetActive(false);
    }
    private void Start()
    {
        _DeleGateGold();
    }

    private void Update()
    {
        //탭누를시 무기 1번 슬롯 2번슬롯 변경
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (cInventory.GetInstance.GetWeaponSlot(1)._item._ItemIcon != null)
            {
                cInventory.GetInstance.ChangeWeapon();
                cUIManager.GetInstance.GetSkill().SetImage(cInventory.GetInstance.GetWeaponSlot(0)._item);
                cUIManager.GetInstance.GetWeaPonSlot().ChangeSlot();
                _WeaPon.SetWeaPon(cInventory.GetInstance.GetWeaponSlot(0)._item);
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_Stat.gameObject.activeSelf == false)
            {
                _Stat.gameObject.SetActive(true);
            }
            else if (_Stat.gameObject.activeSelf == true)
            {
                _Stat.gameObject.SetActive(false);
            }
        }
        //인벤토리 열기
        if(Input.GetKeyDown(KeyCode.I))
        {
            cInventory.GetInstance.SetActive();
        }
        //ESC누를시 일시정지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetStop();


        }
        //클릭시마우스버튼변경
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(_ClickTexture, Vector2.zero, _CursorMode);
        }
        
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(_CursorTexture, Vector2.zero, _CursorMode);
        }
        //플레이어 죽으면 할것
        if (Player.GetInstance._state == State.Die)
        {
            if (!_isDie) { 
            _BackGround.Stop();
            _BackGround.loop = false;
            _BackGround.clip = _BackGroundClip[4];
            _BackGround.Play();
                Player.GetInstance._Ani.SetTrigger("DIe");
            _Dead.SetActive(true);
                _isDie = true;
            }
        }
    }
    //백그라운드 소리재생
    public void SetBackGruond(BackGroundSound back)
    {
        if (back == BackGroundSound.Bilizy) 
        {
            _BackGround.loop = true;
            _BackGround.clip = _BackGroundClip[0];
        }
        else if (back == BackGroundSound.Dungeun) 
        {
            _BackGround.loop = true;
            _BackGround.clip = _BackGroundClip[1];
        }
        else if (back == BackGroundSound.Shop)
        {
            _BackGround.loop = true;
            _BackGround.clip = _BackGroundClip[2];
        }
        else if (back == BackGroundSound.FoodShop) 
        {
            _BackGround.loop = true;
            _BackGround.clip = _BackGroundClip[3];
        }
        else if (back == BackGroundSound.Boss)
        {
            _BackGround.loop = true;
            _BackGround.clip = _BackGroundClip[5];
        }
        else if (back == BackGroundSound.GameStart)
        {
            _BackGround.loop = false;
            _BackGround.clip = _BackGroundClip[6];
        }
        _BackGround.Play();
    }
    //커서변경
    public void SetCursor(Texture2D Cuser, Texture2D Click)
    {
        _CursorTexture = Cuser;
        _ClickTexture = Click;
        Cursor.SetCursor(_CursorTexture, Vector2.zero, _CursorMode);
    }
    //esc일시정지판넬
    public void SetOptionPanel()
    {
      
            _StopPanel.SetActive(false);
            _Option.gameObject.SetActive(true);
        
    }
    public void SetStop()
    {
        if (NPC != null)
        {
            if (!NPC._isNPC)
            {
                return;
            }
        }
        else if (NPC == null)
        {
            _StopPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void CloseStop()
    {
        Time.timeScale = 1;
        _StopPanel.SetActive(false);
    }
    //죽으면 플레이어 리셋
    public void PlayerReset()
    {
        Player.GetInstance._health.MyCurrentValue = 80;
        Player.GetInstance._health._MaxValue = 80;
        Player.GetInstance._Power = 0;
        Player.GetInstance._MoveSpeed = 5.0f;
        Player.GetInstance._Defense = 0;
       Player.GetInstance.transform.rotation = Quaternion.identity;
        Player.GetInstance._state = State.Idle;
        cInventory.GetInstance.InventoryReset();
        cUIManager.GetInstance.GetStat().SetStat();
        _Dead.SetActive(false);
    }
    //현제 NPC가 뭐가 열려있나
    public void SetNPC(cNPC npc)
    {
        NPC = npc;
    }
    public void DeleteNPC()
    {
        NPC = null;
    }
}
