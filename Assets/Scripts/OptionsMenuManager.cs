using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour {
    private GameObject snd;
    public Scrollbar scBarMusic;
    public Scrollbar scBarFX;
    public Toggle togMusic;
    public Toggle togFX;


    void Start() {
        snd = GameObject.FindGameObjectWithTag("SoundManager");
        togMusic.isOn = snd.GetComponent<SoundManager>().isMusicEnabled;
        togFX.isOn = snd.GetComponent<SoundManager>().isFXEnabled;
        scBarMusic.value = snd.GetComponent<SoundManager>().musicVolume;
        scBarFX.value = snd.GetComponent<SoundManager>().fxVolume;

        scBarFX.onValueChanged.AddListener(ScrollbarCallBack);
    }
    
    void ScrollbarCallBack(float f) {
        if (f > 0)
            snd.GetComponent<SoundManager>().playFX(0);
    }

    public void setVolumeMusic() {
        //snd.GetComponent<SoundManager>().musicVolume = scBarMusic.value;
        snd.GetComponent<SoundManager>().setMusicVolume(scBarMusic.value);
        PlayerPrefs.SetFloat("musicVolume", scBarMusic.value);
    }

    public void setVolumeFX() {
        //snd.GetComponent<SoundManager>().fxVolume = scBarFX.value;
        snd.GetComponent<SoundManager>().setFXVolume(scBarFX.value);
        PlayerPrefs.SetFloat("fxVolume", scBarFX.value);
    }

    public void enableMusic() {
        //snd.GetComponent<SoundManager>().isMusicEnabled = togMusic.isOn;
        snd.GetComponent<SoundManager>().setEnableMusic(togMusic.isOn);
        PlayerPrefs.SetInt("music", togMusic.isOn==true?1:0);
    }

    public void enableFX(){
        //snd.GetComponent<SoundManager>().isFXEnabled = togFX.isOn;
        snd.GetComponent<SoundManager>().setEnableFX(togFX.isOn);
        PlayerPrefs.SetInt("fx", togFX.isOn==true?1:0);
    }

}
