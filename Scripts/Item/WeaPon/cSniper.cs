using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSniper : Longrange
{
    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 3;
        for (int i = 0; i < _MaxBullet; ++i)
        {
           GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/Bullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 100.0f;
            _Bullet._Damage = 0;
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);
            _Bullet._BulletState = BulletState.PlayerSniper;
            _BulletPoll.Add(_Bullet);
        }
        _Delay = 1;
        _ItemID = 3;
        _ItemName = "화승총";//아이템이름
        _ItemDescrIption = "비가 오면 사용할수 없는총";//아이템설명
        _Type = ItemType.OneShot;//아이템타입
        _MinAttackDamage = 30;//최소데미지
        _MaxAttackDamage = 41;//최대데미지
        _AttackSpeed = 1f;//공격속도

        _Quality = ItemQuality.Normal;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/MatchlockGun");//아이템 이미지
        _ItemPrice = 700;//아이템가격
        _ReloadTime = 3f;
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
        if (!_isReload)
        {
            if (_CurBulletIndex == _MaxBullet)
            {
                return;
            }


            _BulletPoll[_CurBulletIndex].transform.position = Position;

            _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
            _ItemSound.clip = _GunSound[4];
            _ItemSound.Play();
            _BulletPoll[_CurBulletIndex]._Start = true;
            _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);
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

}
