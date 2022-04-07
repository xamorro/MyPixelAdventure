using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private GameObject sndManager;
    private bool isGameCompleted = false;

    void Start()  {
        //SOUNDMANAGER
        sndManager = GameObject.FindGameObjectWithTag("SoundManager");
        initSoundManager();
        sndManager.GetComponent<SoundManager>().playMusic(0);

        
    } 

    void initSoundManager() {
        sndManager.GetComponent<SoundManager>().setMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        sndManager.GetComponent<SoundManager>().fxVolume = PlayerPrefs.GetFloat("fxVolume");
        sndManager.GetComponent<SoundManager>().setEnableMusic(PlayerPrefs.GetInt("music")==1?true:false);
        sndManager.GetComponent<SoundManager>().isFXEnabled = PlayerPrefs.GetInt("fx")==1?true:false;
    }

    void OnGUI() {
        if (isGameCompleted) {
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 30;
            if ( GUI.Button(new Rect(Screen.width/2-Screen.width/16, Screen.height/2-Screen.height/16, Screen.width/8, Screen.height/8), "REJUGAR", myButtonStyle)) {
                restartGame();
                SceneManager.LoadScene(0);
            }
        }
    }

    void restartGame() {
        isGameCompleted = false;
        //textWinner.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
