using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public float speed;
    bool right=false, left=false, forward=false, back=false;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Image winLose;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game On!");
    }

    IEnumerator LoadScene(float seconds){
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0){
            // Debug.Log("Game Over!");
            Text winLoseText = winLose.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            winLose.color = Color.red;
            winLoseText.color = Color.white;
            winLoseText.text = "Game Over!";
            winLose.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
        if (Input.GetKey("w") || Input.GetKey("up")){
            forward = true;
        }
        if (Input.GetKey("a") || Input.GetKey("left")){
            left = true;
        }
        if (Input.GetKey("s") || Input.GetKey("down")){
            back = true;
        }
        if (Input.GetKey("d") || Input.GetKey("right")){
            right = true;
        }
		if (Input.GetKey(KeyCode.Escape)){
			SceneManager.LoadScene("menu");
		}
    }

    void FixedUpdate(){
        if (forward){
            playerRb.AddForce(0, 0, speed * 10 * Time.deltaTime);
        }
        if (back){
            playerRb.AddForce(0, 0, -speed * 10 * Time.deltaTime);
        }
        if (left){
            playerRb.AddForce(-speed * 10 * Time.deltaTime, 0, 0);
        }
        if (right){
            playerRb.AddForce(speed * 10 * Time.deltaTime, 0, 0);
        }
        forward = back = left = right = false;
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Pickup"){
            score++;
            // Debug.Log($"Score: {score}");
            SetScoreText();
        }
        if (other.tag == "Trap"){
            health--;
            // Debug.Log($"Health: {health}");
            SetHealthText();
        }
        if (other.tag == "Goal"){
            // Debug.Log("You win!");
            Text winLoseText = winLose.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            winLose.color = Color.green;
            winLoseText.color = Color.black;
            winLoseText.text = "You Win!";
            winLose.gameObject.SetActive(true);
			StartCoroutine(LoadScene(3));
        }
    }
	void SetScoreText() => scoreText.text = $"Score: {score}";
	void SetHealthText() => healthText.text = $"Health: {health}";
}
