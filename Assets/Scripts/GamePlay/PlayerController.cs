using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float remainInk = 100f;

    private Rigidbody2D player;
    private bool isOnGround;
    private GameManager gameManager;

    private static Vector2 respawnPoint;
    private static bool ifLoadCheckPoint = false;
    private static float latestCheckpointInk;
    private static List<int> latestCheckpointNo;
    private static List<int> latestCheckpointStar;
    private static List<BasicPen> latestAvailablePens;
    private static List<BasicBrush> latestAvailableBrushes;
    private List<int> collectedStars;
    private DrawingTool drawingTool;

    private GameObject levelUI;
    private GameObject victoryScreen;
    private GameObject loseScreen;

    private Animator playerAnimation;
    public LayerMask groundLayer;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        player = gameManager.player.GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        isOnGround = true;

        gameManager.setInk(remainInk);

        levelUI = gameManager.levelUI;
        victoryScreen = levelUI.transform.Find("VictoryScreen").gameObject;
        loseScreen = levelUI.transform.Find("LoseScreen").gameObject;

        collectedStars = new List<int>();
        drawingTool = GameObject.Find("DrawingTool").GetComponent<DrawingTool>();
        if (!ifLoadCheckPoint)
        {
            respawnPoint = transform.position;
            latestCheckpointInk = remainInk;
            latestCheckpointStar = new List<int>();
            latestCheckpointNo = new List<int>();
        }
        else
        {
            LoadCheckPoint();
        }
    }

    
    void Update()
    {
        // movement
        float direction = Input.GetAxis("Horizontal");
        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * moveSpeed, player.velocity.y);
            transform.localScale = new Vector2(0.25f, 0.25f);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * moveSpeed, player.velocity.y);
            transform.localScale = new Vector2(-0.25f, 0.25f);
        }    
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }
        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));

        // jump
        //if (Input.GetButtonDown("Jump") && isOnGround)
        //{
        //    player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        //    isOnGround = false;
        //    playerAnimation.SetTrigger("Jump")
        //}
        //playerAnimation.SetBool("OnGround", isOnGround);

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            playerAnimation.SetTrigger("Jump");
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        if (Physics2D.Raycast(transform.position, Vector3.down, 1.28f, groundLayer))
        {
            isOnGround = true;
            playerAnimation.SetBool("OnGround", isOnGround);
        }
        else
        {
            isOnGround = false;
            playerAnimation.SetBool("OnGround", isOnGround);
        }

        // ink
        if (gameManager.getInk() <= 0)
        {
            loseScreen.SetActive(true);
        }

        // restart
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //transform.position = respawnPoint;
            ifLoadCheckPoint = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void LoadCheckPoint()
    {
        foreach (int i in latestCheckpointNo)
        {
            GameObject.Find("Checkpoint_No." + i).GetComponent<Checkpoint>().activate();
        }
        foreach (int i in latestCheckpointStar)
        {
            Destroy(GameObject.Find("Star" + i));
        }
        transform.position = respawnPoint;
        gameManager.updateInk(remainInk - latestCheckpointInk);
        drawingTool.availablePens = new List<BasicPen>(latestAvailablePens);
        drawingTool.availableBrushes = new List<BasicBrush>(latestAvailableBrushes);
        foreach (BasicPen pen in latestAvailablePens)
        {
            GameObject pickup = GameObject.Find(pen.name + "Pickup");
            pickup.GetComponent<Pickup>().activeButton();
            Destroy(GameObject.Find(pen.name + "Pickup"));
        }
        foreach (BasicBrush brush in latestAvailableBrushes)
        {
            GameObject pickup = GameObject.Find(brush.name + "Pickup");
            pickup.GetComponent<Pickup>().activeButton();
            Destroy(GameObject.Find(brush.name + "Pickup"));
        }
        ifLoadCheckPoint = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Win")
        {
            victoryScreen.SetActive(true);
        }
        else if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (collision.tag == "MainMenu")
        {
            SceneManager.LoadScene(0);
        }else if(collision.tag == "Star")
        {
            collectedStars.Add(int.Parse(collision.gameObject.name.Substring(collision.gameObject.name.Length - 1, 1)));
        }else if(collision.tag == "Checkpoint")
        {
            Checkpoint collidedCheckPoint = collision.gameObject.GetComponent<Checkpoint>();
            if (!collidedCheckPoint.activated)
            {
                collidedCheckPoint.activate();
                respawnPoint = transform.position;
                latestCheckpointInk = gameManager.getInk();
                latestCheckpointNo.Add(int.Parse(collision.gameObject.name.Substring(collision.gameObject.name.Length - 1, 1)));
                latestCheckpointStar = new List<int>(collectedStars);
                latestAvailablePens = new List<BasicPen>(drawingTool.availablePens);
                latestAvailableBrushes = new List<BasicBrush>(drawingTool.availableBrushes);
            }
            collidedCheckPoint.showText();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            Checkpoint collidedCheckPoint = collision.gameObject.GetComponent<Checkpoint>();
            collidedCheckPoint.clostText();
        }
    }
}
