using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public GameObject visualsCredits;
    public GameObject audioCredits;
    public void VisualsCredits(){
        audioCredits.SetActive(false);
        visualsCredits.SetActive(true);
    }
    public void AudioCredits(){
        visualsCredits.SetActive(false);
        audioCredits.SetActive(true);

    }
    public void OpenLink(string link){
        Application.OpenURL(link);
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit(){
        Debug.Log("Exited");
        Application.Quit();
    }
}
