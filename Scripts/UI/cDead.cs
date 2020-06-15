using UnityEngine;
using System.Collections;

//죽으면 버튼클릭시 마을돌아가기
public class cDead : MonoBehaviour
{

  public void GotoBilizy() {

        cSceneManager.GetInstance.ChangeScene("MainGame", null, 0);
    }
}
