using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//HP
public class cHP : MonoBehaviour
{
    //HP바
    private Image _Gauge;
    //HP수치
    public Text _HPText;
    //HP량
    private float _CurrentFill;
    //최대체력
    public float _MaxValue { get; set; }
    //현재체력
    private float _CurrentValue;
    //현재체력
    public float MyCurrentValue
    {
        get
        {
            return _CurrentValue;
        }

        set
        {
            if (value > _MaxValue) _CurrentValue = _MaxValue;
            else if (value < 0) _CurrentValue = 0;
            else _CurrentValue = value;

            _CurrentFill = _CurrentValue / _MaxValue;
            _HPText.text = (int)_CurrentValue + "  /  " + (int)_MaxValue;
        }
    }

    private void Awake()
    {
       
            _Gauge = GetComponentInChildren<Image>();
       
    }

    private void Update()
    {
        if(_CurrentFill != _Gauge.fillAmount)
        {
            _Gauge.fillAmount = _CurrentFill;
        }
    }

    //체력초기화
    public void Initialize(float currentValue, float maxValue)
    {
        _MaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
    //최력회복
    public void HealHP(float HP,bool MaxHP)
    {
        if (!MaxHP)
        {
            MyCurrentValue += HP;
        }
        else if (MaxHP)
        {
            _MaxValue += HP;
            MyCurrentValue += HP;
        }
    }
    
    
}
