using UnityEngine;
using TMPro;


public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 15f;

    Rigidbody2D rb;
    AudioSource audioSource;

    int score = 0;
    int lives = 5;

    public TextMeshProUGUI scoretxt;
    public GameObject[] livesImage;
    public GameObject gameOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            if (lives <= 0)
            {
                gameover();
            }
            else
            {
                transform.position = Vector3.zero;
                rb.linearVelocity = Vector3.zero;
                lives--;
                livesImage[lives].SetActive(false);
            }
        }

        if(rb.linearVelocity.magnitude>maxVelocity)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(audioSource.clip);

        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score+=10;
            scoretxt.text = score.ToString("00000");
        }
    }
    void gameover()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }

}
