using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCoinController : MonoBehaviour {

    [SerializeField]
    private float menuCoinRotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, menuCoinRotationSpeed * Time.deltaTime, 0);
    }
}
