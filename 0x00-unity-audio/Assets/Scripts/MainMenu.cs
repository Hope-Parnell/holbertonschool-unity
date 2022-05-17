using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        //Set volume upon entering
        mixer.SetFloat("BGMVolume", LinearToDecibel(PlayerPrefs.GetFloat("BGM", 1f)));
        mixer.SetFloat("SFXVolume", LinearToDecibel(PlayerPrefs.GetFloat("SFX", 1f)));
    }
    public void LevelSelect(int level){
        string scene = "Level0" + level.ToString();
        PlayerPrefs.SetString("Prev", scene);
        SceneManager.LoadScene(scene);
    }
    public void Options(){
        PlayerPrefs.SetString("Prev", "MainMenu");
        SceneManager.LoadScene("Options");
    }
    public void Exit(){
        Debug.Log("Exited");
        Application.Quit();
    }
    public void Credits(){
        SceneManager.LoadScene("Credits");
    }
    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
        {
            dB = 20.0f * Mathf.Log10(linear);
        }
        else
        {
            dB = -144.0f;
        }

        return dB;
    }
}
