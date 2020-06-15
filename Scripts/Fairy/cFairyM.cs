using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//커다란페어리
public class cFairyM : MonoBehaviour
{
    AudioSource _Audio;
    AudioClip _Clip;
    
  
    private void Start()
    {
        _Audio = transform.parent.parent.parent.GetComponent<AudioSource>();
        _Clip = Resources.Load<AudioClip>("Sound/Get_Fairy");
        _Audio.clip = _Clip;
    }
    
    //플레이어랑 충돌시 회복
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           _Audio.Play();
            transform.parent.parent.parent.GetComponent<cMapManager>().ReMoveFairy(this.gameObject);
            Player.GetInstance._health.HealHP(20, false);
            Destroy(this.gameObject);
         
        }
    }
}
