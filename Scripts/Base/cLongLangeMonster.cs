using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//원거리몬스터 상단
public class cLongLangeMonster : cMonsterBase
{
    protected float _ShootDelay;///총알 딜레이
    protected float _ShootTimer;//총알 타이머
    protected override void Awake()
    {
        base.Awake();

    }
    //몬스터 맞을때 호출되는함수
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);
    }
    //골드 드랍하는함수
    public override void DropGold()
    {
    }

}
