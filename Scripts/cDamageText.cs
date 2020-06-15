using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//데미지 텍스트
public class cDamageText : MonoBehaviour
{
    float _MoveSpeed=1;
    float _DestroyTime =2;
    TextMeshPro _Text;
    Color _Alpha;
    float _AlphaSpeed=1;
    int _Value;


    void Awake()
    {
        _Text = GetComponent<TextMeshPro>();
    
        _Alpha = _Text.color;
        Invoke("DestroyObject", _DestroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, _MoveSpeed * Time.deltaTime, 0));
        _Alpha.a = Mathf.Lerp(_Alpha.a, 0, Time.deltaTime * _AlphaSpeed);
        _Text.color = _Alpha;
 
     }  
    public void SetDamage(int dam,bool isCritical)
    {
        _Value = dam;
        if (isCritical)
        {
            _Text.faceColor = Color.yellow;
           
        }
        else if (!isCritical)
        {
            _Text.faceColor = Color.white;
      
        }
        _Text.text = _Value.ToString();
    }
    public void SetGold(int Gold)
    {
        _Value = Gold;
        _Text.faceColor = Color.yellow;
        _Text.text = _Value.ToString() + "G";
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}
