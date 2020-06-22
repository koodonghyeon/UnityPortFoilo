using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//현제 장착중인무기
public class cWeaPon : MonoBehaviour
{
    AudioSource _AttackSound;
    List<AudioClip> _Clip = new List<AudioClip>();

    public Image _Cooltime;
    Text _BulletText;
    //무기 이미지 그리기용 랜더러
    private SpriteRenderer _SpriteRend;
    //현제 아이템
    private Item _NowWeaPon;
    //무기 애니매이션 재생용 애니메이터
    private Animator _Ani;
    //검 애니메이션
    private RuntimeAnimatorController _SwardAni;
    //창 애니매이션
    private RuntimeAnimatorController _SpearAni;
    //총 애니메이션
    private RuntimeAnimatorController _GunAni;
    //공격모션
    private cAttack _AttackMotion;
    //스킬 사용했는지
    private bool _OneSkillCheck =true;
    //공격가능상태인지
    public bool _isAttack=false;
    void Awake()
    {
        _AttackSound = GetComponent<AudioSource>();

        _Clip.Add(Resources.Load<AudioClip>("Sound/swing1"));
        _Clip.Add(Resources.Load<AudioClip>("Sound/swish-1"));
       // _Clip.Add(Resources.Load<AudioClip>("Sound/Reload"));
        _AttackMotion = FindObjectOfType<cAttack>();
          _Ani =transform.GetComponent<Animator>();
          _SpriteRend = transform.GetComponent<SpriteRenderer>();
        _SwardAni =Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Sward");
        _SpearAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Spear/Spear");
        _GunAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Gun/Gun");

        _BulletText = cUIManager.GetInstance.GetWeaPonSlot().transform.GetChild(1).GetChild(1).GetComponent<Text>();
    }
    //현제 무기 세팅(무기장착시 세팅)
    public void SetWeaPon(Item _WeaPon)
    {
        if (_NowWeaPon != null)
        {
                   
                _Cooltime.fillAmount = _WeaPon._CollTimeFillAmount;
            if (_WeaPon._Type == ItemType.OneShot)
            {
                Player.GetInstance._Buff.SetTrigger("BuffOff");
                ((Longrange)_WeaPon).DamDown();
            }   
        }

        _NowWeaPon = _WeaPon;
        if (_NowWeaPon._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
            _SpriteRend.sortingOrder = 4;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
            _AttackSound.clip = _Clip[0];
            _BulletText.text = "";
      
        }
        else if (_NowWeaPon._Type == ItemType.Spear)
        {
            _Ani.runtimeAnimatorController = _SpearAni;
            _SpriteRend.sortingOrder = 10;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
            _AttackSound.clip = _Clip[1];
  
            _BulletText.text = "";
        }
        else if (_NowWeaPon._Type == ItemType.Gun|| _NowWeaPon._Type ==ItemType.OneShot)
        {
            _Ani.runtimeAnimatorController = _GunAni;
            _SpriteRend.sortingOrder = 10;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0,0));
            _AttackSound.clip = null;
            _BulletText.text = (((Longrange)_NowWeaPon)._MaxBullet - ((Longrange)_NowWeaPon)._CurBulletIndex).ToString() + "  /  " + ((Longrange)_NowWeaPon)._MaxBullet.ToString();
  
        }
        _Ani.speed = _NowWeaPon._AttackSpeed;
        _SpriteRend.sprite = _NowWeaPon._ItemIcon;

        Player.GetInstance._MinDamage = 0;
        Player.GetInstance._MaxDamage = 0;


        Player.GetInstance._MinDamage += _NowWeaPon._MinAttackDamage;
        Player.GetInstance._MaxDamage += _NowWeaPon._MaxAttackDamage;
        _isAttack = false;
        _AttackMotion.SetItemMotion(_NowWeaPon);
       
    }

    private void Update()
    {
        if (Player.GetInstance._state != State.Die)
        {
            if (Time.timeScale != 0)
            {
                if (_NowWeaPon._Type != ItemType.Gun)
                {
                 
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!_isAttack)
                        {
                            StartCoroutine("Attackmotion");
                            _isAttack = true;

                        }
                        
                    }
                }
                else if (_NowWeaPon._Type == ItemType.Gun)
                {
                    if (Input.GetMouseButton(0))
                    {
                        _Ani.SetTrigger("AttackCheck");

                    }
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (_NowWeaPon._Skill)
                    {
                        if (_OneSkillCheck)
                        {

                            _NowWeaPon.Skill();
                            StartCoroutine(Cooltime(_NowWeaPon._SkillCoolTime));
                        }
                    }
                }
                if (_NowWeaPon._Type == ItemType.Gun || _NowWeaPon._Type == ItemType.OneShot)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        ((Longrange)_NowWeaPon).Reload();

                    }
                }
            }
        }

    }
 
    //총발사 애니메이션이벤트
    IEnumerator Attack()
    {
 
         yield return new WaitForSeconds(((Longrange)_NowWeaPon)._Delay);
        Vector3 _mousePos = Input.mousePosition; //마우스 좌표 저장
        Vector3 _oPosition = this.transform.position;
        Vector3 target = Camera.main.ScreenToWorldPoint(_mousePos);
        Vector2 dir = (target - _oPosition);
        float rotateDegree = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;

        ((Longrange)_NowWeaPon).FireBulet(_oPosition, rotateDegree);
        _isAttack = false;

    }


    //공격
    IEnumerator Attackmotion()
    {


        if (_NowWeaPon._Type == ItemType.Sword || _NowWeaPon._Type == ItemType.Spear)
        {
               if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1")){
                yield return new WaitForSeconds(0.1f);
                _Ani.SetTrigger("AttackCheck");
                _AttackMotion._Attack();
                _AttackSound.Play();
                
            }

                        
            else if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {

                yield return new WaitForSeconds(0.1f);
                _Ani.SetTrigger("AttackCheck");
                _AttackMotion._Attack();
                _AttackSound.Play();
                


            }
        }
        else if (_NowWeaPon._Type == ItemType.OneShot)
        {
            yield return null;
            _Ani.SetTrigger("AttackCheck");
        }

    }

    //공격가능상태가 됐는지여부
    void AttackReady()
    {
        _isAttack = false;

    }
    //스킬 쿨타임
    IEnumerator Cooltime(float CoolTime)
    {
        _OneSkillCheck = false;
        _Cooltime.color = new Color(0, 0, 0, 0.6f);
        _Cooltime.fillAmount = 1;
            while (_Cooltime.fillAmount > 0)
            {
                _Cooltime.fillAmount -= 1 * Time.deltaTime / CoolTime;
            _NowWeaPon._CollTimeFillAmount = _Cooltime.fillAmount;
                yield return null;

            }
       
        yield return new WaitForFixedUpdate();
   
        _OneSkillCheck = true;
    }
}
