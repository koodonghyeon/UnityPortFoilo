using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//아이템 음식등 데이터를 관리하는 메니저
public class cDataBaseManager : cSingleton<cDataBaseManager>
{

    Item item;
    //아이템 리스트
    public  List<Item> _ItemList = new List<Item>();
    //음식 리스트
    public  List<cFood> _FoodList = new List<cFood>();
    protected override void Awake()
    {
        base.Awake();
        //노말
        _ItemList.Add(this.gameObject.AddComponent<cShortSward>());
        _ItemList.Add(this.gameObject.AddComponent<cSniper>());
        _ItemList.Add(this.gameObject.AddComponent<Berdysh>());

        //레어       
        _ItemList.Add(this.gameObject.AddComponent<Gladius>());
        _ItemList.Add(this.gameObject.AddComponent<Halberd>());
        _ItemList.Add(this.gameObject.AddComponent<cMT8>());
        //유니크
        _ItemList.Add(this.gameObject.AddComponent<LightSaver>());
        _ItemList.Add(this.gameObject.AddComponent<Gwendolyn>());
        _ItemList.Add(this.gameObject.AddComponent<Kar98>());
        _ItemList.Add(this.gameObject.AddComponent<cAK47>());
     
        //음식 
        _FoodList.Add(new cFood("계란후라이", "위력", 10.0f, "최대 체력", 8, 60, 450, 6, Resources.Load<Sprite>("UI/food/02_FriedEgg"),1));
        _FoodList.Add(new cFood("디럭스 버거", "위력", 5.0f, "방어력", 2, 55, 340, 8, Resources.Load<Sprite>("UI/food/09_DeluxeBurger"),2));
        _FoodList.Add(new cFood("매운 소스 미트볼", "위력", 20.0f, " ", 0, 40, 500, 10, Resources.Load<Sprite>("UI/food/98_HotMeatball"),3));
        _FoodList.Add(new cFood("초콜릿 쿠키", "최대 체력", 8.0f, "방어력", 1, 43, 400, 4, Resources.Load<Sprite>("UI/food/10_ChocolateCookie"),4));
        _FoodList.Add(new cFood("야채 살사 수프", "방어력", 3f, " ", 0, 60, 550, 6, Resources.Load<Sprite>("UI/food/07_VegetableSalsaSoup"),5));
        _FoodList.Add(new cFood("양파 수프", "최대 체력", 20.0f, " ", 0, 70, 520, 10, Resources.Load<Sprite>("UI/food/05_OnionSoup"),6));
        _FoodList.Add(new cFood("구운 버섯", "크리티컬데미지", 20.0f, " ", 0, 65, 700, 10, Resources.Load<Sprite>("UI/food/03_GrilledMushroom"),7));
        _FoodList.Add(new cFood("딸기파이", "크리티컬데미지", 10.0f, "위력", 10, 70, 1000, 8, Resources.Load<Sprite>("UI/food/18_StrawberryPie"),8));
        _FoodList.Add(new cFood("탄산수", "포만감 30감소", 0f, " ", 0, 35, 1500, 2, Resources.Load<Sprite>("UI/food/26_SparklingWater"),9));

    }
  
}
