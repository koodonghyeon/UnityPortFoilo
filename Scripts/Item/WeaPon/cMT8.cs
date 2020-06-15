using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMT8 : Longrange
{

    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 30;
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
        _ItemID = 6;
        _ItemName = "MT8 카빈";//아이템이름
        _ItemDescrIption = "가볍고 연사가 빠른 돌격소총";//아이템설명
        _Type = ItemType.Gun;//아이템타입
        _MinAttackDamage = 3;//최소데미지
        _MaxAttackDamage = 5;//최대데미지
        _AttackSpeed = 10.53f;//공격속도
        _Quality = ItemQuality.Rare;//아이템등급    

        _ItemIcon = Resources.Load<Sprite>("Itemp/Rifle0");//아이템 이미지
        _ItemPrice = 1500;//아이템가격
        _ReloadTime = 2.1f;
    }
    public override void DamDown()
    {
        // base.DamDown();
    }
    public override void Reload()
    {
        base.Reload();
    }

    //총알 발사
    public override  void FireBulet(Vector2 Position, float _angle)
    {
        base.FireBulet(Position, _angle);
 
    }


}
