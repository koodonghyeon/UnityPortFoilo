using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//타이틀 백그라운드이동용
public class BackGround : MonoBehaviour
{
    //속도
    public float Speed;
    private Renderer m_Renderer=null;
    void Start()
    {

        m_Renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time * Speed, 0);
        m_Renderer.material.mainTextureOffset = textureOffset;
    }
}
