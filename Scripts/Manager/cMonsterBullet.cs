using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//몬스터 오브젝트풀
public class cObject
{

    public List<cBullet> _BulletList= new List<cBullet>();
    public int _MaxIndex;
    public int _index = 0;


    public void Inititlized(ref GameObject obj,int MaxIndex, int Damage,float Speed,BulletState Type,Transform transform)
    {
        _MaxIndex = MaxIndex;
        //복사본 갯수에 맞춰 만들어주기.
        for (int i = 0; i < _MaxIndex; ++i)
        {
            GameObject _obj = MonoBehaviour.Instantiate(obj);
            cBullet _Bullet = _obj.GetComponent<cBullet>();
            _Bullet._Speed = Speed;
            _Bullet._Damage = Damage;
            _Bullet._BulletState = Type;
            _Bullet._Start = false;
            _Bullet.transform.SetParent(transform);
            _Bullet.gameObject.SetActive(false);
            _BulletList.Add(_Bullet);
        }
    }

    public cBullet GetOneObject
    {
        get
        {
      
            if (_index >= _MaxIndex-1)
                _index = 0;
            if (_BulletList[_index].gameObject.activeSelf)
            {
                ++_index;
            }

            cBullet obj = _BulletList[_index];
       
    
        
            return obj;
        }
    }
    public void AllFire()
    {
       
    }
}

public enum Bullet
{
    Benshee,
    RedBat,
    BigBat,
    BigRadBat,
    Arrow
}
public class cMonsterBullet : cSingleton<cMonsterBullet>
{
    public List<cObject> m_objects;

    protected override void Awake()
    {
        base.Awake();

        m_objects = new List<cObject>();

        cObject BansheeBullet = new cObject();
        GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/BansheeBullet")) as GameObject;
        obj.SetActive(false);
        BansheeBullet.Inititlized(ref obj, 24,6,5, BulletState.Boss, transform);
        m_objects.Add(BansheeBullet);
       
        obj = Instantiate(Resources.Load("Prefabs/Bullet/BabyBatBullet")) as GameObject;
        obj.SetActive(false);
        cObject BatBullet = new cObject();
        BatBullet.Inititlized(ref obj, 15, 3, 5, BulletState.Monster, transform);
        m_objects.Add(BatBullet);

        obj = Instantiate(Resources.Load("Prefabs/Bullet/Arrow")) as GameObject;
        obj.SetActive(false);
        cObject Arrow = new cObject();
        Arrow.Inititlized(ref obj, 15, 5, 5, BulletState.Monster, transform);
        m_objects.Add(Arrow);

        obj = Instantiate(Resources.Load("Prefabs/Bullet/BatBullet")) as GameObject;
        obj.SetActive(false);
        cObject BigRadBat = new cObject();
        BigRadBat.Inititlized(ref obj, 40, 5, 5, BulletState.Monster, transform);
        m_objects.Add(BigRadBat);

        obj = Instantiate(Resources.Load("Prefabs/Bullet/BatBullet")) as GameObject;
        obj.SetActive(false);
        cObject GiantBat = new cObject();
        GiantBat.Inititlized(ref obj, 40, 5, 5, BulletState.Monster, transform);
        m_objects.Add(GiantBat);



        obj = Instantiate(Resources.Load("Prefabs/Boss/BossBullet")) as GameObject;
        obj.SetActive(false);
        cObject BossBullet = new cObject();
        BossBullet.Inititlized(ref obj, 120, 9, 5, BulletState.Boss, transform);
        m_objects.Add(BossBullet);
       



    }



    public cBullet GetObject(int index)
    {
        cBullet obj = m_objects[index].GetOneObject;
            return obj;

    }
    public void AllFire(int index)
    {
        for(int i =0;i < m_objects[index]._BulletList.Count; ++i)
        {
            if (m_objects[index]._BulletList[i].gameObject.activeSelf)
            {
                m_objects[index]._BulletList[i]._Start = true;
            }
        }
    }


    public void ActiveBullet(int index)
    {


        for (int i = 0; i < m_objects[index]._BulletList.Count; ++i)
        {
            if (m_objects[index]._BulletList[i].gameObject.activeSelf)
            {
                StartCoroutine(Active(m_objects[index]._BulletList[i]));
            }

        }

    }
    IEnumerator Active(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet._Start = false;
        Bullet.gameObject.SetActive(false);

    }
}
