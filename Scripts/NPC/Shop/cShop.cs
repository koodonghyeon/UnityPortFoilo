using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//상점
public class cShop : MonoBehaviour,IPointerEnterHandler
{

    //프리팹
    public Transform _SlotPrefeb;
    //슬롯포지션
    private float _SlotYPosition = -174;
    //슬롯리스트
    private List<cShopSLot> _ShopSlotList = new List<cShopSLot>();
    void Awake()
    {
     //상점 슬롯세팅  
        for (int i = 0; i < 3; ++i)
        {
            Transform newSlot = Instantiate(_SlotPrefeb);

            newSlot.name = "Slot" + (i + 1);
            newSlot.SetParent(transform);
            newSlot.localPosition = new Vector3(0, _SlotYPosition, 0);
            _ShopSlotList.Add(newSlot.GetComponent<cShopSLot>());
            newSlot.GetComponent<cShopSLot>()._SlotNum = i;
            _SlotYPosition += -180;


        }


    }
    private void Start()
    {
        for (int i = 0; i < _ShopSlotList.Count; ++i)
        {
            _ShopSlotList[i]._SlotSetting += OnPointerEnter;
        }
        //슬롯에 아이템 세팅
        SetItem();

    }

    //아이템 셋팅
    private void SetItem()
    {

        int[] num = new int[3];

        num[0] = Random.Range(0, 6);

        _ShopSlotList[0]._item = cDataBaseManager.GetInstance._ItemList[num[0]];
        _ShopSlotList[0].SetItem();

        for (int i = 1; i < _ShopSlotList.Count; i++)
        {
            // int Number = 0;

            num[i] = Random.Range(0, 6);

            if (num[i] == num[i - 1] || num[i] == num[0])
            {
                i--;
            }
            else
            {
                _ShopSlotList[i]._item = cDataBaseManager.GetInstance._ItemList[num[i]];
                _ShopSlotList[i].SetItem();
            }

        }
    }
    //리스트에서 삭제
    public void ReMove(cShopSLot Slot)
    {
        _ShopSlotList.Remove(Slot);
       
    }
    //아이템 삭제후 다시 정렬
    public void Setting()
    {
        float Y = -174.0f;
        for (int i = 0; i < _ShopSlotList.Count; ++i) {
            _ShopSlotList[i].transform.localPosition = new Vector3(0, Y, 0);
            Y += -180;
                }
    }

    //슬롯 델리게이트에 추가할용도
    public void OnPointerEnter(PointerEventData eventData)
    {
      for(int i =0; i< _ShopSlotList.Count; ++i)
        {
            if (_ShopSlotList[i]._Check)
            {
                _ShopSlotList.Remove(_ShopSlotList[i]);
              
            }
            Setting();
        }
    }
}
