using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//음식슬롯
public class cFoodSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler
{
    //우클릭체크용
    public PointerEventData.InputButton _MouseBtn = PointerEventData.InputButton.Right;
    //음식
    public cFood _Food;
    //음식이름
    Text _FoodName;
    //음식 효과1
    Text _FoodEffect1;
    //음식 효과2
    Text _FoodEffect2;
    
    //체력 회복량
    Text _HP;
    //포만감
    Text _Satiety;
    //음식 가격
    Text _FoodPrice;
    //HP이미지
    Image _HPImage;
    //포만감이미지
    Image _SatietyIamge;
    //가격이미지
    Image _PriceImage;
    //백그라운드이미지
    Image _BackGrond;
    //음식이 팔렸나 여부
    bool _isFood=false;
    //
    AudioSource _EatAudio;
   List<AudioClip> _EatClip=new List<AudioClip>();
    //cFoodSlotList _FoodSlotList;
    public delegate void NowFood(PointerEventData eventData);
    public NowFood _NowFood;
    void Awake()
    {

        _BackGrond      = transform.GetChild(0).GetComponent<Image>();
        _FoodName       = transform.GetChild(1).GetComponent<Text>();
        _FoodEffect1    = transform.GetChild(2).GetComponent<Text>();
        _FoodEffect2    = transform.GetChild(3).GetComponent<Text>();
        _HP             = transform.GetChild(4).GetComponent<Text>();
        _Satiety        = transform.GetChild(5).GetComponent<Text>();
        _FoodPrice      = transform.GetChild(6).GetComponent<Text>();
        _HPImage        = transform.GetChild(7).GetComponent<Image>();
        _SatietyIamge   = transform.GetChild(8).GetComponent<Image>();
        _PriceImage     =transform.GetChild(9).GetComponent<Image>();
        _EatAudio = GetComponent<AudioSource>();
        _EatClip.Add(Resources.Load<AudioClip>("Sound/FoodEat"));
        _EatClip.Add(Resources.Load<AudioClip>("Sound/FoodEat2"));
    }
    
    //음식 세팅
    public void SetFood()
    {

        _FoodName.text = _Food._FoodName.ToString();
        
            _FoodEffect1.text = "<color=#70F170>" + _Food._FoodEffect1 + "</color>" + " " + _Food._FoodStat1;
        if (_Food._FoodEffect2 == " ")
        {
            _FoodEffect2.text = null;
        }
        else
        {
            _FoodEffect2.text = "<color=#70F170>" + _Food._FoodEffect2 + "</color>" + " " + _Food._FoodStat2;
        }
        _HP.text = _Food._HP.ToString();
        _Satiety.text = _Food._Satiety.ToString();
        _FoodPrice.text = _Food._FoodPrice.ToString();
    }
    //음식팔리면 슬롯 클리어
    public void ClearSlot()
    {
        _BackGrond.sprite = Resources.Load<Sprite>("UI/food/ThankYou.korean");
        _FoodName.text     = null;
        _FoodEffect1.text      = null;
        _FoodEffect2.text      = null;
        _HP.text               = null;
        _Satiety.text          = null;
        _FoodPrice.text        = null;
        _HPImage.color = new Color(1, 1, 1, 0);
        _SatietyIamge.color = new Color(1, 1, 1, 0);
        _PriceImage.color = new Color(1, 1, 1, 0);
        transform.parent.GetComponent<cFoodSlotList>().DeleteNowFood();
        _isFood = true;
    }
    //우클릭시 아이템 판매
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == _MouseBtn)
        {
            if (cGameManager.GetInstance.Gold > this._Food._FoodPrice && 
                Player.GetInstance._Food._MyCurrentValue + this._Food._Satiety < Player.GetInstance._Food.MaxValue)
            {
                cGameManager.GetInstance.Gold -= this._Food._FoodPrice;
                Player.GetInstance._health.HealHP(_Food._HP, false);

                switch (_Food._FoodID)
                {
                    case 1:
                    {
                            Player.GetInstance._Power += (int)_Food._FoodStat1;
                            Player.GetInstance._health.HealHP((int)_Food._FoodStat2, true);
                            _EatAudio.clip = _EatClip[0];
   
                            break;
                    }
                    case 2:
                    {
                            Player.GetInstance._Power += (int)_Food._FoodStat1;
                            Player.GetInstance._Defense += (int)_Food._FoodStat2;
                            _EatAudio.clip = _EatClip[1];

                            break;
                    }
                    case 3:
                    {
                            Player.GetInstance._Power += (int)_Food._FoodStat1;
                            _EatAudio.clip = _EatClip[0];

                            break;
                    }
                    case 4:
                    {
                            Player.GetInstance._health.HealHP((int)_Food._FoodStat1, true);
                            Player.GetInstance._Defense += (int)_Food._FoodStat2;
                            _EatAudio.clip = _EatClip[1];

                            break;
                    }
                    case 5:
                    {
                            Player.GetInstance._Defense += (int)_Food._FoodStat1;
                            _EatAudio.clip = _EatClip[0];

                            break;
                    }
                    case 6:
                    {
                            Player.GetInstance._health.HealHP((int)_Food._FoodStat1, true);
                            _EatAudio.clip = _EatClip[1];

                            break;
                    }
                    case 7:
                    {
                            Player.GetInstance._CriticalDamage += (int)_Food._FoodStat1;
                            _EatAudio.clip = _EatClip[0];

                            break;
                    }
                    case 8:
                    {

                            Player.GetInstance._CriticalDamage += (int)_Food._FoodStat1;
                            Player.GetInstance._Power += (int)_Food._FoodStat1;
                            _EatAudio.clip = _EatClip[1];

                            break;
                    }
                    case 9:
                    {
                            Player.GetInstance._Food._MyCurrentValue -= 30;
                        break;
                    }
                }
              
                Player.GetInstance._Food._MyCurrentValue += _Food._Satiety;
                ClearSlot();
                cUIManager.GetInstance.GetStat().SetStat();
                cGameManager.GetInstance._DeleGateGold();

            }
        }
    }
    //마우스가 위로 올라와있을시 현제 음식 이미지변경
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_isFood)
        transform.parent.GetComponent<cFoodSlotList>().SettingNowFood(this);
    }


}
