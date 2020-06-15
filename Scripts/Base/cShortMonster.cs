using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//근거리 몬스터
public class cShortMonster : cMonsterBase
{

    protected override void Awake()
    {
        base.Awake();

    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);
    }
    public override void DropGold()
    {
       
    }

}
