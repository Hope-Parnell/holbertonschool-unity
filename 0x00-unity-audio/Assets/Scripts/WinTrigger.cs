using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text TimerText;
    public AudioSource BGM;
    private void OnTriggerEnter(Collider other){
        BGM.Stop();
        var pc =other.GetComponent<PlayerController>();
        if (pc.rockFootsteps.isPlaying)
            pc.rockFootsteps.Stop();
        if (pc.grassFootsteps.isPlaying)
            pc.grassFootsteps.Stop();
        pc.enabled = false;
        other.GetComponent<Timer>().Win();
        other.GetComponent<Timer>().enabled = false;
        TimerText.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
