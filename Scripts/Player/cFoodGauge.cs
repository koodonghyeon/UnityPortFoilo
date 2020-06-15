using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//포만감
public class cFoodGauge : MonoBehaviour
{

    //포만감게이지
    private Image _Gauge;
    //포만감
    public Text _FoodText;
    //포만감 찬정도
    private float _CurrentFill;
    //최대 포만감
    private float _MaxValue = 100;
    public float MaxValue { get { return _MaxValue; } private set { _MaxValue = value; }}
    //현제 포만감
    private float _CurrentValue =0;
    public float _MyCurrentValue
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
            _FoodText.text = _CurrentValue + "  /  " + _MaxValue;
        }
    }

    private void Awake()
    {
            _Gauge = transform.GetChild(1).GetComponent<Image>();
            _FoodText.text = _CurrentValue + "  /  " + _MaxValue;
    }

    private void Update()
    {
        if (_CurrentFill != _Gauge.fillAmount)
        {
            _Gauge.fillAmount = _CurrentFill;
        }
    }

    


}
