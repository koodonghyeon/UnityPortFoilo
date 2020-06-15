using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAK47: Longrange
{

  

    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 25;
        for (int i = 0; i < _MaxBullet; ++i)
        {
           GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/Bullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 30.0f;
            _Bullet._Damage = 0;
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);
            _Bullet._BulletState = BulletState.Player;
            _BulletPoll.Add(_Bullet);
        }
        _Delay = 0.03f;
        _ItemID = 10;
        _ItemName = "AK-77";//아이템이름
        _ItemDescrIption = "냉탕에 넣고 온탕에 넣어도 아주 튼튼한 믿음직한 총";//아이템설명
        _Type = ItemType.Gun;//아이템타입
        _MinAttackDamage = 5;//최소데미지
        _MaxAttackDamage = 10;//최대데미지
        _AttackSpeed = 10f;//공격속도
        _Quality = ItemQuality.Unique;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Rifle1");//아이템 이미지
        _ItemPrice = 2000;//아이템가격
        _SkillText = "30초간 이동속도 대폭 증가";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_WindForce");//아이템 이미지
        _SkillCoolTime = 45;
        _ReloadTime = 2f;

    }
    //총알 발사
   public override  void FireBulet(Vector2 Position, float _angle)
    {

        base.FireBulet(Position, _angle);
     

    }
    //장전
    public override void Reload()
    {
        base.Reload();
    }
    //스킬
    public override void Skill()
    {

            StartCoroutine(SkillCourutin());

    }
    public override void DamDown()
    {
        // base.DamDown();
    }
    IEnumerator SkillCourutin()
    {
 
        Player.GetInstance._Buff.SetTrigger("MoveBuff");
        Player.GetInstance._MoveSpeed += 6;
        cUIManager.GetInstance.GetStat().SetStat();
        yield return new WaitForSeconds(30.0f);
  
            Player.GetInstance._MoveSpeed -= 6;
            cUIManager.GetInstance.GetStat().SetStat();
            Player.GetInstance._Buff.SetTrigger("BuffOff");


    }

}
