using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused;
            if(isPaused)
                Pause();
            else
                Resume();
        }
    }
    public void Pause(){
        Time.timeScale = 0;
        paused.TransitionTo(0.01f);
        pauseMenu.SetActive(true);
    }
    public void Resume(){
        Time.timeScale = 1;
        unpaused.TransitionTo(0.01f);
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    public void Restart(){
        Time.timeScale = 1;
        unpaused.TransitionTo(0.01f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu(){
        Time.timeScale = 1;
        unpaused.TransitionTo(0.01f);
        SceneManager.LoadScene("MainMenu");
    }
    public void Options(){
        Time.timeScale = 1;
        PlayerPrefs.SetString("Prev", SceneManager.GetActiveScene().name);
        unpaused.TransitionTo(0.01f);
        SceneManager.LoadScene("Options");
    }
}
