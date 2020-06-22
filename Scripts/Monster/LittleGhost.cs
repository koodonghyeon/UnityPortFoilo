using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGhost : cShortMonster
{

 

    public float _AttackDelay = 4f; //어택 딜레이
    float _AttackTimer = 0; //어택 타이머
    BoxCollider2D _AttackBox;
    protected override void Awake()
    {
        base.Awake();
        _AttackBox = transform.GetChild(1).GetComponent<BoxCollider2D>();
       
        _MaxHP = 10;
        _currnetHP = 10;
        _Defense = 0;
        _Clip.Add(Resources.Load<AudioClip>("Sound/Ghost"));
        _AttackDamage = 3;
        _MoveSpeed = 0.5f;
    }

   void FixedUpdate()
    {
   
        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("LittleGhostAttack"))
        {
            _MoveSpeed = 3f;
        }
        else
        {
            _MoveSpeed = 1.5f;
        }
        Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);

        _Rigid.velocity = new Vector2(dir.normalized.x * _MoveSpeed, dir.y * _MoveSpeed);

        _AttackTimer += Time.deltaTime;

        if (_AttackTimer > _AttackDelay) //쿨타임이 지났는지
        {
            _AttackBox.enabled = true;
            _Audio.clip = _Clip[2];
            _Audio.Play();
            _Anim.SetTrigger("Attack");

            StartCoroutine(BoxEnabled());
            _AttackTimer = 0; //쿨타임 초기화
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }
        if (_currnetHP < 1)
        {
            if (!_isDie)
            {
                Die(this.gameObject);
                _isDie = true;
            }
        }
    }
    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(0.3f);
        _AttackBox.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_AttackBox.enabled)
            {
                int _dam = _AttackDamage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(_dam);
                _AttackBox.enabled = false;
            }

        }
          
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);

    }
    public override void DropGold()
    {
        for (int i = 0; i <= 3; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex < 60)
            {
                return;
            }
            else if (RandomIndex >= 60 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            }
          
        }
    }
}