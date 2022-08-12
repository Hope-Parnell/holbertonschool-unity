using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class PlaneSelection : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Vector2 touchPos;
    private TrackableId selectionId;
    private ARPlane selectedPlane = null;
    [SerializeField] Material selected;
    [SerializeField] Material unselected;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject playAgainButton;
    [SerializeField] private TMP_Text hint;
    private void Start() {
        hint.text = "Please select your play surface.";
    }
    private void Awake() {
        planeManager = FindObjectOfType<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
        hint.text = "Please select your play surface.";
   }
    // Update is called once per frame
    private void Update() {
        if(planeManager.enabled){
            if(!getTouchPosition(out Vector2 touchPos)){
                return;
            }
            if(raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon)){
                if(selectedPlane != null)
                    selectedPlane.GetComponent<MeshRenderer>().material = unselected;
                selectionId = hits[0].trackableId;
                selectedPlane = planeManager.GetPlane(selectionId);
                selectedPlane.GetComponent<MeshRenderer>().material = selected;
            }
        }
    }
    public void confirmPlane(){
        foreach (var plane in planeManager.trackables)
        {
            if (plane.trackableId != selectionId)
                Destroy(plane.gameObject);
        }
        planeManager.enabled = false;
        raycastManager.enabled = false;
        confirmButton.SetActive(false);
        inGameMenu.SetActive(true);
        hint.text = "Press Start to Play";
        startButton.SetActive(true);
    }
    public void playGame(){
        startButton.SetActive(false);
        inGameMenu.SetActive(true);
        hint.text = "Pull back to Aim";
        spawnTargets();
    }
    public void gameRestart(){
        SceneManager.LoadScene(0);
    }
    public void quit(){
        Application.Quit();
    }
    bool getTouchPosition(out Vector2 touchPos){
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch((0)).position;
            return true;
        }
        touchPos = default;
        return false;
    }
    void spawnTargets(){

    }
}
