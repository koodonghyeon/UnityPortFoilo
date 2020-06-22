using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어상태
public enum State
{
    Idle=0,
    Move,
    Attack,
    Dash,
    Jump,
    Damage,
    Die
}

//플레이어
public class Player : cCharacter
{
    [HideInInspector]
    //현제 게이지
    public cDash _Dash;
    //싱글톤변수
    private static Player _instacne;
    [HideInInspector]
    //체력
    public cHP _health;
    [HideInInspector]
    //포만감
    public cFoodGauge _Food;
    [HideInInspector]
    //방어력세팅
    public float Defense { set { value = _Defense; } }
    //공격력세팅
    public int _MinDamage { get { return _MinAtteckDamage; } set { _MinAtteckDamage = value; } }
    public int _MaxDamage { get { return _MaxAttackDamage; } set { _MaxAttackDamage = value; } }
    //이동
    float _horizontalMove;
    //점프파워
    public float _JumpPower;
    //캐릭터상태
    public State _state = State.Idle;
    //리지드바디
    [HideInInspector]
    public Rigidbody2D _Rigidbody;
    //대쉬잔상
    private ParticleSystem _DashEffect;
    //최대대시횟수
    public int _DashCount = 3;
    //최대 점프횟수
    [HideInInspector]
    public int _JumpCount = 2;
    //점프상태
    [HideInInspector]
    public bool _isJump = false;
    //대시카운트 충전용시간
    private float _Time = 0f;
    public int _MinAtteckDamage;//최소공격력
    public int _MaxAttackDamage;//최대공격력
    //마우스포지션
    private Vector2 _MousePosition;

    public BoxCollider2D _Box2D;
    [HideInInspector]
    //발판
    public BoxCollider2D foot;
    [HideInInspector]
    //버프
    public Animator _Buff;


    [HideInInspector]
    //크리티컬확률
    public int _Critical;
    [HideInInspector]
    //크리티컬데미지
    public int _CriticalDamage;
    [HideInInspector]
    //위력
    public float _Power;
    //플레이어 회전하기위한 변수
    private bool _isPosition = false;
    //플레이어 데미지 입는변수
    private bool _isCrash = true;
    public Animator _Ani { get { return _Anim; } }
    public bool _isMoveMap=false; 
    protected override void Awake()
    {
        //_health.Initialize(_InitHealth, _InitHealth);
        if (_instacne == null)
        {
            base.Awake();
            _instacne = this;
            DontDestroyOnLoad(gameObject);
            _currnetHP = 80;
            _Renderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
            _health.Initialize(_currnetHP, _currnetHP);
            _Rigidbody = gameObject.GetComponent<Rigidbody2D>();
            _DashEffect = transform.GetChild(5).GetComponent<ParticleSystem>();
            _MoveSpeed = 5.0f;
            _Critical = 20;
            _CriticalDamage = 50;
            _JumpPower = 12;
            _Power = 10;
              _Buff = transform.GetChild(6).GetComponent<Animator>();

            _Clip.Add(Resources.Load<AudioClip>("Sound/Jumping"));
            _Clip.Add(Resources.Load<AudioClip>("Sound/Hit_Player"));
            _Clip.Add(Resources.Load<AudioClip>("Sound/ui-sound-13-dash"));
            _Box2D = GetComponent<BoxCollider2D>();
      
        }
        else if (_instacne != null)
        {
            Destroy(this.gameObject);
        }
    }

    public static Player GetInstance
    {
        get
        {
            //싱글톤이 없다. 
            if (_instacne == null)
            {
                //그러면 씬에서 찾아온다.
                _instacne = GameObject.FindObjectOfType(typeof(Player)) as Player;
            }
            //싱글톤이 없다.
            if (_instacne == null)
            {
                //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                var gameObject = new GameObject(typeof(Player).ToString());
                _instacne = gameObject.AddComponent<Player>();
                //DontDestroyOnLoad(gameObject); -> 씬으로 넘어 갈 때 나는 파괘되지 않는다.
            }
            //그리고 반환한다.
            return _instacne;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale != 0)
        {
            if (!_isMoveMap)
            {
                if (_state != State.Die)
                {
                    //이동
                    _horizontalMove = Input.GetAxisRaw("Horizontal");
                    //점프상태
                    if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.S))
                    {
                        _isJump = true;
                    }

                    AnimationUpdate();
                    //hp확인
                    if (Input.GetKeyDown(KeyCode.O))
                    {
                        _health.MyCurrentValue -= 10;
                    }
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        _health.MyCurrentValue += 10;
                    }

                    //마우스포지션 불러오기
                    _MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                    //마우스좌표에따라 캐릭터 회전
                    if (_MousePosition.x < transform.position.x)
                    {
                        if (_isPosition)
                        {
                            transform.rotation = Quaternion.Euler(0, 180, 0);

                            _isPosition = false;
                        }
                    }
                    else if (_MousePosition.x > transform.position.x)
                    {
                        if (!_isPosition)
                        {
                            transform.rotation = Quaternion.identity;

                            _isPosition = true;
                        }
                    }

                    //대시횟수 충전
                    _Time += Time.deltaTime;
                    if (_Time >= 1.0f)
                    {

                        if (_DashCount < 3)
                        {
                            _Dash.SetEnabled(_DashCount);
                            _DashCount += 1;
                        }
                        _Time = 0;
                    }

                   
                }
            }
        }
    }

    private void FixedUpdate()
    {

        if (_state != State.Die)
        {
            if (!_isMoveMap)
            {
                Jump();
                Move();

                if (Input.GetMouseButtonDown(1))
                {

                    if (_DashCount != 0)
                    {
                        StartCoroutine("Dash");
                    }
                }
            }
            //HP0되면 죽는것
            if (_health.MyCurrentValue == 0)
            {
                _Box2D.isTrigger = false;
                _state = State.Die;
            }
        }
    }

    void Move()
    {
        //대시상태일때 이동안됨
        if (_state == State.Dash)
        {
            return;
        }

        _state = State.Move;

        _Rigidbody.AddForce(Vector2.right * _horizontalMove * 2, ForceMode2D.Impulse);
        if (_Rigidbody.velocity.x > _MoveSpeed)
        {
    
            _Rigidbody.velocity = new Vector2(_MoveSpeed, _Rigidbody.velocity.y);
        }
        else if (_Rigidbody.velocity.x < _MoveSpeed * (-1))
        { 
            _Rigidbody.velocity = new Vector2(_MoveSpeed * (-1), _Rigidbody.velocity.y);
        }
        if (_horizontalMove == 0)
        {
            _Rigidbody.velocity = new Vector2(0, _Rigidbody.velocity.y);
        }

    }


    void Jump()
    {
        //점프상태가 아니거나 점프 횟수가 없으면 리턴
        if (!_isJump || _JumpCount == 0)
        {
            return;
        }
        _Audio.clip= _Clip[0];
        _Audio.Play();
        _state = State.Jump;
        _Rigidbody.velocity = Vector3.zero;
        _Rigidbody.AddForce(Vector2.up * _JumpPower, ForceMode2D.Impulse);
        _Anim.SetBool("Jump", true);
        _JumpCount -= 1;
        _isJump = false;
        _state = State.Idle;
    }
    IEnumerator Dash()
    {

        //대시횟수가 0이아니거나 대시상태가 아닐때 실행
        if (_state != State.Dash)
        {
            _state = State.Dash;
            _Audio.clip = _Clip[2];
            _Audio.Play();
            _DashEffect.Play();
            int _Index = 0;
            Vector3 _StartPoint = transform.position;
            Vector2 _EndPoint = _MousePosition;
            Vector2 _Direction = new Vector2(_EndPoint.x - _StartPoint.x, _EndPoint.y - _StartPoint.y).normalized;
            float _Range = Vector2.Distance(_StartPoint, _EndPoint);
            if (_Range >= 4.0f)
            {
                _Range = 4.0f;
            }
            if (_DashEffect.isPlaying)
            {
                while (_Index < 4)
                {

                    _Index += 1;
                    _Rigidbody.velocity = Vector3.zero;
                    Vector2 m_vecLerp = Vector2.Lerp(_StartPoint, (Vector2)_StartPoint + (_Direction * _Range), (float)_Index / 4.0f);
                    _Rigidbody.MovePosition(m_vecLerp);
                    yield return new WaitForSeconds(0.005f);
                }
            }
            _Dash.SetEnabledfasle(_DashCount - 1);
            _DashCount -= 1;

            _state = State.Idle;
        }
    }

    void AnimationUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            _Anim.SetBool("Run", false);
            _state = State.Idle;
        }
        else
        {
            _Anim.SetBool("Run", true);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Brige") {
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S))
            {
                foot.enabled = false;
                Invoke("Jumper", 0.4f);
            }
        }
    }

    public void Jumper()
    {
        foot.enabled = true;
    }

    public void HIT(int dam)
    {
        if (_state != State.Die)
        {
            if (_isCrash)
            {

                _Audio.clip = _Clip[1];
                _Audio.Play();
                _health.MyCurrentValue -= dam;
                _isCrash = false;
                _Renderer.color = new Color(1, 1, 1, 0.5f);
                StartCoroutine(SetCrash());
            }
        }
    }
    IEnumerator SetCrash()
    {

        yield return new WaitForSeconds(1.3f);
        _isCrash = true;
        _Renderer.color = new Color(1, 1, 1, 1);
    }
    public bool isCritical()
    {
        float Critical;
        Critical = Random.Range(1, 101);
        if (Critical <= _Critical)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
