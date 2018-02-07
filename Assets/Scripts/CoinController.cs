using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    [SerializeField]
    private float rotatinSpeed = 2;
    
    [SerializeField]
    private GroundController groundcontroller;

    public ScoreManager scoremanager;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject coin;
    
    public bool isGroundCoin = true;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        coin.transform.Rotate(0, rotatinSpeed * Time.deltaTime,0);

	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            //Debug.Log("kpav coinin");
            if (isGroundCoin)
            {
                groundcontroller.scoremanager.addCoin();
            } else
            {
                scoremanager.addCoin();
            }
            anim.Play("CoinEffect",-1,0);
            //gameObject.SetActive(false);

        }
    }

    private void OnBecameInvisible()
    {
        if (!isGroundCoin)
        {
            Destroy(gameObject);
        }
    }

}
