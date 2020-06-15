using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//카메라메니저
public class cCameramanager : cSingleton<cCameramanager>
{
    //카메라 흔들기
    private float _CameraShake =0.05f;
    float _ShakeTime;
    //따라갈 타겟
    public GameObject _Target;
    //이동속도
    private float _MoveSpeed=5;
    //타겟위치
    private Vector3 _TargetPosition;
    //카메라가 나가지못할 영역
    private Collider2D Bound;
    //최소 영역
    private Vector3 _minBound;
    //최대 영역
    private Vector3 _maxBound;
    //카메라의 반너비,반높이 값을 지닐변수
    private float _halfWidth;
    private float _halfHeight;
    //카메라 의 반높이 값을 구할 속성을 이용하기 위한변수
    private Camera _theCamera;

    protected override void Awake()
    {
        base.Awake();
        _theCamera = GetComponent<Camera>();
            Bound = GameObject.Find("Bound").GetComponent<Collider2D>();
            _minBound = Bound.bounds.min;
            _maxBound = Bound.bounds.max;
            _halfHeight = _theCamera.orthographicSize;
            _halfWidth = _halfHeight * Screen.width / Screen.height;

    }
  
    void Update()
    {
        if(_Target.gameObject != null)
        {
            _TargetPosition.Set(_Target.transform.position.x, _Target.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, _TargetPosition, _MoveSpeed * Time.deltaTime);
            //카메라 영역조절
            float clampedX = Mathf.Clamp(this.transform.position.x, _minBound.x + _halfWidth, _maxBound.x - _halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, _minBound.y + _halfHeight, _maxBound.y - _halfHeight);
            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
        if(_ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * _CameraShake+ new Vector3(transform.position.x, transform.position.y, -10);
            _ShakeTime -= Time.deltaTime;
        }
        else
        {
            _ShakeTime = 0f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        //마우스포지션 불러오기


    }
    //흔들기
    public void VibrateForTime()
    {
        _ShakeTime = 0.1f;
    }
    //카메라영역설정
    public void SetBound(Collider2D newBound)
    {
        Bound = newBound;
        _minBound = Bound.bounds.min;
        _maxBound = Bound.bounds.max;
    }
    //따라갈 타겟설정
    public void SetTarget(GameObject target,float MoveSpeed)
    {
        _Target = target;
        _MoveSpeed = MoveSpeed;
    }


}
