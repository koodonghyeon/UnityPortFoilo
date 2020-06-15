using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//아이템등급
public enum ItemType
{
    OneShot,
    Gun,
    Sword,
    Spear
}
//아이템등급
public enum ItemQuality
{
    Normal,
    Rare,
    Unique
}
//아이템최상단
[System.Serializable]
public class Item : MonoBehaviour
{                                      
    public int _ItemID                               ;              //아이템번호
    public string _ItemName                          ;              //아이템이름
    public string _ItemDescrIption                   ;              //아이템설명
    public Sprite _ItemIcon                          ;              //아이템이미지
    public  ItemType _Type                           ;              //아이템타입
    public int _MinAttackDamage                      ;              //공격최소데미지
    public int _MaxAttackDamage                      ;              //공격최대데미지
    public float _AttackSpeed                        ;              //공격속도
    public ItemQuality _Quality                      ;              //아이템등급
    public float _ItemPrice                          ;              //아이템 가격
    public string _SkillText                         ;              //스킬설명
    public int _Dam                                  ;              //공격데미지
    public bool _Skill=true                          ;              //스킬사용가능여부
    public Sprite _SkillIcon                         ;              //스킬이미지
    public float _SkillCoolTime                      ;              //스킬쿨타임  
    public float _NowCoolTIme;                                      //쿨타임이 얼마나 돌아갔나여부
    public float _CollTimeFillAmount;
    
    public virtual void Skill() { }


}
//근접무기
public class Shortrange : Item
{

    public override void Skill()
    { }
    //몬스터 공격시 호출되는함수
    public virtual void Attack(cMonsterBase Monster)
    {
        if (Monster != null)
        {
            int randomDamage = Random.Range((int)Player.GetInstance._MinDamage, (int)Player.GetInstance._MaxDamage + 1);
            if (Player.GetInstance.isCritical())
            {
                _Dam = (randomDamage - Monster._Defense) + (int)((float)randomDamage * ((float)Player.GetInstance._CriticalDamage / 100.0f))
                    + (int)((float)randomDamage * ((float)Player.GetInstance._Power / 100));
                Monster.MonsterHIT(_Dam, true);
            }
            else
            {
                _Dam = (randomDamage - Monster._Defense) + (int)((float)randomDamage * ((float)Player.GetInstance._Power / 100));
                Monster.MonsterHIT(_Dam, false);
            }
        }
    }

}
//원거리무기
public class Longrange : Item
{
    protected List<cBullet> _BulletPoll = new List<cBullet>(); //풀에 담을
    protected AudioSource _ItemSound;
    protected List<AudioClip> _GunSound=new List<AudioClip>();
    public float _Delay;
    public int _MaxBullet;
    public int _CurBulletIndex = 0;     //현재 장전된 총알의 인덱스
    public float _ReloadTime;
    protected Text _BulletUI;

    protected bool _isReload=false;
    public GameObject _Reload;
    public Image _ReloadBar;
    protected virtual void Awake()
    {
        _BulletUI = cUIManager.GetInstance.GetWeaPonSlot().transform.GetChild(1).GetChild(1).GetComponent<Text>();
        _Reload = Player.GetInstance.transform.GetChild(7).gameObject;
        _ReloadBar = _Reload.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _ItemSound = GetComponent<AudioSource>();
        _GunSound.Add(Resources.Load<AudioClip>("Sound/Gun"));
         _GunSound.Add(Resources.Load<AudioClip>("Sound/Reload"));
        _GunSound.Add(Resources.Load<AudioClip>("Sound/Reload2"));
        _GunSound.Add(Resources.Load<AudioClip>("Sound/50_sniper_shot-Liam-2028603980"));
        _GunSound.Add(Resources.Load<AudioClip>("Sound/rlaunch(Hecate"));

    }
    //스킬
    public override void Skill()
    { }
    //장전
    public virtual void Reload()
    {
        StartCoroutine(ReloadCourutin());
    }
    //총알발사
    public virtual void FireBulet(Vector2 Position, float _angle)
    {
        if (!_isReload)
        {
            if (_CurBulletIndex == _MaxBullet)
            {
                return;
            }

            _BulletPoll[_CurBulletIndex].transform.position = Position;

            _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
            _ItemSound.clip = _GunSound[0];
            _ItemSound.Play();
            _BulletPoll[_CurBulletIndex]._Start = true;
            _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);
            _BulletUI.text = (_MaxBullet - (_CurBulletIndex + 1)).ToString() + "  /  " + _MaxBullet.ToString();
            StartCoroutine("ActiveBullet", _BulletPoll[_CurBulletIndex]);
            if (_CurBulletIndex >= _MaxBullet - 1)
            {
                StartCoroutine(ReloadCourutin());
            }
            else
            {
                _CurBulletIndex++;
            }
        }
    }
    //장전코루틴
    IEnumerator ReloadCourutin()
    {
        if (_CurBulletIndex != 0)
        {
            _isReload = true;
            _ItemSound.clip = _GunSound[1];
            _ItemSound.Play();
            _Reload.gameObject.SetActive(true);
            _ReloadBar.fillAmount = 1;
            while (_ReloadBar.fillAmount > 0)
            {
     
                _ReloadBar.fillAmount -= 1 * Time.deltaTime / _ReloadTime;

                yield return null;

            }
            yield return new WaitForFixedUpdate();
            _Reload.gameObject.SetActive(false);
            _ItemSound.clip = _GunSound[2];
            _ItemSound.Play();
            _CurBulletIndex = 0;
            _BulletUI.text = (_MaxBullet - _CurBulletIndex).ToString() + "  /  " + _MaxBullet.ToString();
            _isReload = false;
        }
    }
    //총알발사후3초뒤에 총알 끄기
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }
    //스나이퍼 스킬 버프해제용
    public virtual void DamDown()
    {
        if(_CurBulletIndex>0)
        _BulletPoll[_CurBulletIndex - 1]._Damage = 0;
    }
}