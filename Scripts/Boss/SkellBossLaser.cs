using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//보스손
public class SkellBossLaser : MonoBehaviour
{
    //이동속도
    float _Speed = 5.0f;
    //레이저를 발사하냐여부
    public bool _Fire = false;
    //타겟포지션
    Vector3 _TargetPosition;
    //플레이어 피격판정용박스
    BoxCollider2D _HitBox;
    //몇번쐇냐
    public int _Count;
    //공격력
    private int _Damage;
    //애니메이터 배열
    public Animator[] _anim;
    
    //다음에 레이저쏠손
    public SkellBossLaser _SkellLaser;
    private void Awake()
    {
        _HitBox = GetComponent<BoxCollider2D>();
        _HitBox.enabled = false;
        _Damage = 9;
  
    }
    private void FixedUpdate()
    {
        
        if (_Fire)
        {
            transform.position = Vector3.MoveTowards(transform.position, _TargetPosition, _Speed * Time.deltaTime);

            if (this.transform.position == _TargetPosition)
            {
                _anim[0].SetTrigger("Fire");
                for (int i = 1; i < 3; i++)
                {
                   
                    StartCoroutine("StartAnimation", i);
                }
                _Fire = false;
            }
        }
        else if (!_Fire)
        {
            _TargetPosition = new Vector3(transform.position.x, Player.GetInstance.transform.position.y, 0);
      
        }
    }
    //손쏘는패턴후 몇초후에 히트박스 생기고 레이저발사
    IEnumerator StartAnimation(int i)
    {
        yield return new WaitForSeconds(0.8f);
        _HitBox.enabled = true;
        _anim[i].SetTrigger("Fire");
       
    }
    //레이저패턴이 3번만 반복되게 하기위함
    void AnimationEvent()
    {
            _Count += 2;
        if (_Count < 3)
        {
            _SkellLaser._Fire = true;
            this._HitBox.enabled = false;
        }
    }
    //플레이어 때리면 데미지 줌
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.GetInstance.HIT(_Damage);
            _HitBox.enabled = false;
        }
    }
}
