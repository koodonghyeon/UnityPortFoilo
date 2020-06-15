using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//타이틀선택씬
public class cSelect : MonoBehaviour
{
    public cOptionPanel _Panel;

    public void OnButtonDown(Button Sander)
    {
        if (Sander.name == "Play")
        {
     
           cSceneManager.GetInstance.ChangeScene("MainGame",null,0);
        }
        else if (Sander.name == "Option")
        {
            _Panel.gameObject.SetActive(true);
        
        }
        else if (Sander.name == "Exit")
        {
            Debug.Log("Exit");
        }
    }
}
