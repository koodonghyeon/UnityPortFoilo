using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//아이템설명판넬
public class cItemPanel : MonoBehaviour
{
    //아이템 이름
    public Text   _ItemName;
    //아이템 공격력
    public Text   _Damage;
    //아이템 공격속도
    public Text   _Speed;
       //아이템 설명   
    public Text   _ItemDescrlption;
    //아이템 타입
    public Text   _ItemType;
    //아이템 등급
    public Text   _ItemQuality;
    //아이템 이미지
    public Image _Icon;
    //스킬 설명
    public Text _SkillText;
    //스킬아이콘
    public Image _SkillIcon;
    //스킬설명창
    public GameObject _SkillDescrlption;
    //판넬 세팅
    public void SetPanel
        (string ItemName, float MinDamage, float MaxDamage, float Speed, string ItemDescrlption, string ItemType, string ItemQuality, Sprite Icon,string SkillText,Sprite Skillicon)
    {
        //아이템 등급에 따라 이름색변경
        if (ItemQuality == "전설 아이템")
        {
            _ItemName.text = "<color=#FF00B2>" + ItemName + "</color>";
        }
        else if (ItemQuality== "희귀 아이템")
        {
            _ItemName.text = "<color=#3232FF>" + ItemName+"</color>";
        }
        else if (ItemQuality=="일반 아이템")
        {
            _ItemName.text = "<color=#FFFFFF>" + ItemName+"</color>";
        }
        //아이템 세부 세팅
        _Damage.text = "<color=#ffffff>"+"공격력 : "+ "</color>" + "<color=#ff0000>"+MinDamage +" ~ "+ MaxDamage + "</color>";
        _Speed.text = "<color=#ffffff>" + "공격속도 : " + "</color>" + "<color=#ff0000>" + Speed+ "</color>";
        _ItemDescrlption.text ="<color=#46BEFF>"+ItemDescrlption+"</color>";
        _ItemType.text = "<color=#8c8c8c>" + ItemType+"</color>";
        _ItemQuality.text = "<color=#8c8c8c>" + ItemQuality+"</color>";
        _Icon.sprite = Icon;
        if (Skillicon == null)
        {
            _SkillDescrlption.SetActive(false);
        }
        else if (Skillicon != null)
        {
          
            _SkillText.text = "<color=#FFFFFF>" + SkillText + "</color>";
            _SkillIcon.sprite = Skillicon;
        }
    }
}