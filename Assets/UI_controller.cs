using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_controller : MonoBehaviour
{

    void Start()
    {
		// al arrancar, coloca todos los paneles de la UI en falso, excepto el panel principal.
		var paneles = GameObject.Find("UI_paneles");
		foreach(Transform panel in paneles.transform){
			if(panel.name == "Panel_arbol")
				panel.gameObject.SetActive(true);
			else
				panel.gameObject.SetActive(false);
		}
    }

    void Update()
    {

    }

    public void Activo(GameObject obj, bool activo)
    {
        obj.SetActive(activo);
    }
	public void Activar(GameObject obj)
    {
        obj.SetActive(true);
    }
	public void Desactivar(GameObject obj)
    {
        obj.SetActive(false);
    }

}
