using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private GameObject groundPrefab;


    public GameManager gamemanager;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float boostForce;

    [SerializeField]
    private float FollowBallDiff;

    //swipe
    [SerializeField]
    private float swipeMultiplier;
    private float ObjPos;
    private bool isDragging_buffer = false;
    private bool tap;
    public static bool isDragging;
    private Vector2 startTouch;
    public float currentMagnitude;
    public static float zSpeed;
    public static float xPosition;
    private float screenK = 0;
    [SerializeField]
    private float playerMaxX = 2f;

    [SerializeField]
    private float camXdivider = 3;

    [SerializeField]
    private GroundManager groundmanager;




    public void initBall()
    {
        transform.position = new Vector3(0, 3.28f, 0);
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        ifBoostCamera = false;
        ifMoveCamera = false;
    }

    // Use this for initialization
    void Start()
    {
        screenK = Screen.width / (swipeMultiplier * playerMaxX);
        //Debug.Log(Screen.width);
    }

    public static bool goingUpward = false;
    private float positionBuffer;



    // Update is called once per frame
    void Update()
    {
        
        if (!GameManager.isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            };

            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    isDragging = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDragging = false;
                };
            }


            if (isDragging)
            {

                if (Input.touches.Length > 0)
                {
                    currentMagnitude = Input.touches[0].position.x - startTouch.x;
                }

                else if (Input.GetMouseButton(0))
                {
                    currentMagnitude = Input.mousePosition.x - startTouch.x;
                }

            }

            xPosition = transform.position.x;
            if (isDragging != isDragging_buffer && isDragging)
            {
                if (!GameManager.gameStarted)
                {
                    gamemanager.StartGame();
                }
                ObjPos = transform.position.x;
            }
            else
            {
                xPosition = transform.position.x;
            };

            isDragging_buffer = isDragging;

            if (isDragging)
            {
                xPosition = ObjPos + currentMagnitude / screenK;
            };

            if (xPosition > playerMaxX)
            {
                xPosition = playerMaxX;
            }
            if (xPosition < -playerMaxX)
            {
                xPosition = -playerMaxX;
            };


            transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);

            cam.transform.position = new Vector3(xPosition / camXdivider, cam.transform.position.y, cam.transform.position.z);


            float ballY = transform.position.y;
            goingUpward = transform.position.y >= positionBuffer;

            positionBuffer = transform.position.y;

            processCameraMove();
        }        
    }


    [SerializeField]
    private float camDistance = 2;
    [SerializeField]
    private float camMoveSpeed = 4;

    [SerializeField]
    private AudioSource BounceAudioSource;

    public void BallJump(Vector3 cylynderPoistion)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = new Vector3(0, jumpForce, 0);
        rb.isKinematic = false;
        if (GameManager.gameStarted)
        {
            moveCamera(cylynderPoistion);
        }
        BallLastHitY = cylynderPoistion.y;

        BounceAudioSource.Play();
    }

    public static bool boostMode;
    public void Boost()
    {
        boostMode = true;
        GroundManager.allowBoosters = false;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = new Vector3(0, boostForce, 0);
        boostCamera();
        //cameraController.ZoomOut();
        Invoke("disableBoostMode", 4f);
        Invoke("allowBoostersToSpawn", 15f);
    }

    public void disableBoostMode()
    {
        boostMode = false;
        //cameraController.ZoomIn();
    }
    public void allowBoostersToSpawn()
    {
        GroundManager.allowBoosters = true;
    }

    private Vector3 startPos;
    private Vector3 targetPos;
    private float camMoveT;
    private bool ifMoveCamera;

    private void moveCamera(Vector3 cylynderPoistion)
    {
        startPos = cam.transform.position;
        targetPos = new Vector3(cam.transform.position.x, cylynderPoistion.y - camDistance, cam.transform.position.z);
        ifMoveCamera = true;
        ifBoostCamera = false;
        camMoveT = 0;

    }
    private void processCameraMove()
    {
        if (ifMoveCamera)
        {
            camMoveT += Time.deltaTime * camMoveSpeed;
            Vector3 pos = Vector3.Lerp(startPos, targetPos, camMoveT);
            cam.transform.position = new Vector3(cam.transform.position.x, pos.y, cam.transform.position.z);


            if (camMoveT >= 1)
            {
                ifMoveCamera = false;
                camMoveT = 0;
            }
        }else if (ifBoostCamera && goingUpward && transform.position.y >= cam.transform.position.y)
        {
            cam.transform.position = new Vector3(cam.transform.position.x,transform.position.y, cam.transform.position.z);
        }
    }


    private bool ifBoostCamera;
    private void boostCamera()
    {
        ifBoostCamera = true;
        ifMoveCamera = false;
    }
    

    public static float BallLastHitY;


    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "gameOver")
        {
            gamemanager.GameOver();
        }else if (collision.gameObject.tag == "changeColor")
        {
            cam.GetComponent<CameraController>().ChangeColor();// ChangeColor();
            groundmanager.ChangeColor();
        }

    }
}
