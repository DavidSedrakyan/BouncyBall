using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour {

    [SerializeField]
    private GroundController groundcontroller;
    [SerializeField]
    private Animator anim;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if(BallController.BallLastHitY < transform.position.y)
        {
            updateCollisionLocation(false);
        }	
	}
    private bool alreadyJumped = false;

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "ball" && groundcontroller.ballcontroller.gameObject.transform.position.y > transform.position.y && !BallController.goingUpward)
        {
            groundcontroller.ballcontroller.BallJump(transform.position);
            anim.Play("CylinderAnim",-1,0);
            if (GameManager.gameStarted)
            {                
                updateCollisionLocation(true);
            }

            if (!alreadyJumped)
            {
                groundcontroller.scoremanager.addScore();
                alreadyJumped = true;
            }
            
        }

    }
    
    private void updateCollisionLocation(bool tobottom)
    {
        BoxCollider coll = gameObject.GetComponent<BoxCollider>();
        if (tobottom)
        {
            coll.center = new Vector3(coll.center.x, 0.8f, coll.center.z);
        } else
        {
            coll.center = new Vector3(coll.center.x, -0.31f, coll.center.z);
        }
        
    }

        

}
