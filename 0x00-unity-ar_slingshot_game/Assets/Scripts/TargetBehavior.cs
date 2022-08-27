using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetBehavior : MonoBehaviour
{
    private GameObject arOrigin;
    public ARPlane selectedPlane;
    private void Awake() {
        arOrigin = GameObject.Find("AR Session Origin");
        selectedPlane = arOrigin.GetComponent<PlaneSelection>().selectedPlane;
    }
}
