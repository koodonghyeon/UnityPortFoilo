using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//발판
public class cFootBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Brige" || collision.gameObject.tag == "floor")
        {

             Player.GetInstance._JumpCount = 2;
            Player.GetInstance._Ani.SetBool("Jump", false);

        }
    }
}
