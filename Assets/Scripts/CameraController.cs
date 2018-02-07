using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject bg;

    [SerializeField]
    private Animator anim;


    void Start () {
	}

    public void initCamera()
    {
        cam.transform.position = new Vector3(0, 3.38f, -22.2f);
        //zoomingOut = false;
        //zoomingIn = false;


        //if(defaultZoom == 0)
        //{
        //    defaultZoom = cam.fieldOfView;
        //};
        //bg.transform.localScale = new Vector3(6.75f, 6.75f, 1);

        //cam.fieldOfView = defaultZoom;
        //currentZoom = defaultZoom;
    }
	
	// Update is called once per frame
	void Update () {

        //if (zoomingOut)
        //{
        //    currentZoom += zoomOutSpeed*Time.deltaTime;
        //    cam.fieldOfView = currentZoom;

        //    float scale = (currentZoom - defaultZoom) * 0.1275f;
        //    bg.transform.localScale = new Vector3(6.75f + scale, 6.75f + scale,1);

        //    if(currentZoom >= maxZoomOut)
        //    {
        //        zoomingOut = false;
        //    }
        //}

        //if (zoomingIn)
        //{
        //    currentZoom -= zoomInSpeed * Time.deltaTime;
        //    cam.fieldOfView = currentZoom;

        //    float scale = (currentZoom - defaultZoom) * 0.1275f;
        //    bg.transform.localScale = new Vector3(6.75f + scale, 6.75f + scale, 1);

        //    if (currentZoom <= defaultZoom)
        //    {
        //        zoomingIn = false;
        //    }
        //}
    }

//    [SerializeField]
//    private float zoomOutSpeed;
//    [SerializeField]
//    private float maxZoomOut;

//    private float defaultZoom = 0;

//    [SerializeField]
//    private float zoomInSpeed;

//    private float currentZoom;

//    private bool zoomingOut = false;
//    private bool zoomingIn = false;

//    public void ZoomOut()
//    {
//        zoomingOut = true;
//        zoomingIn = false;
//    }

//    public void ZoomIn()
//    {
//        zoomingOut = false;
//        zoomingIn = true;
//    }

    public void ChangeColor()
    {
        anim.Play("ChangeBackground", -1, 0);
    }
}