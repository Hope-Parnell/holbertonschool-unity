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
    public ARPlane selectedPlane = null;
    public List<GameObject> targets;
    [SerializeField] Material selected;
    [SerializeField] Material unselected;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject playAgainButton;
    public TMP_Text hint;
    [SerializeField] GameObject targetModel;
    [SerializeField] int maxTargets = 7;
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
        selectedPlane.GetComponent<MeshRenderer>().enabled = false;
        selectedPlane.GetComponent<LineRenderer>().enabled = false;
        spawnTargets();
        hint.text = "Press Start to Play";
        startButton.SetActive(true);
    }
    public void playGame(){
        startButton.SetActive(false);
        inGameMenu.GetComponent<ScoreKeeper>().enabled = true;
        hint.text = "Pull back to Aim";
    }
    public void gameEnd(){
        hint.text = "Would you like to play again?";
        playAgainButton.SetActive(true);
    }
    public void playAgain(){
        foreach (var target in targets)
            Destroy(target);
        targets.Clear();
        spawnTargets();
        playAgainButton.SetActive(false);
    }
    public void gameRestart(){
        SceneManager.LoadScene(0);
    }
    public void quit(){
        gameEnd();
        // Application.Quit();
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
        for(int i = 0; i < maxTargets; i++){
            GameObject newTarget = Instantiate(targetModel, selectedPlane.center, Quaternion.identity);
            var X = selectedPlane.size.x;
            var Z = selectedPlane.size.y;
            var x = Random.Range(-(X/2 - 0.1f), X/2 - 0.1f);
            var z = Random.Range(-(Z/2 - 0.1f), Z/2 - 0.1f);

            newTarget.transform.position = new Vector3(x + selectedPlane.center.x, newTarget.transform.position.y + 0.1f, z + selectedPlane.center.z);
            targets.Add(newTarget);
        }
    }
}
