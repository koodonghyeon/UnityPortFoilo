using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스 죽을때 터지는 이펙트
public class DieEffect : MonoBehaviour
{
    //이펙트 저장할 리스트
    public List<Animator> _Effect = new List<Animator>();
    AudioSource _Audio;
    AudioClip _Clip;
    private void Awake()
    {
        for(int i =0; i < transform.childCount; ++i)
        {
            _Effect.Add(transform.GetChild(i).GetComponent<Animator>());
        }
        _Audio = GetComponent<AudioSource>();
        _Clip=Resources.Load<AudioClip>("Sound/MonsterDie");
        _Audio.clip = _Clip;
    }

    public void Die(GameObject DIe)
    {
        StartCoroutine(Effect(DIe));
    }
    //저장된순서대로 이펙트를 터트려주고 다끝나면 몬스터 삭제
    IEnumerator Effect(GameObject DIe)
    {
        for (int i = 0; i < _Effect.Count; ++i)
        {
            yield return new WaitForSeconds(0.3f);
            _Effect[i].SetTrigger("Die");
            _Audio.Play();
        }
        yield return new WaitForFixedUpdate();
        Destroy(DIe);
    }

}
