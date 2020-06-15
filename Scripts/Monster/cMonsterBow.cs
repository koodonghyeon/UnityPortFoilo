using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonsterBow : MonoBehaviour
{


    AudioSource _Audio;
    List<AudioClip> _Clip = new List<AudioClip>();
    void Awake()
    {
 
      

        _Audio = transform.parent.GetComponent<AudioSource>();
        _Clip.Add(Resources.Load<AudioClip>("Sound/bow_crossbow_arrow_draw_stretch1_03"));
        _Clip.Add(Resources.Load<AudioClip>("Sound/bow_crossbow_arrow_shoot_type1_03"));

    }
    public void AnimationEvent()
    {
        _Audio.clip = _Clip[0];
        _Audio.Play();
        Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        FireBulet(angle);
    }



    public void FireBulet(float _angle)
    {

        cBullet Bullet = cMonsterBullet.GetInstance.GetObject(2);
        Bullet.transform.position = this.transform.position;

        Bullet.transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        Bullet._Start = true;
        Bullet.gameObject.SetActive(true);
        StartCoroutine("ActiveBullet", Bullet);

    }

    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }
}
