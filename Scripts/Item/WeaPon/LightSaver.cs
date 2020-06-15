using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaver : Shortrange
{
    private void Awake()
    {

        _ItemID = 7;
        _ItemName = "라이트 세이버";//아이템이름
        _ItemDescrIption = "고온의 플라즈마를 칼날삼아 모든 걸 베어버리는 광검";//아이템설명
        _ItemIcon = Resources.Load<Sprite>("Itemp/LightSaber");//
        _Type = ItemType.Sword;//아이템타입
        _MinAttackDamage = 12;//최소데미지
        _MaxAttackDamage = 12;//최대데미지
        _AttackSpeed = 3.28f;//공격속도
        _Quality = ItemQuality.Unique;//아이템등급
        _ItemPrice = 2000;//아이템가격
        _SkillText = "방어력 무시데미지를 넣는다.";
        _SkillCoolTime = 0;

    }
    //방무뎀 때문에 따로해둠
    public override void Attack(cMonsterBase Monster)
    {
        int randomDamage = Random.Range((int)Player.GetInstance._MinDamage, (int)Player.GetInstance._MaxDamage);
        if (Player.GetInstance.isCritical())
        {
            _Dam = randomDamage  + (int)((float)randomDamage * ((float)Player.GetInstance._CriticalDamage / 100.0f))
                + (int)((float)randomDamage * ((float)Player.GetInstance._Power / 100));
            Monster.MonsterHIT(_Dam, true);
        }
        else
        {
            _Dam = randomDamage + (int)((float)randomDamage * ((float)Player.GetInstance._Power / 100));
            Monster.MonsterHIT(_Dam, false);
        }
   
    }

}
