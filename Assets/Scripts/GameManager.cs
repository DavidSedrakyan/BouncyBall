using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameStarted;
    public static bool isGameOver;
    [SerializeField]
    private Canvas gameCanvas;
    [SerializeField]
    private Canvas gameOverCanvas;
    [SerializeField]
    private Canvas menuCanvas;

    [SerializeField]
    private GameObject groundPrefab;

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private BallController ballcontroller;
    [SerializeField]
    private ScoreManager scoremanager;
    [SerializeField]
    private GroundManager groundmanager;
    [SerializeField]
    private CameraController cameracontroller;



    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start () {
        GoToMenu();
    }
	
	void Update () {

    }
    [SerializeField]
    private AudioSource deathAudio;

    public void GameOver()
    {
        isGameOver = true;
        BallController.isDragging = false;
        deathAudio.Play();
        gameStarted = false;
        gameOverCanvas.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        GoToMenu();
    }

    public void GoToMenu()
    {
        isGameOver = false;
        gameStarted = false;
        gameCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
        DeleteObjects();
        initScene();
    }

    public void initScene()
    {
        ballcontroller.initBall();
        cameracontroller.initCamera();
        
        ScoreManager.score = 0;
        ScoreManager.coin = 0;
        groundmanager.initScene();
    }

    public void StartGame()
    {
        gameCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(false);
        gameStarted = true;
    }

    private void DeleteObjects()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("ground");
        for (int i = 0; i < obj.Length; i++)
        {
            Destroy(obj[i]);
        };

        obj = GameObject.FindGameObjectsWithTag("snakeCoin");
        for (int i = 0; i < obj.Length; i++)
        {
            Destroy(obj[i]);
        };

    }


}
