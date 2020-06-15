using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banshee : cLongLangeMonster
{
  

   protected override void Awake()
    {
        base.Awake();
       
        _MaxHP = 60;
        _currnetHP = 60;
        _Defense = 1;
        _Clip.Add(Resources.Load<AudioClip>("Sound/high_pitch_scream_gverb"));
        _ShootDelay = 4;
        _ShootTimer = 0;
    }

    // Update is called once per frame
    private void Update()
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
        _Audio.clip= _Clip[2];
        _Audio.Play();
        for (int i = 0; i < 12; ++i)
        {

            Vector3 dirVec = this.transform.position;
            float angle = 30 * i;
            FireBulet(dirVec,angle);
        }
    }
    public void FireBulet(Vector3 Dir, float _angle)
    {

        cBullet Bullet = cMonsterBullet.GetInstance.GetObject(0);
        Bullet.transform.position = this.transform.position;
        Bullet.transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        Bullet._Start = true;
        Bullet._ChildBullet.rotation = Quaternion.Euler(0f, 0f, 0);
        Bullet.gameObject.SetActive(true);


        StartCoroutine(ActiveBullet(Bullet));
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
            if (RandomIndex >= 40 && RandomIndex <= 90)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                 _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            }
            else if (RandomIndex >= 90 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                obj.transform.position = this.transform.position;
                _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
                    
            }
        }
    }

}