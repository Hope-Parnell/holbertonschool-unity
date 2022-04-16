using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    public GameObject pauseMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            if(paused)
                Pause();
            else
                Resume();
        }
    }
    public void Pause(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void Resume(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        paused = false;
    }
    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Options(){
        Time.timeScale = 1;
        PlayerPrefs.SetString("Prev", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }
}
