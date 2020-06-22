using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadGiantBat : cLongLangeMonster
{ 
    protected override void Awake()
    {
        base.Awake();

      
        _Clip.Add(Resources.Load<AudioClip>("Sound/monster-sound2_bat"));
        _MaxHP = 40;
        _currnetHP = 40;
        _Defense =0;
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

    void AnimationEvent()
    {
        _Audio.clip = _Clip[2];
        _Audio.Play();
        _Dir = (Player.GetInstance.transform.position - this.transform.position);

        StartCoroutine("Attack");
    }
    IEnumerator Attack()
    {
        _Anim.speed = 0;

        for (int i = 0; i < 10; ++i)
        {
            Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / 10), Mathf.Sin(Mathf.PI * 2 * i / 10));
            dirVec += this.transform.position;
            
            float angle = Mathf.Atan2(-_Dir.x, _Dir.y) * Mathf.Rad2Deg;
            yield return new WaitForSeconds(0.1f);
            FireBulet(dirVec, angle,i);

        }

        _Anim.speed = 1;
    }
    void FireBulet(Vector3 Dir, float _angle,int Count)
    {


      cBullet Bullet = cMonsterBullet.GetInstance.GetObject(4);
        Bullet.transform.position = Dir;

        Bullet.transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        Bullet.gameObject.SetActive(true);
        if (Count == 9)
        {
            cMonsterBullet.GetInstance.AllFire(4);
            cMonsterBullet.GetInstance.ActiveBullet(4);
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
