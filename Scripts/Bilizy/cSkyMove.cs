using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//뒷배경 움직이기
public class cSkyMove : MonoBehaviour
{
    public Transform[] m_BackGroundFront = new Transform[3];
    readonly uint BACKGROUND_SIZE = 3;
    private Vector3 m_vBackGroind1Finsh = new Vector3(-48.13f, 0, 0);
    [SerializeField] private float BackGround1Speed = 5.0f;
    private Transform m_Target = null;
    void Start()
    {
        m_Target = m_BackGroundFront[0];
    }
    void Update()
    {
        for (int i = 0; i < BACKGROUND_SIZE; ++i)
        {
            m_BackGroundFront[i].position -= new Vector3(Time.deltaTime * BackGround1Speed, 0, 0);
        }
        if (m_Target.position.Compare(ref m_vBackGroind1Finsh))
        {

            m_Target.position = m_BackGroundFront[BACKGROUND_SIZE - 1].position + new Vector3(48.0f, 0, 0);

            for (int i = 0; i < BACKGROUND_SIZE - 1; ++i)
            {
                m_Target.Swap(ref m_BackGroundFront[i], ref m_BackGroundFront[i + 1]);
            }
            m_Target = m_BackGroundFront[0];
        }
    }
}
