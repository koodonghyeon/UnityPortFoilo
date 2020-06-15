using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cBossMonster : cMonsterBase
{
    //보스검
    protected cBossSword _BossSword;
    //검을 저장해놓는풀
    protected List<cBossSword> _BossSwordPoll = new List<cBossSword>();

    protected int _CurBossSwordIndex = 0;
    protected int _MaxBossSword;

 
    DieEffect _DieEffect;
    protected override void Awake()
    {
        base.Awake();
        _DieEffect = FindObjectOfType<DieEffect>();
    }
    //보스타격
    public override void MonsterHIT(int dam, bool isCritical)
    {
        if (_currnetHP > 0)
        {
             _Audio.clip = _Clip[0];
             _Audio.Play();
             GameObject Dam = Instantiate(_Damage);
             Dam.transform.position = new Vector3(this.transform.position.x+1, this.transform.position.y-1, this.transform.position.z);
            Dam.transform.localScale = new Vector3(1.5f, 1.5f, 0);
             Dam.GetComponent<cDamageText>().SetDamage(dam, isCritical);
            _Renderer.color = new Color(1, 0, 0, 1);
            _currnetHP -= dam;
            _HPBarBackGround.SetActive(true);
            _HPBar.fillAmount = (float)_currnetHP / _MaxHP;
            StartCoroutine(SetRed());
            
        }
       
    }
    //보스 타격시이펙트
    IEnumerator SetRed()
    {
        yield return new WaitForSeconds(0.2f);
        _Renderer.color = new Color(1, 1, 1, 1);
    }
    //보스죽을때
    public override void Die(GameObject gameObject)
    {
        transform.parent.parent.GetComponent<cBossMap>().ReMoveBoss(gameObject);
        transform.parent.parent.GetComponent<cBossMap>()._isBox = true;
            _DieEffect.Die(this.gameObject);
        StartCoroutine(Gold());
        _isDie = true;

    }
    //골드드랍
    IEnumerator Gold()
    {
        for (int i = 0; i < 20; ++i)
        {
            GameObject obj = Instantiate(_BigGold) as GameObject;
            obj.transform.position = this.transform.position;
            _GoldX = Random.Range(-100, 100);
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(_GoldX, _GoldFower));
            yield return new WaitForSeconds(0.1f);
        }
    }

}
