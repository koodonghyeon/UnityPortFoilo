using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//누구의총알인가
public enum BulletState
{
    Player,
    PlayerSniper,
    Monster,
    Boss
}
public class cBullet : MonoBehaviour
{

    //공격력
    public int _Damage;
    //속도
    public float _Speed;

    Animator _Anim;
   public Transform _ChildBullet;
    //이총알이 플레이어용인지 몬스터용인지
    public BulletState _BulletState;
    public bool _Start = true;

    private void Awake()
    {
        if (transform.childCount == 0)
        {
            _ChildBullet = null;
            _Anim = GetComponent<Animator>();

        }
        else if (transform.childCount >= 1)
        {
            _ChildBullet = transform.GetChild(0);
            _Anim = transform.GetChild(0).GetComponent<Animator>();
        }
    }
    void Update()
    {
        if (_Start)
        {
            transform.position += transform.up * _Speed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        //벽이나 땅에 충돌하면 총알 없어짐
        if (_BulletState != BulletState.Boss)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("floor"))
            {
                _Anim.SetTrigger("Fire");
                _Start = false;
            }
        }
        //자신이 플레이어불릿이고 몬스터랑충돌시 여기로
        if (_BulletState == BulletState.Player||_BulletState ==BulletState.PlayerSniper)
        {
            if (collision.CompareTag("MonsterHitBox"))
            {
                cMonsterBase Monster;
                Monster = collision.GetComponent<cMonsterBase>();
               int RandomDamage = _Damage+Random.Range((int)Player.GetInstance._MinDamage, (int)Player.GetInstance._MaxDamage + 1);
   
                if (Player.GetInstance.isCritical())
                {
                    _Damage = (RandomDamage - Monster._Defense) + (int)((float)RandomDamage * ((float)Player.GetInstance._CriticalDamage / 100.0f))
                        + (int)((float)RandomDamage * ((float)Player.GetInstance._Power / 100));
                    if (_Damage < 1)
                    {
                        _Damage = 1;
                    }
                    Monster.MonsterHIT(_Damage, true);
                }
                else
                {
                    _Damage = (RandomDamage - Monster._Defense) + (int)((float)RandomDamage * ((float)Player.GetInstance._Power / 100));
                    if (_Damage < 1)
                    {
                        _Damage = 1;
                    }
                    Monster.MonsterHIT(_Damage, false);
                }
                _Anim.SetTrigger("Fire");

                this._Damage = 0;
                if(_BulletState!=BulletState.PlayerSniper)
                    _Start = false;
              
            }
            
        }
        //자신이 몬스터 불릿이면 여기로
        else if (_BulletState == BulletState.Monster)
        {
            if (collision.CompareTag("Player"))
            {
                int dam = _Damage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(dam);
                _Anim.SetTrigger("Fire");
                _Start = false;

           
            }

        }
        //자신이 보스불릿이면 여기로
       else if (_BulletState == BulletState.Boss)
        {
            if (collision.CompareTag("Player"))
            {
                int dam = _Damage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(dam);
                _Anim.SetTrigger("Fire");
             

            }
        }

       
    }


    //충돌시 총알없어짐
    public void CrashBullet()
    {
        this.gameObject.SetActive(false);
    }
}
  