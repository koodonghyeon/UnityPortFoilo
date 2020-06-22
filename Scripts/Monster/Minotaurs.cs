using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaurs : cShortMonster
{
    bool _isDash = false;
    float _Chack = 0f;

    float _AttackDelay = 2f; //런 딜레이
    float _AttackTimer = 0; //런 타이머
    BoxCollider2D _AttackRangeBox;
    BoxCollider2D _AttackBox;
    protected override void Awake()
    {
        base.Awake();
        _AttackBox = transform.GetChild(2).GetComponent<BoxCollider2D>();

        _AttackDamage = 9;
        _MaxHP = 80;
        _currnetHP = 80;
        _Defense = 1;
        _AttackRangeBox = transform.GetChild(3).GetComponent<BoxCollider2D>();
        _MoveSpeed = 8;
    }

  void Update()
    {
 
        _Chack += Time.deltaTime;

        _AttackTimer += Time.deltaTime;
        if (_AttackTimer > _AttackDelay && _isDash == false) //쿨타임이 지났는지
        {
            _Anim.SetTrigger("Attack");
            _AttackTimer = 0; //쿨타임 초기화
        }


        if (_Rigid.velocity == Vector2.zero)
        {
            _Anim.SetBool("Run", false);
        }

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("MinotaursCharge"))
        {
            _isDash = true;
        }
        else
        {
            _isDash = false;
        }

        if (!_isDash)
        {
            if (Player.GetInstance.transform.position.x < this.transform.position.x)
            {
                _Renderer.flipX = true;
            }
            if (Player.GetInstance.transform.position.x > this.transform.position.x)
            {
                _Renderer.flipX = false;
            }
        }
        if (_currnetHP < 1)
        {
            if (!_isDie)
            {
                Die(this.gameObject);
                _isDie = true;
            }
        }
        if (_Chack >= 8)
        {
            _AttackRangeBox.enabled = true;
        }
    }
    public void Dash()
    {
        if (_Chack >= 8f)
        {

            _AttackRangeBox.enabled = false;
            _AttackBox.enabled = true;
            StartCoroutine(BoxEnabled());
            _Anim.SetBool("Run", true);
            _Dir = (Player.GetInstance.transform.position - this.transform.position);
            float dashSpeed = 20f;
            if (Player.GetInstance.transform.position.x < this.transform.position.x)
            {
                dashSpeed *= -1;
            }
            _Rigid.velocity = new Vector2((_Dir.normalized.x * _MoveSpeed) + dashSpeed, 0);

            _Chack = 0;
        }
    }
    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(1f);
        _AttackBox.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_AttackBox.enabled)
            {
                if (_AttackRangeBox.enabled == false)
                {
                    int _dam = _AttackDamage - Player.GetInstance._Defense;
                    Player.GetInstance.HIT(_dam);
                    _AttackBox.enabled = false;
                }
            }


        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //공격판정 스타트용
        if (other.gameObject.CompareTag("Player"))
        {
            Dash();
        }
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);
    
    }
    public override void DropGold()
    {
        for (int i = 0; i <= 10; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
          if(RandomIndex < 30)
            {
                return;
            }
            else if (RandomIndex >= 30 && RandomIndex <= 70)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            }
            else if (RandomIndex >= 70 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                obj.transform.position = this.transform.position;
                _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            }
        }
    }
    void SetBoxTrue()
    {
        _AttackBox.enabled = true;
    }
    void SetBoxfalse()
    {
        _AttackBox.enabled = false;
    }
}
