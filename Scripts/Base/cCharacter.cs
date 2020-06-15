using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터, 플레이어 최상단
public class cCharacter : MonoBehaviour
{
    public int _currnetHP;//체력
    public int _MaxHP;//최대체력

    public float _MoveSpeed;//이동속도
    public int _Defense = 0;//방어력
    protected SpriteRenderer _Renderer;
    protected Animator _Anim;
    protected AudioSource _Audio;
    protected List<AudioClip> _Clip = new List<AudioClip>();
    protected Rigidbody2D _Rigid;
    protected virtual void Awake()
    {

        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        _Audio = gameObject.GetComponent<AudioSource>();
        _Anim = gameObject.GetComponentInChildren<Animator>();
        _Rigid = gameObject.GetComponentInChildren<Rigidbody2D>();


    }
}
