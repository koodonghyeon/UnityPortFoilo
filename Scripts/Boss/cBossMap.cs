using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스맵
public class cBossMap : MonoBehaviour
{
    AudioSource _Audio;
    List<AudioClip> _Clip = new List<AudioClip>();
    //현제맵에 몬스터가 있나여부
    public bool _isMonster = true;
    //맵에있는 몬스터
    public List<Transform> _MonsterList = new List<Transform>();
    //문리스트
    public List<cDoor> _DoorList = new List<cDoor>();
    //문여닫이용 델리게이트, 열린맵 리스트에 집어넣기용
    private delegate void _Door();
    //문닫기
    private _Door _DoorClose;
    //문열기
    private _Door _DoorOpen;
    //박스 생성될 위치
    private GameObject _Box;
    //유니크박스
    private GameObject _UniqueBox;
    //몬스터들 관리할 상위 오브젝트
    private Transform _Monster;
    //박스가 등장했나여부
    public bool _isBox=false;

    public bool _Boss = false;

    void Awake()
    {
        _Audio = GetComponent<AudioSource>();
        _Clip.Add(Resources.Load<AudioClip>("Sound/itembox"));
        _Clip.Add(Resources.Load<AudioClip>("Sound/Chest2"));


        _UniqueBox = Resources.Load("Prefabs/DunguenBox/UniqueBox") as GameObject;
     
        SetNowMap();

    }
    void Update()
    {

        if (_MonsterList.Count == 0)
        {
            _isMonster = false;
            if (_isBox)
            {
                SetBox();
                _isBox = false;
            }
        }
        if (_Boss)
        {
            if (_isMonster)
            {
                _DoorClose?.Invoke();
            }
            else if (!_isMonster)
            {

                _DoorOpen?.Invoke();

            }
        }
        
    }
    //문세팅
    public void DoorSetting()
    {
        //문 델리게이트 추가 및 문리스트에 추가
        Transform Door = transform.Find("Door");
        if (Door != null)
        {
            _DoorList.Clear();
            for (int i = 0; i < Door.transform.childCount; ++i)
            {
                _DoorList.Add(Door.transform.GetChild(i).GetComponent<cDoor>());

            }
            for (int i = 0; i < Door.transform.childCount; ++i)
            {
                _DoorOpen += Door.transform.GetChild(i).GetComponent<cDoor>().Open;
                _DoorClose += Door.transform.GetChild(i).GetComponent<cDoor>().Close;
            }
        }
    }
    //현제 맵세팅
    public void SetNowMap()
    {
            DoorSetting();
            _MonsterList.Clear();
             _Monster = transform.Find("Monster");
            _Box=transform.Find("Box").gameObject;
    }
    //클리어시 박스
    void SetBox()
    {
                GameObject obj = Instantiate(_UniqueBox) as GameObject;
                obj.transform.position = _Box.transform.position;
                obj.transform.SetParent(_Box.transform);
                _Audio.Play();
    }
    //보스 등장!
    public void SetBoss()
    {
 
            _MonsterList.Add(_Monster.transform.GetChild(0));

            _Monster.transform.GetChild(0).gameObject.SetActive(true);
  
        _isMonster = true;
    }
    //보스죽었을때
    public void ReMoveBoss(GameObject gameObject)
    {
   _MonsterList.Remove(gameObject.transform);
    }
}
