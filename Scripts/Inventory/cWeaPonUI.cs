using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//무기UI
public class cWeaPonUI: MonoBehaviour
{
    //1번 UI무기슬롯
    private Image _SlotIamge1;
    //2번UI무기 슬롯
    private Image _SlotIamge2;

    private void Start()
    {
        _SlotIamge1 = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _SlotIamge2 = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _SlotIamge1.sprite = cInventory.GetInstance.GetWeaponSlot(0)._item._ItemIcon;
       _SlotIamge2.sprite = cInventory.GetInstance.GetWeaponSlot(1)._item._ItemIcon;
     
    }

    //현제 장착중인 무기로 이미지 변경
    public void SetItem()
    {
     
        _SlotIamge1.sprite = cInventory.GetInstance.GetWeaponSlot(0)._item._ItemIcon;
        if (cInventory.GetInstance.GetWeaponSlot(1)._item._ItemIcon != null)
        {
           
            _SlotIamge2.sprite = cInventory.GetInstance.GetWeaponSlot(1)._item._ItemIcon;
        }
  
    }
    //2번 슬롯이 null이 아닐때 무기 변경
    public void ChangeSlot()
    {

            Sprite TempSprite = _SlotIamge1.sprite;
            _SlotIamge1.sprite = _SlotIamge2.sprite;
            _SlotIamge2.sprite = TempSprite;
     }
}
