using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Exit버튼 단순히 끄기만 할것임
public class cFoodExit : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public void FoodTableExit()
    {
        this.gameObject.SetActive(false);
    }

}
