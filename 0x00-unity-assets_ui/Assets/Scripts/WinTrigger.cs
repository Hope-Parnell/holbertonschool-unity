using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text TimerText;
    private void OnTriggerEnter(Collider other){
        other.GetComponent<Timer>().Win();
        other.GetComponent<Timer>().enabled = false;
        TimerText.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
