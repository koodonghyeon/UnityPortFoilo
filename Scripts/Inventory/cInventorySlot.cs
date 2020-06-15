using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//슬롯타입
public enum SlotType
{
    Basic,
    Weapon,
    Shild
}

//인벤토리 슬롯
public class cInventorySlot : MonoBehaviour,IDragHandler, IPointerExitHandler,IEndDragHandler,IPointerEnterHandler,IBeginDragHandler,IPointerClickHandler
{
    //우클릭체크용
    public PointerEventData.InputButton _MouseBtn = PointerEventData.InputButton.Right;
   
    //무기변경용
    private cWeaPon _Weapon;
    //슬롯넘버
    public int _number;
    //현제슬롯에 있는 아이템
    public Item _item;
    //아이템이 있냐없냐
    public bool _isItem =false;
    //아이템 설명창
    public cItemPanel _Panel;
    //슬롯타입
    public SlotType  _SlotType= SlotType.Basic;
    //아이템 등급
    private string _ItemQuality;
    //아이템 타입
    private string _ItemType;

    AudioSource _SellSound;
    AudioClip _SellClip;
    private void Awake()
    {

        _Panel.gameObject.SetActive(false);
  
        _SellSound = GetComponent<AudioSource>();
        _SellClip = Resources.Load<AudioClip>("Sound/coin2");
         _Weapon = FindObjectOfType<cWeaPon>();
    }

    //아이템 판넬에 셋팅
    public void SetItem(Item _Item)
    {
        if (_Item != null)
        {
            Sprite Image = _Item._ItemIcon;
            _item = _Item;
            if (Image != null)
            {
                _isItem = true;
            }
            else if (Image == null)
            {
                _isItem = false;
            }

            if (_item._Type == ItemType.Sword)
            {
                _ItemType = "한손(주무기)";
            }
            else
            {
                _ItemType = "양손(주무기)";
            }
            if (_item._Quality == ItemQuality.Normal)
            {
                _ItemQuality = "일반 아이템";
            }
            else if (_item._Quality == ItemQuality.Rare)
            {
                _ItemQuality = "희귀 아이템";
            }
            else if (_item._Quality == ItemQuality.Unique)
            {
                _ItemQuality = "전설 아이템";
            }
            _Panel.SetPanel(_item._ItemName, _item._MinAttackDamage, _item._MaxAttackDamage, _item._AttackSpeed, _item._ItemDescrIption, _ItemType, _ItemQuality, _item._ItemIcon,_item._SkillText,_item._SkillIcon);
        }
    }
  
    //드래그중일때 인벤토리 밑의 드래깅 아이템 밑으로 아이템 부모를바꿈
    public void OnDrag(PointerEventData eventData)
    {
        if (transform.childCount > 0)
            transform.GetChild(0).SetParent(cInventory.GetInstance._DraggingItem);
        cInventory.GetInstance._DraggingItem.GetChild(0).position = eventData.position;
    }
    //마우스가 슬롯위에있을때 아이템이있으면 판넬로 설명창 뛰움
    public void OnPointerEnter(PointerEventData eventData)
    {
        cInventory.GetInstance._EnteredSlot = this;
        if (_isItem)
        {
            _Panel.gameObject.SetActive(true);
            _Panel.transform.SetParent(cInventory.GetInstance._GetPanel);
        }
    }

    //마우스가 슬롯 밖으로 나가면 판넬 숨김
    public void OnPointerExit(PointerEventData eventData)
    {
        cInventory.GetInstance._EnteredSlot = null;

        _Panel.gameObject.SetActive(false);
       
    }

    //드래그 끝날때 끝나는 슬롯이 뭐냐에 따라 효과다름 
    public void OnEndDrag(PointerEventData eventData)
    {
        //아까 옮긴 아이템을 다시 자기자식으로 옮김
        cInventory.GetInstance._DraggingItem.GetChild(0).SetParent(transform);
        transform.GetChild(0).localPosition = Vector3.zero;
        
        if (cInventory.GetInstance._EnteredSlot != null)
        {
            //레이캐스트 타겟을 켬
            transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
         //이동하는 슬롯이 무기슬롯이고 첫번째 무기슬롯일때 무기변경하고 현제 장착중인 무기도 변경
            if (cInventory.GetInstance._EnteredSlot._SlotType == SlotType.Weapon && cInventory.GetInstance._EnteredSlot == cInventory.GetInstance.GetWeaponSlot(0))
            {
                ChangeItem();
                _Weapon.SetWeaPon(cInventory.GetInstance._EnteredSlot._item);
                cUIManager.GetInstance.GetWeaPonSlot().SetItem();
                cUIManager.GetInstance.GetSkill().SetImage(cInventory.GetInstance._EnteredSlot._item);
            }
            //1번 무기 슬롯에있는걸 2번무기슬롯이 비어있을때 이동안됨
            else if (this == cInventory.GetInstance.GetWeaponSlot(0)&& cInventory.GetInstance._EnteredSlot == cInventory.GetInstance.GetWeaponSlot(1)&& cInventory.GetInstance.GetWeaponSlot(1)._item._ItemIcon == null)
            {
                return;
            }
            // 1번무기슬롯에 있는 아이템 못뺌
            else if (this == cInventory.GetInstance.GetWeaponSlot(0) && cInventory.GetInstance._EnteredSlot._SlotType == SlotType.Basic && cInventory.GetInstance._EnteredSlot._item._ItemIcon == null)
            {
                return;
            }
            //2번무기 슬롯아이템 집어넣고 UI상에 보이게함
            else if (cInventory.GetInstance._EnteredSlot._SlotType == SlotType.Weapon && cInventory.GetInstance._EnteredSlot == cInventory.GetInstance.GetWeaponSlot(1))
            {
                ChangeItem();
                cUIManager.GetInstance.GetWeaPonSlot().SetItem();
            }
            //일반 슬롯에 아이템이 있으면 무기변경
            else if (cInventory.GetInstance._EnteredSlot._SlotType == SlotType.Basic&& this == cInventory.GetInstance.GetWeaponSlot(0))
            {
                      ChangeItem();
                    _Weapon.SetWeaPon(this._item);
                    cUIManager.GetInstance.GetWeaPonSlot().SetItem();
            }
            //일반 슬롯 끼리는 아이템 위치변경
            else if (cInventory.GetInstance._EnteredSlot._SlotType == SlotType.Basic)
            {

                ChangeItem();
            }
          }
    }
    //슬롯간 아이템 변경
    private void ChangeItem()
    {
        Item tempItem = _item;
        _item = cInventory.GetInstance._EnteredSlot._item;
        cInventory.GetInstance._EnteredSlot._item = tempItem;
        cInventory.GetInstance.ItemImageChange(this);
        cInventory.GetInstance.ItemImageChange(cInventory.GetInstance._EnteredSlot);
    }
    //드래그 시작할때 이미지의 레이캐스트 타겟을 끔
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
    }
    //아이템판매
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == _MouseBtn)
        {
            if (cInventory.GetInstance._Shop._isActiveShop)
            {
                if (this._item != cInventory.GetInstance._EmptyItem)
                {
                    cGameManager.GetInstance.Gold += this._item._ItemPrice / 2;
                    cGameManager.GetInstance._DeleGateGold();
                    this._item = cInventory.GetInstance._EmptyItem.GetComponent<Item>();
                    _SellSound.clip = _SellClip;
                    _SellSound.Play();
                    cInventory.GetInstance.ItemImageChange(this);
                    }
            }


        }
    }
}
