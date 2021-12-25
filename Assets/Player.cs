using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpForce;
    Rigidbody2D rb2d;
    SpriteRenderer playerSprite;
    GameManager gm;
    public GameObject panel;
    bool isEnded = true;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        if (rb2d == null)
        {
            Debug.Log("Rigidbody2D is NULL");
        }
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnded)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                rb2d.velocity = Vector2.up * jumpForce;
            }
        }
        else
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                Play();
                rb2d.velocity = Vector2.up * jumpForce;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ColorChanger")
        {
            playerSprite.color = 
                collider.GetComponent<SpriteRenderer>().color;
            gm.InstantiateCircle();
            gm.InstantiateColorChanger();            
            return;
        }
        if (collider.tag == "Circle")
        {
            if (playerSprite.color != collider.GetComponent<SpriteRenderer>().color)
            {
                Debug.Log("GAME OVER");
                GameOver();
            }
        }
        
    }

    public void Play()
    {
        panel.SetActive(false);
        rb2d.gravityScale = 1f;
        gm.CreateCircles();
        gm.SetPlayerColor();
        isEnded = false;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
