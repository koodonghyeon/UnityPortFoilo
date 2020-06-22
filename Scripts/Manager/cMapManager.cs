using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//맵을 관리하는 매니저
public class cMapManager : MonoBehaviour
{

    AudioSource _Audio;
   List<AudioClip> _Clip= new List<AudioClip>();
    //현제 맵
    public Transform _NowMap =null;
    //열린맵 리스트
    public List<Transform> _OpenMapList = new List<Transform>();
    //닫힌맵리스트
    public List<Transform> _CloseMapList = new List<Transform>();
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
    //노말박스
    private GameObject _NormalBox;
    //레어박스
    private GameObject _RareBox;
    //유니크박스
    private GameObject _UniqueBox;
    //박스 생성안됬을때
    private GameObject _NoonBox;
    //몬스터들 관리할 상위 오브젝트
    private Transform _Monster;
    //작은 페어리
    private GameObject _SmallFairy;
    //큰페어리
    private GameObject _BigFairy;
    //박스 관리용 리스트
    public List<GameObject> _BoxList=new List<GameObject>();
    //문열고닫는거 한번만 호출되게할변수
    bool _isOpen;
    //월드맵 
    public cWorldMap cworldMap;
    //텔포
    public WormPassage wormPassage;
  
    void Awake()
    {
     
        _OpenMapList.Add(transform.GetChild(0));
        for(int i =1; i < transform.childCount; ++i)
        {
            _CloseMapList.Add(transform.GetChild(i));
        }
        SetNowMap(_OpenMapList[0]);
        _Audio = GetComponent<AudioSource>();
        _Clip.Add(Resources.Load<AudioClip>("Sound/itembox"));
        _Clip.Add(Resources.Load<AudioClip>("Sound/Chest2"));

        _NormalBox = Resources.Load("Prefabs/DunguenBox/NormalBox") as GameObject;
        _RareBox = Resources.Load("Prefabs/DunguenBox/RareBox") as GameObject;
        _UniqueBox = Resources.Load("Prefabs/DunguenBox/UniqueBox") as GameObject;
        _NoonBox = Resources.Load("Prefabs/DunguenBox/NoonBox") as GameObject;
        _SmallFairy = Resources.Load("Prefabs/Fairy/FairyS") as GameObject;
        _BigFairy = Resources.Load("Prefabs/Fairy/FairyM") as GameObject;

    }
    void Update()
    {

     if (_MonsterList.Count == 0)
     {
            if (wormPassage != null)
            {
                wormPassage.mobCheck = true;
       
            }
            _isMonster = false;


            if (_BoxList.Count == 0)
            {
                int i = Random.Range(0, 2);
                if (i == 0)
                    SetFairy();
                else
                    SetBox();
            }

        }

            if (!_isMonster)
            {
                _DoorOpen?.Invoke();
                _isOpen = true;
             }
            else if (_isMonster)
            {
                _DoorClose?.Invoke();
            if (_isOpen)
            {
                _Audio.clip = _Clip[1];
                _Audio.Play();
                _isOpen = false;
            }
            }
    }

    //문세팅
    public void DoorSetting(Transform NowMap)
    {
        //현제맵에있는 문 딜리게이트에서 제거
        if (_NowMap != null) 
        {
            Transform _NowMapDoor = _NowMap.Find("Door");
            if (_NowMapDoor.transform.childCount != 0)
            {
                for (int i = 0; i < _NowMapDoor.transform.childCount; ++i)
                {
                    _DoorOpen -= _NowMapDoor.transform.GetChild(i).GetComponent<cDoor>().Open;
                    _DoorClose -= _NowMapDoor.transform.GetChild(i).GetComponent<cDoor>().Close;
                }
            }
        }
        //이동한 맵 문 델리게이트 추가 및 문리스트에 추가
        Transform Door = NowMap.Find("Door");
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
    public void SetNowMap(Transform NowMap)
    {

        if (NowMap != null)
        {
            DoorSetting(NowMap);
            _NowMap = NowMap;
            _MonsterList.Clear();
            _BoxList.Clear();

            for (int i = 0; i < cworldMap._WorldMapCloseList.Count; i++)
            {
                if (_NowMap.gameObject.name == cworldMap._WorldMapCloseList[i].gameObject.name)
                {
                    cworldMap._WorldMapOpenList.Add(cworldMap._WorldMapCloseList[i]);
                    cworldMap._WorldMapCloseList.Remove(cworldMap._WorldMapCloseList[i]);
                    cworldMap.Open();
                }
            }

            if (_NowMap.gameObject.CompareTag("MonsterMap"))
            {

                _Monster = _NowMap.transform.Find("Monster");
                for (int i = 0; i < _Monster.childCount; ++i)
                {
                    _MonsterList.Add(_Monster.transform.GetChild(i));

                    _Monster.transform.GetChild(i).gameObject.SetActive(true);
                }
                _isMonster = true;


                _Box = _NowMap.transform.Find("Box").gameObject;
                for (int i = 0; i < _Box.transform.childCount; ++i)
                {
                    _BoxList.Add(_Box.transform.GetChild(i).gameObject);
                }
            }
            else if (_NowMap.gameObject.CompareTag("NoMonsterMap") || _NowMap.gameObject.CompareTag("Shop") || _NowMap.gameObject.CompareTag("FoodShop"))
            {
                _Box = null;
                _Monster = null;
            }

            if (_NowMap.transform.Find("WormPassage") !=null)
            {
                wormPassage= _NowMap.transform.Find("WormPassage").GetComponent<WormPassage>();
            }
            else
            {
                wormPassage = null;
            }
        }
    }
    //박스 세팅
    void SetBox()
    {
        if (_NowMap.gameObject.CompareTag("MonsterMap"))
        {
            if (_NowMap != null)
            {
                int RandomIndex = Random.Range(1, 101);
                _Audio.clip = _Clip[0];
                if (RandomIndex >= 1 && RandomIndex <= 50)
                {
                    GameObject obj = Instantiate(_NoonBox) as GameObject;
                    obj.transform.position = _Box.transform.position;
                    obj.transform.SetParent(_Box.transform);
                    _BoxList.Add(obj);
                }
                else if (RandomIndex >= 51 && RandomIndex <= 80)
                {
                    GameObject obj = Instantiate(_NormalBox) as GameObject;
                    obj.transform.position = _Box.transform.position;
                    obj.transform.SetParent(_Box.transform);
                    _BoxList.Add(obj);
                    _Audio.Play();

                }
                else if (RandomIndex >= 81 && RandomIndex <= 95)
                {
                    GameObject obj = Instantiate(_RareBox) as GameObject;
                    obj.transform.position = _Box.transform.position;
                    obj.transform.SetParent(_Box.transform);
                    _BoxList.Add(obj);
                    _Audio.Play();

                }
                else if (RandomIndex >= 95 && RandomIndex <= 100)
                {
                    GameObject obj = Instantiate(_UniqueBox) as GameObject;
                    obj.transform.position = _Box.transform.position;
                    obj.transform.SetParent(_Box.transform);
                    _BoxList.Add(obj);
                    _Audio.Play();

                }


            }

        }

    }
    //페어리세팅
    void SetFairy()
    {
        if (_NowMap.gameObject.CompareTag("MonsterMap"))
        {
            if (_NowMap != null)
            {
                int RandomIndex = Random.Range(1, 101);
                _Audio.clip = _Clip[0];
                if (RandomIndex >= 1 && RandomIndex <= 50)
                {
                    GameObject obj = Instantiate(_NoonBox) as GameObject;
                    obj.transform.position = _Box.transform.position;
                    obj.transform.SetParent(_Box.transform);
                    _BoxList.Add(obj);
                }
                else if (RandomIndex >= 51 && RandomIndex <= 90)
                {
                    GameObject obj = Instantiate(_SmallFairy) as GameObject;

                    obj.transform.position = _Box.transform.position;
                    obj.transform.SetParent(_Box.transform);
                    _Audio.Play();
                    _BoxList.Add(obj);
                    GameObject Noon = Instantiate(_NoonBox) as GameObject;

                    Noon.transform.SetParent(_Box.transform);
                    _BoxList.Add(Noon);
                }
                else if (RandomIndex >= 91 && RandomIndex <= 100)
                {
                    GameObject obj = Instantiate(_BigFairy) as GameObject;
                    obj.transform.SetParent(_Box.transform);
                    obj.transform.position = _Box.transform.position;
                    _Audio.Play();
                    _BoxList.Add(obj);
                    //한번 갔던방에 갔다왔을떄 다시 실행안하기위해 추가하는 박스
                    GameObject Noon = Instantiate(_NoonBox) as GameObject;
                    Noon.transform.SetParent(_Box.transform);
                    _BoxList.Add(Noon);
                }
            }
        }

    }

   public void ReMoveMonster(GameObject gameObject) {
        _MonsterList.Remove(gameObject.transform);
    }
    public void ReMoveFairy(GameObject Fairy)
    {
        _BoxList.Remove(Fairy);
    }
}
