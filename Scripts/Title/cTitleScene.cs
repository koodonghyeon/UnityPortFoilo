using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//뒷배경 움직이기
public static class cTransform
{
    public static bool Compare(this Vector3 position, ref Vector3 rhs)
    {
        if (position.x >= rhs.x)
        {
            return false;
        }
        return true;
    }
    public static void Swap(this Transform position, ref Transform lhs, ref Transform rhs)
    {
        Transform temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
}
//타이틀씬
public class cTitleScene : MonoBehaviour
{
    //이동하는 오브젝트들
    public Transform[] _BackGroundFront = new Transform[2];
    public Transform[] _BackGround2 = new Transform[2];
    public Transform[] _Cloud1 = new Transform[2];
    public Transform[] _Cloud2 = new Transform[2];
    readonly uint BACKGROUND_SIZE = 2;
    private Vector3 _BackGroind1Finsh = new Vector3(-27.4f, 0, 0);
    private Vector3 _BackGroind2Finsh = new Vector3(-25.4f, 0, 0);
    private Vector3 _Cloud1Finsh = new Vector3(-21.8f, 0, 0);
    private Vector3 _Cloud2Finsh = new Vector3(-18.2f, 0, 0);
    [SerializeField] private float _BackGround1Speed = 5.0f;
    [SerializeField] private float _BackGround2Speed = 5.0f;
    [SerializeField] private float _Cloud1Speed = 5.0f;
    [SerializeField] private float _Cloud2Speed = 5.0f;
    private Transform _Target1 = null;
    private Transform _Target2 = null;
    private Transform _CloudTarget1 = null;
    private Transform _CloudTarget2 = null;
    private AudioClip _Clip;
    private AudioSource _Source;
    //마우스커서
    public Texture2D _CursorTexture;
    public CursorMode _CursorMode = CursorMode.Auto;
    private void Awake()
    {
        _Target1 = _BackGroundFront[0];
        _Target2 =_BackGround2[0];
        _CloudTarget1 = _Cloud1[0];
        _CloudTarget2 = _Cloud2[0];
        _Source = GetComponent<AudioSource>();
        _Clip = Resources.Load<AudioClip>("Sound/title");
        _Source.clip = _Clip;
        _Source.Play();
        _CursorTexture = Resources.Load<Texture2D>("UI/BasicCursor");
        Cursor.SetCursor(_CursorTexture, Vector2.zero, _CursorMode);
    }
    private void FixedUpdate()
    {
        //1번이동
        for (int i = 0; i < BACKGROUND_SIZE; ++i)
        {
            _BackGroundFront[i].position -= new Vector3(Time.deltaTime * _BackGround1Speed, 0, 0);
            _BackGround2[i].position -= new Vector3(Time.deltaTime * _BackGround2Speed, 0, 0);
            _Cloud1[i].position -= new Vector3(Time.deltaTime * _Cloud1Speed, 0, 0);
            _Cloud2[i].position -= new Vector3(Time.deltaTime * _Cloud2Speed, 0, 0);
        }
        if (_Target1.position.Compare(ref _BackGroind1Finsh))
        {

            _Target1.position =_BackGroundFront[BACKGROUND_SIZE - 1].position+ new Vector3(37.03f, 0, 0);

            for (int i = 0; i < BACKGROUND_SIZE - 1; ++i)
            {
                _Target1.Swap(ref _BackGroundFront[i], ref _BackGroundFront[i + 1]);
            }
            _Target1 = _BackGroundFront[0];
        }
        //백그라운드 2번이동
        if (_Target2.position.Compare(ref _BackGroind2Finsh))
        {

            _Target2.position = _BackGround2[BACKGROUND_SIZE - 1].position + new Vector3(33.0f, 0, 0);

            for (int i = 0; i < BACKGROUND_SIZE - 1; ++i)
            {
                _Target2.Swap(ref _BackGround2[i], ref _BackGround2[i + 1]);
            }
            _Target2 = _BackGround2[0];
        }
        //구름1번이동
        if (_CloudTarget1.position.Compare(ref _Cloud1Finsh))
        {

            _CloudTarget1.position = _Cloud1[BACKGROUND_SIZE - 1].position+ new Vector3(37.4f, 0, 0);

            for (int i = 0; i < BACKGROUND_SIZE - 1; ++i)
            {
                _CloudTarget1.Swap(ref _Cloud1[i], ref _Cloud1[i + 1]);
            }
            _CloudTarget1 = _Cloud1[0];
        }
        //구름2번이동
        if (_CloudTarget2.position.Compare(ref _Cloud2Finsh))
        {

            _CloudTarget2.position = _Cloud2[BACKGROUND_SIZE - 1].position + new Vector3(38.0f, 0, 0);

            for (int i = 0; i < BACKGROUND_SIZE - 1; ++i)
            {
                _CloudTarget2.Swap(ref _Cloud2[i], ref _Cloud2[i + 1]);
            }
            _CloudTarget2 = _Cloud2[0];
        }
    }
}
