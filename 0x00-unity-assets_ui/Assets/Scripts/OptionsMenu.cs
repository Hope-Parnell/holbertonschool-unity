using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertY;
    public Toggle freeCam;
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

    }

    // Update is called once per frame
    public void Back(){
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
        SceneManager.LoadScene(PlayerPrefs.GetString("Prev"));
    }
}
