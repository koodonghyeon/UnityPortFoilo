using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSkel : cShortMonster
{



    float _Chack = 0f;
    BoxCollider2D _AttackBox;

    protected override void Awake()
    {
        base.Awake();
        _AttackBox = transform.GetChild(2).GetComponent<BoxCollider2D>();
        _Anim = GetComponent<Animator>();

        _Clip.Add(Resources.Load<AudioClip>("Sound/swing3")); 
        _Anim.SetBool("Run", true);
        _MaxHP = 75;
        _currnetHP = 75;
        _Defense = 1;
        _AttackDamage = 7;
        _MoveSpeed = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        _Chack += Time.deltaTime;
        _Dir = (Player.GetInstance.transform.position - this.transform.position);

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("BigWhiteSkelMove"))
        {
            _Rigid.velocity = new Vector2(_Dir.normalized.x * _MoveSpeed, 0);
        }
        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("BigWhiteSkelAttack"))
        {
            _Rigid.velocity = Vector2.zero;
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _HPBarBackGround.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            transform.rotation = Quaternion.identity;
            _HPBarBackGround.transform.rotation = Quaternion.identity;
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
    void Attack()
    {
        if (_Chack >= 4f)
        {
            _AttackBox.enabled = true;
            StartCoroutine(BoxEnabled());
                _Anim.SetTrigger("Attack");
            _Chack = 0;
        }
    }
    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(0.5f);
        _AttackBox.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_AttackBox.enabled)
            {
                _Audio.clip=_Clip[2];
                _Audio.Play();
        
                int _dam = _AttackDamage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(_dam);
                _AttackBox.enabled = false;
            }


        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Attack();
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
            if (RandomIndex >= 30 && RandomIndex <= 80)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            }
            else if (RandomIndex >= 80 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                obj.transform.position = this.transform.position;
                _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            }
        }
    }
}
