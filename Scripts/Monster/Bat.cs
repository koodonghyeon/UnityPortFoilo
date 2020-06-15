using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : cShortMonster
{

    public int _moveRangeX;
    public int _moveRangeY;
   protected override void Awake()
    {
        base.Awake();
        _Rigid = GetComponent<Rigidbody2D>();
        Invoke("MoveRange", 1);
        _MaxHP = 15;
        _currnetHP = 15;
        _Defense = 0;
    }

    // Update is called once per frame
   void FixedUpdate()
    {

        _Rigid.velocity = new Vector2(_moveRangeX, _moveRangeY);

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "floor" || other.gameObject.tag == "Wall")
        {
            _moveRangeX *= -1 ;
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
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);
       
    }
    public override void DropGold()
    {
        for (int i = 0; i <= 4; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex >= 50 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                _GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            }
        }
    }
}
