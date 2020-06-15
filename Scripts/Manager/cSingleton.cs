using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//싱글톤 클래스
public class cSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T _instacne = default(T);

    public static T GetInstance
    {
        get
        {
            //싱글톤이 없다. 
            if(_instacne == null)
            {
                //그러면 씬에서 찾아온다.
                _instacne = GameObject.FindObjectOfType(typeof(T)) as T;
            }
            //싱글톤이 없다.
            if(_instacne == null)
            {
                //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                var gameObject = new GameObject(typeof(T).ToString());
                _instacne = gameObject.AddComponent<T>();
                //DontDestroyOnLoad(gameObject); -> 씬으로 넘어 갈 때 나는 파괘되지 않는다.
            }
            //그리고 반환한다.
            return _instacne;
        }
    }
    //여기도 중요!
    protected virtual void Awake()
    {
        if (_instacne == null)
        {
            _instacne = Create();
            //삭제 하지 말라.
            DontDestroyOnLoad(gameObject);
        }
        else if(_instacne != null)
        {
            Destroy(this.gameObject);
        }
    }

    public static T Create()
    {
        return cSingleton<T>._instacne;
    }

}
