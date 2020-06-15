using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//몬스터최상단
public class cMonsterBase : cCharacter
{


   //데미지텍스트
    protected GameObject _Damage;
    //몬스터 체력바
    protected GameObject _HPBarBackGround;
    protected Image _HPBar;
    //죽으면 드랍될 골드 프리팹
    protected GameObject _SmallGold;
    protected GameObject _BigGold;
    //골드에 붙여진 리지드바디
    protected Rigidbody2D _SmallGoldRigidBody;
    protected Rigidbody2D _BigGoldRigidBody;
    //골드 드랍되는 파워
    protected int _GoldFower;
    //공격력
    protected int _AttackDamage;
    //골드 사방으로 뿌리기위한 변수
    protected int _GoldX;
    //몬스터가 죽었나여부
    protected bool _isDie=false;
    //플레이어방향
    protected Vector2 _Dir;

    protected override void Awake()
    {
        base.Awake();

        _Clip.Add(Resources.Load<AudioClip>("Sound/Hit_Monster"));
        _Clip.Add(Resources.Load<AudioClip>("Sound/MonsterDie"));
        _HPBarBackGround = transform.GetChild(0).GetChild(0).gameObject;
        _HPBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        _HPBar.fillAmount = 1;
        _BigGold= Resources.Load("Prefabs/Item/Bullion") as GameObject;
        _SmallGold = Resources.Load("Prefabs/Item/GoldCoin") as GameObject;
        _Damage = Resources.Load("Prefabs/Text") as GameObject;
        _GoldFower = 200;
       
    }
    //몬스터 맞을때 호출되는함수
    public virtual void MonsterHIT(int dam,bool isCritical)
    {

        if (_currnetHP > 0)
        {
            _Audio.clip = _Clip[0];
             _Audio.Play();
               GameObject Dam = Instantiate(_Damage);
             Dam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            Dam.GetComponent<cDamageText>().SetDamage(dam, isCritical);
            _currnetHP -= dam;
            _HPBarBackGround.SetActive(true);
            _HPBar.fillAmount = (float)_currnetHP / _MaxHP;
            CancelInvoke();
            Invoke("ActiveHP", 3f);
        }


    }

   

   //몬스터 죽을때 호출되는 함수
    public virtual void Die(GameObject gameObject)
    {
        DropGold();
        transform.parent.parent.parent.GetComponent<cMapManager>().ReMoveMonster(gameObject);
        _Audio.clip = _Clip[1];
        _Audio.Play();
        _Anim.SetTrigger("Die");

    }
    //죽으면 호출될것
    void SetActive()
    {

        Destroy(this.gameObject);
    }
    //골드드랍
    public virtual void DropGold()
    {
        
    }
    //HP바 숨기기
    void ActiveHP()
    {
        _HPBarBackGround.SetActive(false);
    }
}
