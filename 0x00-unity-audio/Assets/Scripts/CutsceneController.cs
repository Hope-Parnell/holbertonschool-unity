using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject timerCanvas;
    public GameObject mainCamera;
    private GameObject cutsceneCamera;
    // Start is called before the first frame update
    void Start()
    {
        cutsceneCamera = GameObject.Find("CutsceneCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1){
            timerCanvas.SetActive(true);
            mainCamera.SetActive(true);
            cutsceneCamera.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
        }

    }
}
