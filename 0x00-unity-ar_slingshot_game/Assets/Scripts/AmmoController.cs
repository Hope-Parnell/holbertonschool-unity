using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoController : MonoBehaviour
{
    private GameObject arOrigin;
    public List<GameObject> targets;
    private void Awake() {
        arOrigin = GameObject.Find("AR Session Origin");
        targets = arOrigin.GetComponent<PlaneSelection>().targets;

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target"){
            targets.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            var scoreKeeper = GameObject.Find("InGameMenu");
            scoreKeeper.GetComponent<ScoreKeeper>().Score();
        }
    }
}
