using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Halberd : Shortrange
{
    private void Awake()
    {

        _ItemID = 2;
        _ItemName = "미늘창";//아이템이름
        _ItemDescrIption = "도끼처럼 혹은 창처럼 사용할 수 있는 무기";//아이템설명
        _Type = ItemType.Spear;//아이템타입
        _MinAttackDamage = 14;//최소데미지
        _MaxAttackDamage = 20;//최대데미지
        _AttackSpeed = 1.43f;//공격속도
        _Quality = ItemQuality.Normal;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Halberd");//아이템 이미지
        _ItemPrice = 500;//아이템가격
    
    }
    public override void Attack(cMonsterBase Monster)
    {
        base.Attack(Monster);
    }
  
}
