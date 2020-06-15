using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Gwendolyn : Shortrange
{
    private void Awake()
    {

        _ItemID = 8;
        _ItemName = "그웬돌린";//아이템이름
        _ItemDescrIption = "마력을 흡수하는 푸른 돌을 깍아 만든 창";//아이템설명
        _Type = ItemType.Spear;//아이템타입
        _MinAttackDamage = 16;//최소데미지
        _MaxAttackDamage = 22;//최대데미지
        _AttackSpeed = 2f;//공격속도
        _Quality = ItemQuality.Unique;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Gwendolyn");//아이템 이미지
        _ItemPrice = 2000;//아이템가격
        _SkillText = "30초간 크리티컬 데미지 증가";
        _SkillIcon = Resources.Load<Sprite>("UI/Stat/CritcalDamage");
        _SkillCoolTime = 40;
    }
    public override void Attack(cMonsterBase Monster)
    {
        base.Attack(Monster);
 

    }
    public override void Skill()
    {
 
            StartCoroutine(SkillCorutin());

    }
    IEnumerator SkillCorutin()
    {
     
        Player.GetInstance._Buff.SetTrigger("CriticalBuff");
        Player.GetInstance._CriticalDamage += 30;
        cUIManager.GetInstance.GetStat().SetStat();
        yield return new WaitForSeconds(30.0f);
    
            Player.GetInstance._CriticalDamage -= 30;
            cUIManager.GetInstance.GetStat().SetStat();

            Player.GetInstance._Buff.SetTrigger("BuffOff");
  
    }

}
