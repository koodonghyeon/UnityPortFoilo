using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//음식점 포만감
public class cFoodTableGauge : MonoBehaviour
{
    //포만감바
    private Image _Gauge;
    //포만감수치
    public Text _FoodText;
    
    //HP바 수치
    private float currentFill;

    private void Start()
    {
        _Gauge = GetComponent<Image>();
        currentFill = Player.GetInstance._Food._MyCurrentValue / Player.GetInstance._Food.MaxValue;
        _FoodText.text = Player.GetInstance._Food._MyCurrentValue + " / " + Player.GetInstance._Food.MaxValue;

        
    }
    private void Update()
    {
        currentFill = Player.GetInstance._Food._MyCurrentValue / Player.GetInstance._Food.MaxValue;
        if (currentFill != _Gauge.fillAmount)
        {
            _Gauge.fillAmount = currentFill;
            _FoodText.text = Player.GetInstance._Food._MyCurrentValue + " / " + Player.GetInstance._Food.MaxValue;
        }
    }

   


}
