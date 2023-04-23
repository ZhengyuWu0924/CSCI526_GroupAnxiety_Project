using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerProperty {PositiveMag, NegativeMag, Antigravity, NONE}
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float remainInk = 100f;
    public float playerGravity = 2f;
    public Sprite gravityStatusImage;
    public Sprite positiveStatusImage;
    public Sprite negativeStatusImage;

    private Rigidbody2D player;
    private bool isOnGround;
    private GameManager gameManager;

    private static Vector2 respawnPoint;
    public static bool ifLoadCheckPoint = false;
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
    private GameObject starBar;
    private LevelStar levelStar;
    private GameObject instructionScreen;

    private Animator playerAnimation;
    public LayerMask groundLayer;
    private Vector3 playerWidth = new Vector3(0.5f, 0, 0);
    private float playerHeight = 1.4f;
    public Magnetism playerMag = Magnetism.None; // Initialize character's magnetism to None
    private PlayerProperty playerPro = PlayerProperty.NONE; // Initialize character's property

    private PlayerStatusController playerStatusController;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        try
        {
            player = gameManager.player.GetComponent<Rigidbody2D>();
            player.gravityScale = 2f; // Initialize Player gravity to default value
            playerAnimation = GetComponent<Animator>();
            isOnGround = true;

            playerStatusController = GameObject.FindObjectOfType<PlayerStatusController>(true);

            gameManager.setInk(remainInk);

            levelUI = gameManager.levelUI;
            victoryScreen = levelUI.transform.Find("VictoryScreen").gameObject;
            instructionScreen = levelUI.transform.Find("InstructionScreen").gameObject;
            loseScreen = levelUI.transform.Find("LoseScreen").gameObject;
            starBar = levelUI.transform.Find("LevelStar").gameObject;
            levelStar = starBar.GetComponent<LevelStar>();

            drawingTool = GameObject.Find("DrawingTool").GetComponent<DrawingTool>();
            if (!ifLoadCheckPoint)
            {
                collectedStars = new List<int>();
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
        catch (Exception e)
        {
            Debug.Log(e);
            ifLoadCheckPoint = false;
            respawnPoint = transform.position;
            latestCheckpointInk = remainInk;
            latestCheckpointStar = new List<int>();
            latestCheckpointNo = new List<int>();
            latestAvailablePens = new List<BasicPen>();
            latestAvailableBrushes = new List<BasicBrush>();
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

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            playerAnimation.SetTrigger("Jump");
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        if (Physics2D.Raycast(transform.position, Vector3.down, playerHeight, groundLayer) ||
            Physics2D.Raycast(transform.position - playerWidth, Vector3.down, playerHeight, groundLayer) ||
            Physics2D.Raycast(transform.position + playerWidth, Vector3.down, playerHeight, groundLayer))
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
    }

    private void LoadCheckPoint()
    {
        foreach (int i in latestCheckpointNo)
        {
            GameObject.Find("Checkpoint_No." + i).GetComponent<Checkpoint>().activate();
        }
        collectedStars = new List<int>(latestCheckpointStar);
        levelStar.SetStars(latestCheckpointStar.Count);
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
            GameObject pickup = GameObject.Find(pen.penName + "Pickup");
            pickup.GetComponent<Pickup>().activeButton();
            Destroy(GameObject.Find(pen.name + "Pickup"));
        }
        foreach (BasicBrush brush in latestAvailableBrushes)
        {
            GameObject pickup = GameObject.Find(brush.brushName + "Pickup");
            pickup.GetComponent<Pickup>().activeButton();
            Destroy(GameObject.Find(brush.name + "Pickup"));
        }
        ifLoadCheckPoint = false;
        if (instructionScreen != null)
        {
            instructionScreen.SetActive(false);
        }
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
            levelStar.SetStars(collectedStars.Count);
            
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

    /*
    @input: String type property the character gonna be changed to
    @TODO: Modify the case statement when the magnet brush got splitted
    This function will be called when the shrines been brushed
    then change the character properties based on the brush
    Shrine color will also be changed, corresponding to the brush
    */
    public void OnShrineBrushed(BasicBrush chosenBrush, BrushType currentBrush){
        player = gameManager.player.GetComponent<Rigidbody2D>();
        switch(currentBrush){
            case BrushType.MAGNET_POS:
                /*
                    Implement this part later when the magnet brush issues have been fixed
                */
                playerPro = playerPro == PlayerProperty.PositiveMag ? PlayerProperty.NONE : PlayerProperty.PositiveMag;
                playerMag = playerPro == PlayerProperty.PositiveMag ? Magnetism.Postive : Magnetism.None;
                
                playerGravity = 2f;
                player.gravityScale = playerGravity;
                jumpSpeed = 8f;
                playerStatusController.ActivateStatus(positiveStatusImage);
                gameManager.updateInk(2);
                break;
            case BrushType.MAGNET_NEG:
                playerPro = playerPro == PlayerProperty.NegativeMag ? PlayerProperty.NONE : PlayerProperty.NegativeMag;
                playerMag = playerPro == PlayerProperty.NegativeMag ? Magnetism.Negtive : Magnetism.None;
                
                playerGravity = 2f;
                player.gravityScale = playerGravity;
                jumpSpeed = 8f;
                playerStatusController.ActivateStatus(negativeStatusImage);
                gameManager.updateInk(2);
                break;
            case BrushType.GRAVITY:
                // check current activatived character property first
                // if none is activating, change it to anti-gravity
                // otherwise, cancel the anti-gravity influence and change it back to default state
                playerPro = playerPro == PlayerProperty.Antigravity ? PlayerProperty.NONE : PlayerProperty.Antigravity;

                // remove the influence of magetism
                playerMag = Magnetism.None; 

                // change the player's gravity scale based on its current property state
                playerGravity = playerPro == PlayerProperty.Antigravity ? 1f : 2f; 
                player.gravityScale = playerGravity;

                // change the player's jump ability based on its current property state
                jumpSpeed = playerPro == PlayerProperty.Antigravity ? 10f : 8f;
                playerStatusController.ActivateStatus(gravityStatusImage);
                gameManager.updateInk(4);
                break;
            default:
                break;
        }
        print(playerMag);
        shrineColorChange(chosenBrush, currentBrush);
        
    }

    private void shrineColorChange(BasicBrush chosenBrush, BrushType currentBrush){
        // Find all shrine tag objects
        GameObject[] shrineObjects = GameObject.FindGameObjectsWithTag("Shrine");
        foreach (GameObject shrineObject in shrineObjects){
            Renderer renderer = shrineObject.GetComponent<Renderer>();
            renderer.material.color = renderer.material.color == chosenBrush.getColor(currentBrush) ? resetShrineColor() : chosenBrush.getColor(currentBrush);
            
        }
    }

    private Color resetShrineColor(){
        playerStatusController.DeactivateStatus();
        return Color.white;
    }
}
