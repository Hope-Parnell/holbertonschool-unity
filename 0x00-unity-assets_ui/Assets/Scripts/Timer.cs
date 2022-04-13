using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time - startTime;
        string minutes = ((int) time / 60).ToString();
        string seconds = (time % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
    }
}
