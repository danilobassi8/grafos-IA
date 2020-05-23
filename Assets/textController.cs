using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textController : MonoBehaviour
{
    public string texto;
    public GameObject padre;
	public Vector3 offset = new Vector3(0,0,0);

    private Camera camara;

    void Start()
    {
        camara = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = padre.transform.position+offset;

    }
}
