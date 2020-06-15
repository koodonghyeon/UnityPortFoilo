using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//음식슬롯관리용 
public class cFoodSlotList : MonoBehaviour
{
    //음식슬롯 프리팹
    public Transform _Slot;
    //현재음식
    public Image _NowFood;
    //프리팹 세팅용 Y좌표
    private float Y=-100;
    //슬롯 리스트
    private List<cFoodSlot> _FoodSlotList = new List<cFoodSlot>();

    void Awake()
    {

        _NowFood = GameObject.Find("NowFood").GetComponent<Image>();
        for (int i = 0; i < 3; i++)
            {
                Transform newSlot = Instantiate(_Slot);

                newSlot.name = "Slot" + (i + 1);
                newSlot.SetParent(transform);
                newSlot.localPosition = new Vector3(25f, Y, 0);
                _FoodSlotList.Add(newSlot.GetComponent<cFoodSlot>());
            Y -= 217;

        }

        _NowFood.transform.localScale = new Vector3(5, 5, 0);
    }
    private void Start()
    {
        SetFood();
       _NowFood.sprite = _FoodSlotList[0]._Food._FoodIcon;
    }

    //아이템 셋팅
    private void SetFood()
    {

        int[] num = new int[3];

        num[0] = Random.Range(0, 6);

        _FoodSlotList[0]._Food = cDataBaseManager.GetInstance._FoodList[num[0]];
        _FoodSlotList[0].SetFood();

        for (int i = 1; i < _FoodSlotList.Count; i++)
        {

            num[i] = Random.Range(0, 6);

            if (num[i] == num[i - 1] || num[i] == num[0])
            {
                i--;
            }
            else
            {
                _FoodSlotList[i]._Food = cDataBaseManager.GetInstance._FoodList[num[i]];
                _FoodSlotList[i].SetFood();
            }

        }
    }

    //현재음식 세팅
    public void SettingNowFood(cFoodSlot Slot)
    {
        _NowFood.sprite = Slot._Food._FoodIcon;
    }
    //음식팔렸을때 현재음식 삭제
    public void DeleteNowFood()
    {
        _NowFood.sprite = null;
    }
}
