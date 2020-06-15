using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//음식클래스
public class cFood
{
    //음식이름
    public string _FoodName { get; }
    //음식1 스텟효과
    public string _FoodEffect1 { get; }
    //음식 스텟1
    public float _FoodStat1 { get; }
    //음식2 스텟효과
    public string _FoodEffect2 { get; }
    //음식 스텟 2;
    public float _FoodStat2 { get; }
    //배부름 게이지
    public int _Satiety { get; }
    //음식가격
    public int _FoodPrice { get; }
    //회복될 HP
    public int _HP { get; }
    //음식 이름
    public int _FoodID { get; }
    //음식 이미지
    public Sprite _FoodIcon { get; }
    public cFood(string Name,string Effect1,float Stat1,string Effect2,float Stat2, int Satiey,int Price, int HP,Sprite Icon,int ID) 
    {
        _FoodName = Name;
        _FoodEffect1 = Effect1;
        _FoodStat1 = Stat1;
        _FoodEffect2 = Effect2;
        _FoodStat2 = Stat2;
        _Satiety = Satiey;
        _FoodPrice = Price;
        _HP = HP;
        _FoodIcon = Icon;
        _FoodID = ID;
    }


}
