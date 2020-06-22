using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBat : cLongLangeMonster
{
   

   protected override void Awake()
    {
        base.Awake();

      
        _Clip.Add(Resources.Load<AudioClip>("Sound/monster-sound2_bat"));
        _MaxHP = 55;
        _currnetHP = 55;
        _Defense = 0;
        _ShootDelay = 4f;
        _ShootTimer = 0;
}


     void Update()
    {
        _ShootTimer += Time.deltaTime;

        if (_ShootTimer > _ShootDelay) //쿨타임이 지났는지
        {
            _Anim.SetTrigger("Fire");

            _ShootTimer = 0; //쿨타임 초기화
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

    public void AnimationEvent()
    {
     
        _Audio.clip = _Clip[2];
        _Audio.Play();
        _Dir = (Player.GetInstance.transform.position - this.transform.position);
        for (float i = 0f; i <= 0.8f; i += 0.4f)
        {
            Invoke("Attack", i);
        }
    }

    public void Attack()
    {
        for (int i = -1; i < 2; ++i)
        {
          
            float angle = Mathf.Atan2(-_Dir.x, _Dir.y) * Mathf.Rad2Deg;
            angle += 25 * i;
            FireBulet(this.transform.position, angle);
        }
    }
    public void FireBulet(Vector3 _Dir, float _angle)
    {

        cBullet Bullet = cMonsterBullet.GetInstance.GetObject(4);
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
        for (int i = 0; i <= 5; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex < 35)
            {
                return;
            }
            else if (RandomIndex >= 35 && RandomIndex <= 80)
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
