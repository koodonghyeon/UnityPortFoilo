using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBat : cLongLangeMonster
{ 
    public int _moveRangeX;
    public int _moveRangeY;


    protected override void Awake()
    {
        base.Awake();

        _MaxHP = 20;
        _Clip.Add(Resources.Load<AudioClip>("Sound/Bat2"));
        _currnetHP = 20;
        _Defense = 0;
        Invoke("MoveRange", 1);
        _ShootDelay = 4f;
        _ShootTimer = 0;
    }


    void FixedUpdate()
    {

        _ShootTimer += Time.deltaTime;
        
        if (_ShootTimer > _ShootDelay) //쿨타임이 지났는지
        {
            _Anim.SetTrigger("Fire");
            _ShootTimer = 0; //쿨타임 초기화
        }
        

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("Bat_Rad"))
        {
            _Rigid.velocity = new Vector2(_moveRangeX, _moveRangeY);
        }
        else if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("Bat_RadAttack"))
        {
            _Rigid.velocity = Vector2.zero;
        }

        if(Player.GetInstance.transform.position.x < this.transform.position.x)
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "floor" || other.gameObject.tag == "BaseLine")
        {
            _moveRangeX *= -1;
            _moveRangeY *= -1;
            CancelInvoke();
            Invoke("MoveRange", 0.1f);
        }
    }

    //재귀 함수
    void MoveRange()
    {
        _moveRangeX = Random.Range(-2, 3);
        _moveRangeY = Random.Range(-2, 3);

        float naxtMoveRange = Random.Range(2f, 4f);
        Invoke("MoveRange", naxtMoveRange);
    }

    public void AnimationEvent()
    {
        _Audio.clip = _Clip[2];
        _Audio.Play();
        Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            FireBulet( angle);
    }
    public  void FireBulet( float _angle)
    {
        cBullet Bullet = cMonsterBullet.GetInstance.GetObject(1);
        Bullet.transform.position = this.transform.position;

        Bullet.transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        Bullet._Start = true;
        Bullet.gameObject.SetActive(true);
        StartCoroutine("ActiveBullet", Bullet);
     
    }
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
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
            if (RandomIndex >= 55 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            }
         
        }
    }
}
