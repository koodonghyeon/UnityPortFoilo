using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어랑 충돌시 씬전환하는녀석
public class cGameStart : MonoBehaviour
{
    public GameObject _DungeonEat;
    private Animator _DunGeonAnim;

    private void Awake()
    {
        _DunGeonAnim = _DungeonEat.GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            _DungeonEat.transform.position = new Vector3(Player.GetInstance.transform.position.x, _DungeonEat.transform.position.y, 0);
            _DunGeonAnim.SetTrigger("Open");

            cUIManager.GetInstance.gameObject.SetActive(false);
           
            Player.GetInstance._Ani.StopPlayback();
            Player.GetInstance._isMoveMap = true;
            Player.GetInstance._Rigidbody.velocity = Vector3.zero;


        }
    }

}

