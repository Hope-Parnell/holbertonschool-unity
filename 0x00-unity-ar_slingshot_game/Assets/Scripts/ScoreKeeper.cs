using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public int score = 0;
    [SerializeField] int ammoTotal = 7;
    public TMP_Text scoreText;
    public TMP_Text ammoText;
    public List<GameObject> targets;
    [SerializeField] private GameObject arOrigin;
    [SerializeField] private GameObject ammo;
    [SerializeField] private float ammoFlyTime = 1f;
    private TMP_Text hint;
    private bool draw = false;
    private GameObject ammoPiece;
    private int currentAmmo;
    private bool ammoInFlight = false;
    private void Awake() {
        hint = arOrigin.GetComponent<PlaneSelection>().hint;
        targets = arOrigin.GetComponent<PlaneSelection>().targets;
        currentAmmo = ammoTotal;
    }
    private void Update() {
        var mousePos = Input.mousePosition;
        mousePos.z = 0.5f;
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && !ammoInFlight){
            hint.text = "Release to shoot";
            var ammoPos = Camera.main.ScreenToWorldPoint(mousePos);
            ammoPiece = Instantiate(ammo, ammoPos, Quaternion.identity, Camera.main.transform);
            draw = true;
            currentAmmo--;
            ammoText.text = "Ammo: " + currentAmmo.ToString() + "/" + ammoTotal.ToString();
        }
        if (Input.GetMouseButtonUp(0) && draw){
            draw = false;
            ammoPiece.transform.parent = null;
            var rb = ammoPiece.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(ammoPiece.transform.forward * 25, ForceMode.Impulse);
            hint.text = "Pull back to Aim";
            ammoInFlight = true;
            StartCoroutine("AmmoInFlight");
        }
        if(draw)
        {
            ammoPiece.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            ammoPiece.transform.rotation = Camera.main.transform.rotation;
        }
        if ((currentAmmo <= 0 && !ammoInFlight && !draw) || targets.Count == 0){
            arOrigin.GetComponent<PlaneSelection>().gameEnd();
        }
    }
    public void Score(){
        score += 10;
        scoreText.text = score.ToString();
    }
    IEnumerator AmmoInFlight(){
        yield return new WaitForSeconds(ammoFlyTime);
        Destroy(ammoPiece);
        ammoInFlight = false;
    }
    public void ResetGame(){
        score = 0;
        scoreText.text = "0";
        currentAmmo = 7;
        ammoText.text = "Ammo: " + currentAmmo.ToString() + "/" + ammoTotal.ToString();
    }
}
