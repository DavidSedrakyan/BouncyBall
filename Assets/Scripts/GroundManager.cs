using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

    [SerializeField]
    private GameObject groundPrefab;

    [SerializeField]
    private GameObject boostPrefab;

    [SerializeField]
    private BallController ballcontroller;

    [SerializeField]
    private ScoreManager scoremanager;


    [SerializeField]
    private Camera cam;
    public static float nextSpawnY = 0;

    [SerializeField]
    private float minSpawnY = 2;
    [SerializeField]
    private float maxSpawnY = 4;
    [SerializeField]
    private float minSpawnX = 2;
    [SerializeField]
    private float maxSpawnX = 4;

    [SerializeField]
    private float boosterMinX;
    [SerializeField]
    private float boosterMaxX;

    [SerializeField]
    private Material material1;
    [SerializeField]
    private Material material2;

    public static Material currentGroundMaterial;






    // Use this for initialization
    void Start () {
		
    }

	// Update is called once per frame
	void Update () {
        if (nextSpawnY < cam.transform.position.y+20)
        {
            if (!BallController.boostMode)
            {
                SpawnGround();
            }
            nextSpawnY += Random.Range(minSpawnY, maxSpawnY);
        }

        if (BallController.boostMode)
        {
            snakeCoinY = 0;
            snakeCoinStartY = nextSpawnY;
            ProcessSnakeCoins();
        }
   
        
    }


    [SerializeField]
    private float snakeCoinYInterval;

    [SerializeField]
    private GameObject snakeCoinPrefab;

    [SerializeField]
    private float snakeCoinAmplitude = 2;
    [SerializeField]
    private float snakeCoinYMultiplyer = 2;

    float snakeCoinY = 0;
    float snakeCoinStartY;
    
    private void ProcessSnakeCoins()
    {
        snakeCoinY += snakeCoinYInterval;
        float x = Mathf.Sin((snakeCoinStartY + snakeCoinY)*snakeCoinYMultiplyer) * snakeCoinAmplitude;
        GameObject snakeCoin = Instantiate(snakeCoinPrefab, new Vector3(x, snakeCoinStartY + snakeCoinY, 3.74f), Quaternion.identity);
        snakeCoin.GetComponent<CoinController>().scoremanager = scoremanager;
        snakeCoin.GetComponent<CoinController>().isGroundCoin = false;
    }

    public static bool allowBoosters;

    private void SpawnGround()
    {
        groundCount += 1;

        bool isCoinVisible = Random.Range(0, 10) < 3;
        float x = Random.Range(minSpawnX, maxSpawnX);
        Vector3 spawnPosition = new Vector3(x, nextSpawnY, 3.74f);

        if (groundCount == 1)
        {
            isCoinVisible = false;
            spawnPosition = new Vector3(0, 0, 3.74f);
        }
        else if (groundCount == 2)
        {
            float leftX = Random.Range(minSpawnX, minSpawnX + 1.5f);
            float rightX = Random.Range(maxSpawnX - 1.5f, maxSpawnX);

            float newX = rightX;
            if (Random.Range(0, 2) == 1)
            {
                newX = leftX;
            }
            spawnPosition = new Vector3(newX, nextSpawnY, 3.74f);

        };        

        GameObject ground = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);

        

        bool isSmall = Random.Range(0, 10) < 2;
        if (ScoreManager.score >= 30 && isSmall)
        {
            Vector3 groundTransformLocalScale = ground.GetComponent<GroundController>().cylynderwrapper.transform.localScale;
            groundTransformLocalScale.x = 0.6f;
            groundTransformLocalScale.z = 0.6f;
            ground.GetComponent<GroundController>().cylynderwrapper.transform.localScale = groundTransformLocalScale;
        }


        bool isMoving = Random.Range(0, 10) < 2;
        if (ScoreManager.score > 15 && isMoving )
        {
            ground.GetComponent<GroundController>().movingGround = true;
            ground.GetComponent<GroundController>().speed = Random.Range(0.1f, 0.2f);
            if (ScoreManager.score >= 30) {
                ground.GetComponent<GroundController>().speed = Random.Range(0.2f, 0.3f);
            }
        }


        bool isMoving70prcent = Random.Range(0, 10) < 7;
        if (ScoreManager.score >= 100 && isMoving70prcent)
        {
            ground.GetComponent<GroundController>().movingGround = true;
            ground.GetComponent<GroundController>().speed = Random.Range(0.1f, 0.4f);
        }



       

        ground.GetComponent<GroundController>().ballcontroller = ballcontroller;
        ground.GetComponent<GroundController>().scoremanager = scoremanager;
        ground.GetComponent<GroundController>().isCoinVisible = isCoinVisible;

        bool isBoosterVisible = Random.Range(0, 10) < 1 && ScoreManager.score >= 20 && allowBoosters;

        bool isColorchangerVisible = Random.Range(0, 10) > 5;

        ground.GetComponent<GroundController>().isColorChanger = isColorchangerVisible;



        if (isBoosterVisible){

            bool isRightBooster;
            if (spawnPosition.x - boosterMinX < 4) {
                isRightBooster = true;
            }else if(boosterMaxX - spawnPosition.x < 4)
            {
                isRightBooster = false;
            }else
            {
                isRightBooster = Random.Range(0, 2) == 0;
            }
            float boostSpawnPositionX = Random.Range(spawnPosition.x + 4, boosterMaxX);
            if (isRightBooster)
            {
                boostSpawnPositionX = Random.Range(boosterMinX, spawnPosition.x - 4);

            }
            Vector3 boostSpawnPosition = new Vector3(boostSpawnPositionX, nextSpawnY + 2, 3.74f);
            GameObject boost = Instantiate(boostPrefab, boostSpawnPosition, Quaternion.identity);
            boost.GetComponent<BoosterController>().ballcontroller = ballcontroller;
       }



    }
    
    private int groundCount;
    public void initScene()
    {
        currentGroundMaterial = material1;
        groundCount = 0;
        nextSpawnY = 5;
        SpawnGround();
        allowBoosters = true;
    }

    public void ChangeColor()
    {
        currentGroundMaterial = material2;
    }


}
