using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//음식점 골드
public class cTableGold : MonoBehaviour
{
    //레스토랑골드
    Text FoodTableGold;
    private void Awake()
    {
        FoodTableGold = GetComponent<Text>();
        cGameManager.GetInstance._DeleGateGold += SetGold;
        SetGold();
    }

    private void SetGold()
    {
        FoodTableGold.text = cGameManager.GetInstance.Gold.ToString();
    }

}
