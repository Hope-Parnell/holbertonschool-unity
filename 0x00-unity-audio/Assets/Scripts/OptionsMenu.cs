using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertY;
    public Toggle freeCam;
    public AudioMixer mixer;
    public Slider BGMSlider;
    public Slider SFXSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("yInvert")){
            if (PlayerPrefs.GetInt("yInvert") == -1)
                invertY.isOn = true;
            else
                invertY.isOn = false;
        }
        else{
            PlayerPrefs.SetInt("yInvert", 1);
        }
        if (PlayerPrefs.HasKey("freeCam")){
            if (PlayerPrefs.GetInt("freeCam") == 1)
                freeCam.isOn = true;
            else
                freeCam.isOn = false;
        }
        else{
            PlayerPrefs.SetInt("freeCam", 0);
        }
        BGMSlider.value = PlayerPrefs.GetFloat("BGM", 1f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
    }

    // Update is called once per frame
    public void Back(){
        SetVolume();
        SceneManager.LoadScene(PlayerPrefs.GetString("Prev"));
    }
    public void Apply(){
        if (invertY.isOn)
            PlayerPrefs.SetInt("yInvert", -1);
        else
            PlayerPrefs.SetInt("yInvert", 1);
        if (freeCam.isOn)
            PlayerPrefs.SetInt("freeCam", 1);
        else
            PlayerPrefs.SetInt("freeCam", 0);
        PlayerPrefs.SetFloat("BGM", BGMSlider.value);
        PlayerPrefs.SetFloat("BGM", BGMSlider.value);
        SetVolume();
        SceneManager.LoadScene(PlayerPrefs.GetString("Prev"));
    }
    public void ChangeBGM(){
        float volume = LinearToDecibel(BGMSlider.value);
        mixer.SetFloat("BGMVolume", volume);
    }
    public void ChangeSFX(){
        float volume = LinearToDecibel(SFXSlider.value);
        mixer.SetFloat("SFXVolume", volume);
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
    private void SetVolume(){
        mixer.SetFloat("BGMVolume", LinearToDecibel(PlayerPrefs.GetFloat("BGM", 1f)));
        mixer.SetFloat("SFXVolume", LinearToDecibel(PlayerPrefs.GetFloat("SFX", 1f)));
    }
}
