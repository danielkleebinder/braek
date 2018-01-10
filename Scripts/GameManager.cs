//Manager for everything in play

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int powerDropDirection = 1;
    public int startCountdown = 3;
    public int lives = 3;           //not used AT THIS POINT
    public int bricks = 20;         //for life counter
    public float resetDelay = 1f;   //time until everything gets instantiated again
    public Text livesText;          //UI text
    public GameObject gameOver;     //UI text
    public GameObject youWon;       //UI text
    public GameObject playerOneLevelPrefab;      //Prefab
    public GameObject playerTwoLevelPrefab;      //Prefab
    //public GameObject paddle;
    public GameObject P1_paddle;               //Prefab
    public GameObject P2_paddle;               //Prefab
    public GameObject power;                //Prefab
    public GameObject deathParticles;       //Prefab
    public GameObject P1_ball;              //Prefab
    public GameObject P2_ball;              //Prefab
    public static GameManager instance = null;  //instance of this class for use in other classes
    public GameObject paddle_sound1;  //sound for collision 1
    public GameObject paddle_sound2;  //sound for collision 2 #Variety

    private GameObject P1_clonePaddle; //Copy of paddle
    private GameObject P2_clonePaddle; //Copy of paddle
    public GameObject P1_cloneBricks;   //Copy of bricks
    public GameObject P2_cloneBricks;   //Copy of bricks

    // Cameras
    public Camera cameraClassic;
    public GameObject cameraSBS;

    public GameObject BallsHolder; //Holds all balls
    public GameObject P1_cloneBall;   //Copy of ball
    public GameObject P2_cloneBall;   //Copy of ball

    //private Vector3 paddlePos;
    private Vector3 P1_paddlePos;
    private Vector3 P2_paddlePos;

    // Power Up/Down Drops
    public GameObject fogPowerDown;
    public GameObject gravityPullPowerDown;
    public GameObject energyBarrierPowerUp;
    public GameObject metalballPowerUp;
    public GameObject turretPowerUp;
    public GameObject sizeIncreasePowerUp;
    public GameObject sizeDecreasePowerDown;

    // Power Up/Down Variables
    public bool[] turretActive = new bool[] { false, false };
    public bool[] barrierActive = new bool[] { false, false };

    // Sound Effects Power Ups and Ball Switch
    public GameObject gunPickupSFX;
    public GameObject gunShotSFX;
    public GameObject metallballSFX;
    public GameObject fogSpawnSFX;
    public GameObject energybarrierSFX;
    public GameObject gravityPullSFX;
    public GameObject sizeBiggerSFX;
    public GameObject sizeSmallerSFX;

    public GameObject switchZoneSFX;

    public AudioSource gunPickupSFXA;
    public AudioSource gunShotSFXA;
    public AudioSource metallballSFXA;
    public AudioSource fogSpawnSFXA;
    public AudioSource energybarrierSFXA;
    public AudioSource gravityPullSFXA;
    public AudioSource sizeBiggerSFXA;
    public AudioSource sizeSmallerSFXA;

    public AudioSource switchZoneSFXA;

  //Initialization
  void Awake()
    {
        //only one gm required
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        Setup();

        //Rematch button
        Time.timeScale = 1f;
  }

    //Initial setup for paddle and bricks
    public void Setup()
    {

        P1_paddlePos = P1_paddle.transform.position;
        P2_paddlePos = P2_paddle.transform.position;


        // Paddles
        //P1_clonePaddle = Instantiate(paddle, paddlePos, paddle.transform.rotation) as GameObject;
        P1_clonePaddle = Instantiate(P1_paddle, P1_paddlePos, P1_paddle.transform.rotation) as GameObject;
        //P2_clonePaddle = Instantiate(paddle, new Vector3(paddlePos.x, paddlePos.y * (-1), paddlePos.z), paddle.transform.rotation); // new Quaternion(180, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w)) as GameObject; //other side
        P2_clonePaddle = Instantiate(P2_paddle, P2_paddlePos, P2_paddle.transform.rotation);

        // Paddle Clones
        P1_clonePaddle.GetComponent<PaddleController>().setInputAxis(GameSettings.cameraClassic ? "P1_vertical" : "P1_horizontal");
        P2_clonePaddle.GetComponent<PaddleController>().setInputAxis(GameSettings.cameraClassic ? "P2_vertical" : "P2_horizontal");
        P1_clonePaddle.GetComponent<PaddleController>().setSounds(paddle_sound1.GetComponent(typeof(AudioSource)) as AudioSource, paddle_sound2.GetComponent(typeof(AudioSource)) as AudioSource);
        P2_clonePaddle.GetComponent<PaddleController>().setSounds(paddle_sound1.GetComponent(typeof(AudioSource)) as AudioSource, paddle_sound2.GetComponent(typeof(AudioSource)) as AudioSource);

        P1_cloneBricks = Instantiate(playerOneLevelPrefab, playerOneLevelPrefab.transform.position, playerOneLevelPrefab.transform.rotation);
        P2_cloneBricks = Instantiate(playerTwoLevelPrefab, playerTwoLevelPrefab.transform.position, playerTwoLevelPrefab.transform.rotation);

        // Apply game settings
        cameraClassic.enabled = GameSettings.cameraClassic;
        cameraSBS.SetActive(!GameSettings.cameraClassic);

        // Gamestart countdown
        StartCoroutine(Countdown());
        Invoke("StartGame", startCountdown);

        //Audiosource assignment
        gunPickupSFXA       = gunPickupSFX.GetComponent<AudioSource>();
        gunShotSFXA         = gunShotSFX.GetComponent<AudioSource>();
        metallballSFXA      = metallballSFX.GetComponent<AudioSource>();
        fogSpawnSFXA        = fogSpawnSFX.GetComponent<AudioSource>();
        energybarrierSFXA   = energybarrierSFX.GetComponent<AudioSource>();
        gravityPullSFXA     = gravityPullSFX.GetComponent<AudioSource>();
        sizeBiggerSFXA      = sizeBiggerSFX.GetComponent<AudioSource>();
        sizeSmallerSFXA     = sizeSmallerSFX.GetComponent<AudioSource>();

        switchZoneSFXA      = switchZoneSFX.GetComponent<AudioSource>();
  }

    IEnumerator Countdown()
    {
        Text cdt = GameObject.Find("Countdown").GetComponent<Text>();
        for (int i = startCountdown; i > 0; i--)
        {
            cdt.text = "" + i;
            yield return new WaitForSeconds(1);
        }
        cdt.enabled = false;
        GameObject.Find("CountdownBackground").GetComponent<Image>().enabled = false;
    }

    void StartGame()
    {
        BallsHolder = GameObject.FindGameObjectWithTag("Balls"); //Safer than "Find"rm.rotation);

        // Instantiate all Balls in a Parent "Balls Holder"
        P1_cloneBall = Instantiate(P1_ball, P1_ball.transform.position, P1_ball.transform.rotation, BallsHolder.transform);
        P2_cloneBall = Instantiate(P2_ball, P2_ball.transform.position, P2_ball.transform.rotation, BallsHolder.transform);
    }

    //Reload level
    void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}