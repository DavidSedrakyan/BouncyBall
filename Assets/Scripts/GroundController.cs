using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

    public BallController ballcontroller;
    public ScoreManager scoremanager;
    public bool isCoinVisible;
    public bool isColorChanger;
    [SerializeField]
    public GameObject cylynderwrapper;

    private float t = 0;

    [SerializeField]
    private GameObject coinWrapper;

    [SerializeField]
    private GameObject changeColorWrapper;


    private Vector3 startPos = new Vector3(-5, -3, 2.14f);
    private Vector3 endPos = new Vector3(5, -3, 2.14f);

   //[SerializeField]
    public float speed;

    private bool moveRight = true;
    // Use this for initialization
    void Start () {
        if (movingGround)
        {
            t = Random.Range(0.0f, 1.0f);
        }
        coinWrapper.SetActive(isCoinVisible);
        changeColorWrapper.SetActive(isColorChanger);
    }

    public bool movingGround = false;


    // Update is called once per frame
    void Update () {

        CheckMaterial();

        if (movingGround)
        {
            MoveGround();
        }
    }

    private void MoveGround()
    {
        if (moveRight)
        {
            t += speed * Time.deltaTime;
            if (t >= 1)
            {
                moveRight = false;
            }
        }else
        {
            t -= speed * Time.deltaTime;
            if (t <= 0)
            {
                moveRight = true;
            }
        }
        //Debug.Log(t);
        float random = Random.Range(-gameObject.transform.position.x, gameObject.transform.position.x);
        startPos = new Vector3(-5, gameObject.transform.position.y, gameObject.transform.position.z);
        endPos = new Vector3(5, gameObject.transform.position.y, gameObject.transform.position.z);
        transform.position = Vector3.Lerp(startPos, endPos, t);
    }

    private void OnBecameInvisible()
    {
        if(gameObject.transform.position.y < ballcontroller.gameObject.transform.position.y)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private MeshRenderer meshrenderer;

    private Material currentMaterialBuffer;
    private void CheckMaterial()
    {
      
        if (currentMaterialBuffer != GroundManager.currentGroundMaterial)
        {
            meshrenderer.material = GroundManager.currentGroundMaterial;
        }

        currentMaterialBuffer = GroundManager.currentGroundMaterial;
    }


}
