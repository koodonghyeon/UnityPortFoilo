using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSmallGold : MonoBehaviour
{
    //자기자신의 골드
   int _Gold;
    public GameObject _GoldText;

    private void Start()
    {
        _Gold = 10;
 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
             GameObject Dam = Instantiate(_GoldText);
                Dam.transform.position = Player.GetInstance.transform.position;
                Dam.GetComponent<cDamageText>().SetGold(_Gold);
                cGameManager.GetInstance.Gold += _Gold;
                cGameManager.GetInstance._DeleGateGold();
            Destroy(this.gameObject);
        }
    }
}
