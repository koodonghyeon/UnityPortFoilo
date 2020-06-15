using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//음식점 HP
public class cFoodHP : MonoBehaviour
{
    //HP바
    private Image _Gauge;
    //HP수치
    public Text _HPText;
    
    //HP바 수치
    private float _CurrentFill;

    private void Start()
    {
        _Gauge = GetComponent<Image>();
        _CurrentFill = Player.GetInstance._health.MyCurrentValue / Player.GetInstance._health._MaxValue;
        _HPText.text = Player.GetInstance._health.MyCurrentValue + " / " + Player.GetInstance._health._MaxValue;

        
    }
    private void Update()
    {
        _CurrentFill = Player.GetInstance._health.MyCurrentValue / Player.GetInstance._health._MaxValue;
        if (_CurrentFill != _Gauge.fillAmount)
        {
            _Gauge.fillAmount = _CurrentFill;
            _HPText.text = Player.GetInstance._health.MyCurrentValue + " / " + Player.GetInstance._health._MaxValue;
        }
    }
}
