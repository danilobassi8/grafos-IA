using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodoController : MonoBehaviour
{

    public string letra = "A";
    public GameObject texto_pegado;
    

    private GameObject texto;

    void Start()
    {
        texto = (GameObject)Instantiate(texto_pegado, GameObject.Find("Canvas Fijo").transform);
        texto.GetComponent<textoSeguidorController>().texto = letra.ToString();
        texto.GetComponent<textoSeguidorController>().FollowThis = this.transform.Find("nodoFrente").transform;
    }

    void Update()
    {

    }

    void OnDestroy()
    {
        Destroy(texto);
    }
}