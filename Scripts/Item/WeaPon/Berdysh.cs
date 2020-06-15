using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Berdysh : Shortrange
{
    private void Awake()
    {

        _ItemID = 5;
        _ItemName = "버디슈";//아이템이름
        _ItemDescrIption = "초승달 모양의 날이 선 무기";//아이템설명
        _Type = ItemType.Spear;//아이템타입
        _MinAttackDamage = 18;//최소데미지
        _MaxAttackDamage = 22;//최대데미지
        _AttackSpeed = 0.95f;//공격속도
        _Quality = ItemQuality.Rare;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Berdysh");//아이템 이미지
        _ItemPrice = 1000;//아이템가격
 
    }
    public override void Attack(cMonsterBase Monster)
    {
        base.Attack(Monster);

    }
   

}
