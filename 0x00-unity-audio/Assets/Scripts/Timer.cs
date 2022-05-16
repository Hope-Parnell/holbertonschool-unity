using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text winTime;
    public float startTime;
    public GameObject winCanvas;
    void Update()
    {
        float time = Time.time - startTime;
        string minutes = ((int) time / 60).ToString();
        string seconds = (time % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
    }
    public void Win(){
        winTime.text = timerText.text;
        winCanvas.gameObject.SetActive(true);
    }
}
