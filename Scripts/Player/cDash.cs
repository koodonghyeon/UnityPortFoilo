using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//대쉬 슬롯
public class cDash :MonoBehaviour
{
   
    //대쉬 슬롯 이미지 배열
    private GameObject[] _DashSlot =new GameObject[3];
   

    private void Start()
    {
        for(int i=0; i < 3; ++i)
        {
            _DashSlot[i]=transform.GetChild(i).gameObject;
            _DashSlot[i].SetActive(true);
        }
    }
    //활성화
    public void SetEnabled(int DashCount)
    {
        if (DashCount < 3 && DashCount > -1)
        {
            _DashSlot[DashCount].SetActive(true);
        }

    }
    //비활성화
    public void SetEnabledfasle(int DashCount)
    {
        if (DashCount < 3&& DashCount>-1)
        {
            _DashSlot[DashCount].SetActive(false);
        }
    }
    //전부 활성화
  public void AllTrue()
    {
        for (int i = 0; i < 3; ++i)
        {
 
            _DashSlot[i].SetActive(true);
        }
    }
}
