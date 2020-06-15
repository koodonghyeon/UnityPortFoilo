using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//실질적인 공격용스크립트(공격범위및박스콜라이더)
public class cAttack : MonoBehaviour
{
    //애니메이터
  public  Animator _Ani;
    //검애니
    RuntimeAnimatorController _SwardAni;
    //창애니
    RuntimeAnimatorController _SpearAni;
    //현제 아이템
    Item _Nowitem;
    //무기 공격범위 박스
    BoxCollider2D _HitBox;
    //공격용 코루틴
    public delegate void _AttackStart();
    public _AttackStart _Attack;

    void Awake()
    {
        _Attack += Attack;
      
        _Ani = GetComponent<Animator>();
        _HitBox = GetComponent<BoxCollider2D>();
        _SwardAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Swing");
       _SpearAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Spear/SpearAttack");
    }
    //무기타입별로 공격모션변경
    public void SetItemMotion(Item item)
    {
        _Nowitem = item;
        if (item._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
            _Attack += cCameramanager.GetInstance.VibrateForTime;
        }

        else if (item._Type == ItemType.Spear)
        {
            _Ani.runtimeAnimatorController = _SpearAni;
            _Attack += cCameramanager.GetInstance.VibrateForTime;
        }
        else if (item._Type == ItemType.Gun||item._Type ==ItemType.OneShot)
        {
            _Ani.runtimeAnimatorController = null;
            _Attack -= cCameramanager.GetInstance.VibrateForTime;
            _HitBox.enabled = false;
        }
        _Ani.speed = item._AttackSpeed;

    }
    //공격애니메이션재생

    private void Attack()
    {

        _Ani.SetTrigger("AttackCheck");
    }
    //몬스터 충돌체크
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("MonsterHitBox"))
        {
            ((Shortrange)_Nowitem).Attack(collision.GetComponent<cMonsterBase>());

        }
    }

}
