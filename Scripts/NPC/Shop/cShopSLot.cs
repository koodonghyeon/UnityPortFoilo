using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class cShopSLot : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    
    public bool _Check =false;
    //우클릭체크용
    public PointerEventData.InputButton _MouseBtn = PointerEventData.InputButton.Right;
    //아웃라인
    private Outline _OutLine;
    //아이템
    public Item _item;
    //아이템 이미지
    Image _ItemIcon;
    //아이템 이름
    Text _ItemName;
    //아이템 가격
    Text _ItemPrice;
    //슬롯 번호
    public int _SlotNum;
    //델리게이트
    public delegate void SlotSetting(PointerEventData eventData);
    public SlotSetting _SlotSetting;
   
    AudioSource _ShopAudio;
    AudioClip _ShopClip;
    private void Awake()
    {
        _OutLine = transform.GetChild(1).GetComponent<Outline>();
        _ItemIcon = transform.GetChild(0).GetComponent<Image>();
        _ItemName = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        _ItemPrice = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        _ShopAudio = GetComponent<AudioSource>();
        _ShopClip = Resources.Load<AudioClip>("Sound/coin");
    
    }
  //아이템세팅
    public void SetItem()
    {
        if (_item._Quality == ItemQuality.Unique)
        {
            _ItemName.text = "<color=#FF00B2>" + _item._ItemName + "</color>";
        }
        else if (_item._Quality == ItemQuality.Rare)
        {
            _ItemName.text = "<color=#3232FF>" + _item._ItemName + "</color>";
        }
        else if (_item._Quality == ItemQuality.Normal)
        {
            _ItemName.text = "<color=#FFFFFF>" + _item._ItemName + "</color>";
        }
        _ItemIcon.sprite = _item._ItemIcon;
        _ItemPrice.text = _item._ItemPrice.ToString();
    }

    //마우스 나가면 비활성화
    public void OnPointerExit(PointerEventData eventData)
    {

        _OutLine.enabled = false;
    }

    //마우스올라와있을시 활성화
    public void OnPointerEnter(PointerEventData eventData)
    {
        _OutLine.enabled =true;
    }

    //마우스우클릭시 아이템구매
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == _MouseBtn)
        {
            if (cGameManager.GetInstance.Gold > this._item._ItemPrice)
            {
                cGameManager.GetInstance.Gold -= this._item._ItemPrice;
                cInventory.GetInstance.AddItem(this._item);
                _ShopAudio.clip = _ShopClip;
                _ShopAudio.Play();
                _Check = true;
                _SlotSetting(eventData);
                this.gameObject.SetActive(false);
                cGameManager.GetInstance._DeleGateGold();
            }


        }
    }
}
