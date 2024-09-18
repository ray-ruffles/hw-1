using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Ray Rulifson, 9/17/24, Player Controller as well as game controller

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float pickupSpeed; //Specifies pickup speed
    private float timer;
    private int seconds;
    private int countdown;
    private float cowMultiplier;
    
    // Variables for cow object
    public GameObject cow;
    private float cowTimer;
    private int cowSeconds;

    public Text countText;
    public Text winText;
    public Text loseText;
    public Button restartButton;
    public Text cowText;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        countdown = 60; //specifies countdown number
        timer = 0.0f;
        cowMultiplier = 0.0f;
        countText.text = "Timer: " + countdown.ToString();
        winText.text = "";
        loseText.text = "";
        cowText.text = "Cow Multiplier: " + cowMultiplier.ToString();
        restartButton.gameObject.SetActive(false); //hide buton

        //Cow Timer
        cowTimer = 0.0f;
        cowSeconds = 0;
    }

    // FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {       
        // Displays diffrent texts depending on timer
        if ((countdown - timer) >= 0 && loseText.text == "")
        {
            countText.text = "Timer: " + (countdown - seconds).ToString();
            
            // Timer
            timer += Time.deltaTime;
            seconds = (int)timer % 60;
        }
        if ((countdown - timer) <= 0)
        {
            countText.text = "Timer: 0";
            winText.text = "You Win!";
            restartButton.gameObject.SetActive(true);
        }

        //Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;

        //Cow Multiplier
        cowText.text = "Cow Multiplier: " + cowMultiplier.ToString();
        //Determines if cow is active and if so, wait to turn back on.
        if (!cow.activeSelf)
        {
            cowTimer += Time.deltaTime;
            cowSeconds = (int)cowTimer % 60;

        }
        if (cowSeconds >= 5)
        {
            cow.SetActive(true);
            cowTimer = 0f;
            cowSeconds = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PickUp") && (countdown - seconds) >= 0) 
        {
            loseText.text = "You Lose!";
            restartButton.gameObject.SetActive(true); // show button
            //other.gameObject.SetActive(false); // disappear from scene
        }

        if (other.gameObject.CompareTag("Cow"))
        {
            cowMultiplier += .1f;
            pickupSpeed -= .1f;
        }
    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene"); // restarts the game
    }
}
