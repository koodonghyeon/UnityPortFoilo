using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//맵에 붙어있는 바운드
public class cBound : MonoBehaviour
{

    private cMapManager _Map;
    public Camera minimap;
    public GameObject cameraPosition;

    private void Awake()
    {
        _Map = transform.parent.parent.GetComponent<cMapManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            cCameramanager.GetInstance.SetBound(this.gameObject.GetComponent<BoxCollider2D>());

            _Map.SetNowMap(this.transform.parent);

            minimap.transform.position = cameraPosition.transform.position;

        }
        if (_Map._NowMap.gameObject.CompareTag("FoodShop") )
        {
           cGameManager.GetInstance.SetBackGruond(BackGroundSound.FoodShop);
        }
        if(_Map._NowMap.gameObject.CompareTag("Shop"))
        {
            cGameManager.GetInstance.SetBackGruond(BackGroundSound.Shop);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {


            Transform Map = _Map._CloseMapList.Find(x => x == transform.parent);
            if (Map)
            {
                _Map._CloseMapList.Remove(Map);
                _Map._OpenMapList.Add(Map);
            }

        }
        if (_Map._NowMap.gameObject.CompareTag("FoodShop")|| _Map._NowMap.gameObject.CompareTag("Shop"))
        {
            cGameManager.GetInstance.SetBackGruond(BackGroundSound.Dungeun);
        }
    }

}
