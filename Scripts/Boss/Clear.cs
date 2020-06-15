using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다깨고 마을로 돌아가는 것
public class Clear: MonoBehaviour
{
    public GameObject _EndPanel;
    private void Awake()
    {
        _EndPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _EndPanel.gameObject.SetActive(true);
        }

    }
    //클리어후 마을로 복귀버튼 누르면 호출
    public void ClearButton()
    {
        cSceneManager.GetInstance.ChangeScene("MainGame", null, 0);
    }
}
