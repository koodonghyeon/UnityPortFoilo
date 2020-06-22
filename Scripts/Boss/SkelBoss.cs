using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//보스 메인
public class SkelBoss : cBossMonster
{
    enum State
    {
        Normal,
        Bullet,
        Sword,
        Laser
    }
    
    public Transform _BossBack;
    int _SwordX = 0;
    bool _isAttack=false;
    int _RandomIndex;
    State state = State.Normal;

    public SkellBossLaser[] skellBossLasers =new SkellBossLaser[2];
    //인트로가 끝났나여부
    bool _isIntro=false;

    protected override void Awake()
    {
        base.Awake();

        _MaxBossSword = 5;
        for (int i = 0; i < _MaxBossSword; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Boss/BossSword")) as GameObject;
            cBossSword _Sword = obj.GetComponent<cBossSword>();
            _Sword._Speed = 20.0f;
  
            _Sword.transform.SetParent(transform);
            _Sword.gameObject.SetActive(false);

            _BossSwordPoll.Add(_Sword);
        }
        skellBossLasers[0] = transform.GetChild(2).GetComponent<SkellBossLaser>();
        skellBossLasers[1] = transform.GetChild(3).GetComponent<SkellBossLaser>();
        skellBossLasers[0]._SkellLaser = skellBossLasers[1];
        skellBossLasers[0]._Count = 0;
        skellBossLasers[1]._SkellLaser = skellBossLasers[0];
        skellBossLasers[1]._Count = 0;
        _Clip.Add(Resources.Load<AudioClip>("Sound/beliallaugh_rev"));
        _MaxHP = 400;
        _currnetHP = 400;
    }
    //인트로 코루틴시작
    private void Start()
    {
        StartCoroutine(SkellBossIntro());
 
    }


    private void FixedUpdate()
    {
        if (_isIntro)
        {
            if (_currnetHP <= 0)
            {
                if (!_isDie)
                {
                    _isDie = true;
                    _Anim.SetTrigger("Die");
                    Die(this.gameObject);
                }
            }

            if (!_isAttack)
                {
                  
                    if (state == State.Laser)
                    {
                        _RandomIndex = Random.Range(0, 2);
                        skellBossLasers[_RandomIndex]._Fire = true;
                        _isAttack = true;

                    }
                    else if (state == State.Bullet)
                    {
                        _Anim.SetTrigger("Attack");
                        _isAttack = true;

                    }
                    else if (state == State.Sword)
                    {
                      
                        Vector3 dirVec = new Vector3(_BossBack.transform.position.x + -4, _BossBack.transform.position.y + 5, 0);
                        StartCoroutine(FireSword(dirVec));
                        _isAttack = true;

                    }
                }

            if (!_isDie)
            {
                if (skellBossLasers[0]._Count > 3 || skellBossLasers[1]._Count > 3)
                {
                   
                        if (state != State.Normal)
                        {
                            StartCoroutine("SkellBossState");
                            skellBossLasers[0]._Count = 0;
                            skellBossLasers[1]._Count = 0;
                        }
                        state = State.Normal;
                }
            }

               
    
        }
    }
    //보스인트로 재생끝나면 보스패턴시작
    IEnumerator SkellBossIntro()
    {
        cCameramanager.GetInstance.SetTarget(this.gameObject,1.0f);
        _Audio.clip = _Clip[2];
        _Audio.Play();
        Player.GetInstance._isMoveMap = true;
        yield return new WaitForSeconds(5.0f);
        cCameramanager.GetInstance.SetTarget(Player.GetInstance.gameObject,5.0f);
        _HPBarBackGround.gameObject.SetActive(true);
        _isIntro = true;
        Player.GetInstance._isMoveMap = false;
        _isAttack = true;
        StartCoroutine("SkellBossState");

    }
    //보스패턴 코루틴
    IEnumerator SkellBossState()
    {
        yield return new WaitForSeconds(2.0f);
        int randomNum = Random.Range(0, 3);
        if (_isAttack)
        {
        


            if (randomNum == 0)
            {
                state = State.Bullet;
            }
            else if (randomNum == 1)
            {
                state = State.Sword;
            }
            else if (randomNum == 2)
            {
                state = State.Laser;
            }
        _isAttack = false;
        }
    }
    //불릿4방향으로쏘는패턴
    IEnumerator AnimationEvent()
    {
        float _BulletAngle = 0;
        for (int j = 0; j < 30; ++j)
        {
            if (_isDie)
                break;
            for (int i = 0; i < 4; ++i)
            {
                Vector3 dirVec = _BossBack.transform.position;

                float angle = 90 * i+ _BulletAngle;
                FireBulet(dirVec, angle);
            }
            yield return new WaitForSeconds(0.2f);
            _BulletAngle += 5;
        }
        state = State.Normal;
  
    
    }
    //맞을때 호출
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);
    }
    //오브젝트풀에서 비활성화된 총알 불러와서 발사
    public void FireBulet(Vector3 Dir, float _angle)
    {


        cBullet Bullet = cMonsterBullet.GetInstance.GetObject(5);
        Bullet.transform.position = Dir;
        Bullet.transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        Bullet._Start = true;
        Bullet.gameObject.SetActive(true);


        StartCoroutine(ActiveBullet(Bullet));
    }
    //검쏘는패턴
    IEnumerator FireSword(Vector3 Dir)
    {
        _BossSwordPoll[_CurBossSwordIndex].transform.position = Dir;       
        _BossSwordPoll[_CurBossSwordIndex].gameObject.SetActive(true);
        _BossSwordPoll[_CurBossSwordIndex]._Start = true;

        yield return new WaitForSeconds(0.4f);
        if (_isDie)
            StopCoroutine(FireSword(Dir));
       if (_CurBossSwordIndex >= _MaxBossSword - 1)
        {
            _SwordX = 0;
            _CurBossSwordIndex = 0;
        }
        else if(_CurBossSwordIndex <= _MaxBossSword - 1)
        {
            _SwordX += 2;
            Vector3 dirVec = new Vector3(_BossBack.transform.position.x + -4 + _SwordX, _BossBack.transform.position.y + 5, 0);
            _CurBossSwordIndex++;
            StartCoroutine(FireSword(dirVec));           
        }
        if (_BossSwordPoll[_CurBossSwordIndex] == _BossSwordPoll[4])
        {
            state = State.Normal;
            StartCoroutine("SkellBossState");
        }
    }
    //불릿발사후3초후 불릿삭제
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet._Start = false;
        Bullet.gameObject.SetActive(false);
        
    }


}
