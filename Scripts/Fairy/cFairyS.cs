using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//작은페어리
public class cFairyS : MonoBehaviour
{
    AudioSource _Audio;
    AudioClip _Clip;
    private void Start()
    {
       _Audio = transform.parent.parent.parent.GetComponent<AudioSource>();
        _Clip = Resources.Load<AudioClip>("Sound/Get_Fairy");
        _Audio.clip = _Clip;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { 
                _Audio.Play();
                Player.GetInstance._health.HealHP(10, false);
                transform.parent.parent.parent.GetComponent<cMapManager>().ReMoveFairy(this.gameObject);
                Destroy(this.gameObject);
        }
    }
}
