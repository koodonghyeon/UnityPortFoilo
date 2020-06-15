using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kar98 : Longrange
{

    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 5;
        for (int i = 0; i < _MaxBullet; ++i)
        {
           GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/Bullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 40.0f;
            _Bullet._Damage = 0;
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);
            _Bullet._BulletState = BulletState.PlayerSniper;
            _BulletPoll.Add(_Bullet);
        }
        _Delay = 0.2f;
        _ItemID = 9;
        _ItemName = "1898년형 단기병총";//아이템이름
        _ItemDescrIption = "이 나라의 기술력은 세계 제일이지! -피아트-";//아이템설명
        _Type = ItemType.OneShot;//아이템타입
        _MinAttackDamage = 27;//최소데미지
        _MaxAttackDamage = 35;//최대데미지
        _AttackSpeed = 0.87f;//공격속도

        _Quality = ItemQuality.Unique;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Kar98");//아이템 이미지
        _ItemPrice = 3000;//아이템가격
        _SkillText = "다음 한발의 총알의 공격력이 아주강해집니다.";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_DeadlyShot");//아이템 이미지
        _SkillCoolTime = 30;
        _ReloadTime = 2.5f;
     
   
    }


    //총알 발사
    public override void FireBulet(Vector2 Position, float _angle)
    {
        if (!_isReload)
        {
            if (_CurBulletIndex == _MaxBullet)
            {
                return;
            }


            _BulletPoll[_CurBulletIndex].transform.position = Position;

            _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
            if (Player.GetInstance._Buff.GetCurrentAnimatorStateInfo(0).IsName("PowerBuff") && Player.GetInstance._Buff.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
            {
                _ItemSound.clip = _GunSound[3];
                _ItemSound.Play();
                Player.GetInstance._Buff.SetTrigger("BuffOff");
            }
            else
            {
                _ItemSound.clip = _GunSound[0];
                _ItemSound.Play();
            }
            _BulletPoll[_CurBulletIndex]._Start = true;
            _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);
            if (_CurBulletIndex > 0)
            {
                DamDown();
            }
            _BulletUI.text = (_MaxBullet - (_CurBulletIndex + 1)).ToString() + "  /  " + _MaxBullet.ToString();
            StartCoroutine("ActiveBullet", _BulletPoll[_CurBulletIndex]);
            if (_CurBulletIndex >= _MaxBullet - 1)
            {
                Reload();
            }
            else
            {
                _CurBulletIndex++;
            }
        }
    
    }
    public override void DamDown()
    {
       // base.DamDown();
    }
    public override void Reload()
    {
        base.Reload();
    }
    public override void Skill()
    {
   
            StartCoroutine(SkillCourutin());
  
    }
    IEnumerator SkillCourutin()
    {
        Player.GetInstance._Buff.SetTrigger("PowerBuff");
        cBullet Bullet= _BulletPoll[_CurBulletIndex];
        Bullet._Damage += 30;
         yield return new WaitForSeconds(30.0f);

        Bullet._Damage = 0;

        Player.GetInstance._Buff.SetTrigger("BuffOff");
    }

}
