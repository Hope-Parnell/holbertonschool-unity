using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public GameObject teleportLocation;
    public GameObject player;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleport");
        StartCoroutine("Teleport");
    }
    IEnumerator Teleport()
    {
        player.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        player.transform.position = teleportLocation.transform.position;
        animator.SetBool("isFalling", true);
        yield return new WaitForSeconds(0.05f);
        player.GetComponent<PlayerController>().enabled = true;
    }
}
