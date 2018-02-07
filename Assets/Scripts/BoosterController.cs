using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterController : MonoBehaviour {


    [SerializeField]
    public BallController ballcontroller;

    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            ballcontroller.Boost();
        }
    }


}
