using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cRareBox : cNPC
{
    int _ItemCount=0;
    SpriteRenderer _Renderer;

    bool _isOpen = false;
    protected override void Awake()
    {

        base.Awake();

        _Renderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (_isPlayer)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                ItemDrop();
            }
    
        } 
    }
     void ItemDrop()
    {
        if (_ItemCount == 0)
        {
            int _RandNum = Random.Range(3, 6);         // 레어등급 바꾸기
            if (_RandNum >= 3 && _RandNum < 6)
            {
                GameObject _DropNode = null;
                _DropNode = (GameObject)Instantiate(Resources.Load("Prefabs/Item/DropNode"));
                _DropNode.transform.position = this.transform.position;

                Item _ItemValue = cDataBaseManager.GetInstance._ItemList[_RandNum];
                ItemDrop a_RefItemInfo = _DropNode.GetComponent<ItemDrop>();
                if (a_RefItemInfo != null)
                {

                    a_RefItemInfo.SetItem(_ItemValue);
                    if (_ItemValue._Type == ItemType.Spear || _ItemValue._Type == ItemType.Sword)
                    {
                        a_RefItemInfo.transform.Rotate(new Vector3(0, 0, 90));
                    }
                    else
                    {
                        a_RefItemInfo._ButtonF.transform.rotation = Quaternion.identity;
                    }
                }
                // 동적으로 텍스쳐 이미지 바꾸기
                SpriteRenderer a_RefRender = _DropNode.GetComponent<SpriteRenderer>();
                a_RefRender.sprite = _ItemValue._ItemIcon;

            }
        }
        _ItemCount += 1;

        _isOpen = true;
        _ButtonF.SetActive(false);
        _Renderer.sprite = Resources.Load<Sprite>("Itemp/EleteTresure1Opened");
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isOpen)
        base.OnTriggerEnter2D(collision);

    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
