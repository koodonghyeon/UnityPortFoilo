using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cInventory :  cSingleton<cInventory>
{
    //상점 판매
   public cShopNPC _Shop;
    //인번토리 활성화여부
    bool _ActiveInventory = false;
    //인벤토리
    private Transform _Inventory;
    //아이템슬롯 프리팹
    public Transform _Slot;
    //무기슬롯 프리팹
    public Transform _WeaPonSlot;
    //방어구 슬롯 프리팹
    public Transform _ShildSlot;
    //아이템 설명창 부모용판넬
    private Transform _Panel;
    //현재 마우스가 올려져있는 슬롯
    public cInventorySlot _EnteredSlot;
    //아이템 슬롯 리스트
    public List<cInventorySlot> _InventorySlot = new List<cInventorySlot>();
    //무기슬롯 리스트
    private List<cInventorySlot> _WeaPonSlotList = new List<cInventorySlot>();
    //방어구 슬롯 리스트
    private List<cInventorySlot> _ShildSlotList = new List<cInventorySlot>();
    //기본무기셋팅용
    public cWeaPon _NowWeaPon;
    //드래그중인 아이템 부모용 슬롯
    private Transform _draggingItem;
    //현제 골드
    private Text _Gold;
    //설명창 부모용 판넬 프로퍼티
    public Transform _GetPanel { get { return _Panel; } }
    //드래그 아이템 부모용 판넬 프로퍼티
    public Transform _DraggingItem {get { return _draggingItem; } }
    //프리펩 설정용 X좌표
    float X = -278.0f;
    //프리펩 설정용 Y좌표
    float Y = -51.0f;
    //인벤토리 열때 나는 소리
    AudioSource _Audio;
    AudioClip _Clip;
    //빈칸에 채워놓을 빈아이템
    public GameObject _EmptyItem;
    protected override void Awake()
    {
        base.Awake();
        _EmptyItem = Resources.Load("Prefabs/Inventory/EmptyItem") as GameObject;
        _Inventory = transform.GetChild(0);
        _draggingItem = transform.GetChild(0).GetChild(1);
        _Panel = transform.GetChild(0).GetChild(2);
        _Inventory.gameObject.SetActive(_ActiveInventory);
        _Gold = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        _Audio = GetComponent<AudioSource>();
        _Clip = Resources.Load<AudioClip>("Sound/OpenInventory");
        _Audio.clip = _Clip;
        //아이템 슬롯 세팅
        for (int i = 0; i < 3; i++)
        {
      
            for (int j = 0; j < 5; j++)
            {
                Transform newSlot = Instantiate(_Slot);
              
                newSlot.name = "Slot" + (i + 1) + "." + (j + 1);
                newSlot.SetParent(_Inventory.GetChild(0).transform);
                newSlot.localPosition= new Vector3(X,Y,0);
                _InventorySlot.Add(newSlot.GetComponent<cInventorySlot>());
                newSlot.GetComponent<cInventorySlot>()._number = i * 5 + j;
                X += 140.0f;
                if (j == 4)
                    X = -278.0f;
            }
            Y -= 127.0f;
        }
        Y = 296.0f;
        X = -244.0f;
        //무기슬롯 세팅
        for(int i = 0; i < 2; ++i)
        {
            Transform WeaponSLot = Instantiate(_WeaPonSlot);
            WeaponSLot.name = "WeaPon" + i;
            WeaponSLot.SetParent(_Inventory.GetChild(0).transform);
            WeaponSLot.localPosition = new Vector3(X, Y, 0);
            _WeaPonSlotList.Add(WeaponSLot.GetComponent<cInventorySlot>());
            WeaponSLot.GetComponent<cInventorySlot>()._number = i + 1;
            X += 353.0f;
        }

        X = -100.0f;
        //방어구 슬롯세팅
        for (int i = 0; i < 2; ++i)
        {
            Transform ShildSlot = Instantiate(_ShildSlot);
            ShildSlot.name = "Shild" + i;
            ShildSlot.SetParent(_Inventory.GetChild(0).transform);
            ShildSlot.localPosition = new Vector3(X, Y, 0);
            _ShildSlotList.Add(ShildSlot.GetComponent<cInventorySlot>());
            ShildSlot.GetComponent<cInventorySlot>()._number = i + 1;
            X += 353.0f;
        }



        //옵저버패턴
        cGameManager.GetInstance._DeleGateGold += SetGold;

        _Shop = FindObjectOfType<cShopNPC>();
    }
    private void Start()
    {
      
        _WeaPonSlotList[0]._item = cDataBaseManager.GetInstance._ItemList[0];
        ItemImageChange(_WeaPonSlotList[0]);
        _NowWeaPon.SetWeaPon(_WeaPonSlotList[0]._item);
   
    }

    //인벤토리 껏다켰다하는 함수
    public void SetActive()
    {
        if (_ActiveInventory)
        {
            Time.timeScale = 1;
            _Inventory.gameObject.SetActive(false);
        }
        else if (!_ActiveInventory)
        {
            Time.timeScale = 0;
            _Audio.Play();
            _Inventory.gameObject.SetActive(true);
        }

        _ActiveInventory = !_ActiveInventory;

    }
    //인벤토리에 골드 세팅하는 함수
    public void SetGold()
    {
        _Gold.text = cGameManager.GetInstance.Gold.ToString();
    }
  //현제 선택된 슬롯 반환
    public cInventorySlot GetWeaponSlot(int num)
    {
        return _WeaPonSlotList[num];
    }
    //무기슬롯 1번 2번 변경
    public void ChangeWeapon()
    {
        if (_WeaPonSlotList[1]._isItem == true)
        {
            Item TempItem = _WeaPonSlotList[0]._item;
            _WeaPonSlotList[0]._item = _WeaPonSlotList[1]._item;
            _WeaPonSlotList[1]._item = TempItem;
            ItemImageChange(_WeaPonSlotList[0]);
            ItemImageChange(_WeaPonSlotList[1]);
        }
    }
    public void InventoryReset()
    {
        Item Empty = _EmptyItem.GetComponent<Item>();
        for (int i = 0; i < _InventorySlot.Count; i++)
        {
         
                _InventorySlot[i]._item = Empty;
                _InventorySlot[i]._isItem = false;
                ItemImageChange(_InventorySlot[i]);
                
           
        }
        }
    //아이템 추가 아이템슬롯 빈칸부터 채움
    public void AddItem(Item item)
    {
        for (int i = 0; i < _InventorySlot.Count; i++)
        {
            if (!_InventorySlot[i]._isItem)
            {
                _InventorySlot[i]._item = cDataBaseManager.GetInstance._ItemList.Find(x=>x._ItemName == item._ItemName);
                ItemImageChange(_InventorySlot[i]);
                break;
            }
        }
    }

    //아이템 이미지 체인지
    public void ItemImageChange(cInventorySlot Slot)
    {
       
            Slot.SetItem(Slot._item);
            Slot.transform.GetChild(0).GetComponent<Image>().sprite = Slot._item._ItemIcon;
       
    }
}

