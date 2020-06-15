using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class cShortSward : Shortrange
{
    private void Awake()
    {

        _ItemID = 1;
        _ItemName = "숏소드";//아이템이름
        _ItemDescrIption = "가볍고 휘두르기 편한 검";//아이템설명
        _Type = ItemType.Sword;//아이템타입
        _MinAttackDamage = 8;//최소데미지
        _MaxAttackDamage = 10;//최대데미지
        _AttackSpeed = 3.03f;//공격속도
        _Quality = ItemQuality.Normal;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/BasicShortSword_New");//아이템 이미지
        _ItemPrice = 500;//아이템가격
 
    }
    public override void Attack(cMonsterBase Monster)
    {
        base.Attack(Monster);

    }
  

}
