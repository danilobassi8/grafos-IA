using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{


    public float zoomSpeed = 2f;
    public float dragSpeed = 6f;


    private Camera cam;
    private float targetZoom;


    void Start()
    {
        cam = Camera.main;		
    }

    void Update()
    {

        //drag camera around with Middle mouse or clicks.
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);
        }

        //Zoom in and out with Mouse Wheel
        //transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);

		float scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (scrollData>0)
        {
			cam.orthographicSize -= scrollData*zoomSpeed;
        }
		if (scrollData<0)
        {
			cam.orthographicSize -= scrollData*zoomSpeed;
        }

    }
}