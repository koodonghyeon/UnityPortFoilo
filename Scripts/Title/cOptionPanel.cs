using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//설정창
public class cOptionPanel: MonoBehaviour
{
    //백그라운드 설정용 슬라이더
    public Slider _BackGround;
    //이펙트용 슬라이더
    public Slider _EffectSound;
    //오디오 믹서
    public AudioMixer _Mixer;
    //백그라운드 볼륨
    private float _BackVol;
    //이펙트볼륨
    private float _EffectVol;
    private float _PreviousBack;
    private float _PreviousEffect;

    //백그라운드 사운드 설정
    public void BackGroundControll()
    {
        _BackVol = _BackGround.value;
        if (_BackVol == -40f) _Mixer.SetFloat("BackGround", -80);
        else _Mixer.SetFloat("BackGround", _BackVol);

    }
    //이펙트 사운드 설정
    public void EffectControll()
    {
        _EffectVol = _EffectSound.value;

        if (_EffectVol == -40f) _Mixer.SetFloat("Effect", -80);
        else _Mixer.SetFloat("Effect", _EffectVol);

    }
    //버튼클릭시 설정저장
    public void OnButtonDown(Button btn)
    {

        if (btn.name == "Exit")
        {
            setConTroll(false);
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if(btn.name == "Complete")
        {
            setConTroll(true);
        }
    }

    void setConTroll(bool isSeting)
    {
        if (isSeting)
        {
            _BackVol = _BackGround.value;
            _PreviousBack= _BackVol;
            if (_BackVol == -40f) _Mixer.SetFloat("BackGround", -80);
            else _Mixer.SetFloat("BackGround", _BackVol);

            _EffectVol = _EffectSound.value;
            _PreviousEffect= _EffectVol;
            if (_EffectVol == -40f) _Mixer.SetFloat("Effect", -80);
            else _Mixer.SetFloat("Effect", _EffectVol);
        }
        else if(!isSeting)
        {
            _BackGround.value = _PreviousBack;
            _EffectSound.value = _PreviousEffect;
            if (_PreviousBack == -40f) _Mixer.SetFloat("BackGround", -80);
            else _Mixer.SetFloat("BackGround", _PreviousBack);

            if (_PreviousEffect == -40f) _Mixer.SetFloat("Effect", -80);
            else _Mixer.SetFloat("Effect", _PreviousEffect);
        }
    }
}
