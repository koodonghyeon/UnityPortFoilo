using UnityEngine;
using System.Collections;

//아이템 드랍노드
public class ItemDrop : cNPC
{
    AudioSource _Audio;
    AudioClip _Clip;
    //현재 아이템
    public Item _item;



    private Rigidbody2D _rigid;

    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody2D>();
        _Audio = Player.GetInstance.GetComponent<AudioSource>();
        _Clip = Resources.Load<AudioClip>("Sound/GetItem");
     
        _rigid.velocity = new Vector2( 0 ,5.0f);
    }


    private void Update()
    {
        if (_isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _Audio.clip = _Clip;
                _Audio.Play();
                cInventory.GetInstance.AddItem(_item);
                Destroy(this.gameObject);
            }
        }

    }
    //아이템 셋팅
    public void SetItem(Item item)
    {
        _item = item;
    }
    //플레이어 충돌여부 및 F버튼 활성화 여부체크
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
