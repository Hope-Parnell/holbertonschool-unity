using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public GameObject teleportLocation;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleport");
        StartCoroutine("Teleport");
    }
    IEnumerator Teleport()
    {
        player.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        player.transform.position = teleportLocation.transform.position;
        yield return new WaitForSeconds(0.01f);
        player.GetComponent<PlayerController>().enabled = true;
    }
}
