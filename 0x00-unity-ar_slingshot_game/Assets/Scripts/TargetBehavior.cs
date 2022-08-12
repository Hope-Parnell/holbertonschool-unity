using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetBehavior : MonoBehaviour
{
    [SerializeField] GameObject arOrigin;
    public ARPlane selectedPlane;
    private void Awake() {
        selectedPlane = arOrigin.GetComponent<PlaneSelection>().selectedPlane;
    }
}
