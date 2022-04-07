using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private GameObject sndManager;

    void Start()  {
        //SOUNDMANAGER
        sndManager = GameObject.FindGameObjectWithTag("SoundManager");
        initSoundManager();
        sndManager.GetComponent<SoundManager>().playMusic(0);

        
    } 

    void initSoundManager() {
        //sndManager.GetComponent<SoundManager>().musicVolume = PlayerPrefs.GetFloat("musicVolume");
        sndManager.GetComponent<SoundManager>().setMusicVolume(PlayerPrefs.GetFloat("musicVolume"));

        sndManager.GetComponent<SoundManager>().fxVolume = PlayerPrefs.GetFloat("fxVolume");
        //sndManager.GetComponent<SoundManager>().setFXVolume(PlayerPrefs.GetFloat("fxVolume"));

        //sndManager.GetComponent<SoundManager>().isMusicEnabled = PlayerPrefs.GetInt("music")==1?true:false;
        sndManager.GetComponent<SoundManager>().setEnableMusic(PlayerPrefs.GetInt("music")==1?true:false);

        sndManager.GetComponent<SoundManager>().isFXEnabled = PlayerPrefs.GetInt("fx")==1?true:false;
        //sndManager.GetComponent<SoundManager>().setEnableFX(PlayerPrefs.GetInt("fx")==1?true:false);
    }
}
