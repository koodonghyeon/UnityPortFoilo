using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cStat : MonoBehaviour
{
    Text _Damage;
    Text _CriticalDamage;
    Text _JumpPower;
    Text _Defence;
    Text _MoveSpeed;
    Text _Critical;
    void Awake()
    {
       _Damage =transform.GetChild(6).GetComponent<Text>();
       _CriticalDamage = transform.GetChild(7).GetComponent<Text>();
       _JumpPower = transform.GetChild(8).GetComponent<Text>();
       _Defence = transform.GetChild(9).GetComponent<Text>();
       _MoveSpeed = transform.GetChild(10).GetComponent<Text>();
        _Critical = transform.GetChild(11).GetComponent<Text>();
        this.gameObject.SetActive(false);
    }

    public void SetStat()
    {
        _Damage.text = Player.GetInstance._Power.ToString();
        _CriticalDamage.text = Player.GetInstance._CriticalDamage.ToString();
        _JumpPower.text = Player.GetInstance._JumpPower.ToString();
        _Defence.text = Player.GetInstance._Defense.ToString();
        _MoveSpeed.text = Player.GetInstance._MoveSpeed.ToString();
        _Critical.text = Player.GetInstance._Critical.ToString();
    }
}
