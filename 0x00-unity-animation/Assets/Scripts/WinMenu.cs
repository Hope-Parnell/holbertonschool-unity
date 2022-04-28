using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public string nextScene;
    public void MainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Next(){
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }
}
