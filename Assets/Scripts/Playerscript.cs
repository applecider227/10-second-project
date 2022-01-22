using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Playerscript : MonoBehaviour
{
    public float velocity = 35;
    private Rigidbody2D rb;
    float horizontal;

    public TextMeshProUGUI coins;

    private int coinsCollected = 0;

    public AudioClip coinPickup;

    public AudioClip victoryMusic;
    public AudioClip defeatMusic;
   public AudioSource audioSource;

   private int playOnce = 0;

   float currentTime = 0f;

   float startingTime = 12f;
   [SerializeField] Text countdownText;

    public GameObject winText;
    public GameObject loseText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetCoinsText();
        audioSource = GetComponent<AudioSource>();
        currentTime = startingTime;
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    void SetCoinsText()
    {
       coins.text = "Coins Collected: " + coinsCollected.ToString() + "/5";  
    }

    // Update is called once per frame
    void Update()
    {
         Input.GetAxis("Horizontal");
          
        if(Input.GetKeyDown("space"))
        {
            rb.velocity = Vector2.up * velocity;
        }
        if(Input.GetKey("a"))
        {
            rb.velocity = Vector2.left * velocity;
        }
        if(Input.GetKey("d"))
        {
            rb.velocity = Vector2.right * velocity;
        }

         currentTime -= 1 * Time.deltaTime;
      countdownText.text = currentTime.ToString("0");
      if (currentTime <= 0)
      {
          currentTime = 0;
          velocity = 0f;
          if (coinsCollected < 5)
          {
            loseText.SetActive(true);
            if (playOnce == 0)
              {
              audioSource.PlayOneShot(defeatMusic);
             playOnce = playOnce +1;
              }
          }
          if (Input.GetKeyDown(KeyCode.R))
          {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
          }
           if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
   
    if (coinsCollected == 5)
          {
              winText.SetActive(true);
              if (playOnce == 0)
              {
              audioSource.PlayOneShot(victoryMusic);
             playOnce = playOnce +1;
              }
          }
    if (currentTime > 10)
    {
        velocity = 0f;
    }
    if (currentTime <= 10 && currentTime > 0)
    {
        velocity = 2.25f;
    }
    }
    void OnCollisionEnter2D(Collision2D collision)
 {
     if(collision.collider.tag == "Coin")
     {
         Destroy(collision.collider.gameObject);
         coinsCollected = coinsCollected +1;
         SetCoinsText();
         audioSource.clip = coinPickup;
            audioSource.Play();
     }
 }
}
