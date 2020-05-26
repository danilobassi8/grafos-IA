using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnArbolGenerico : MonoBehaviour {

	public GameObject areatexto;
	public string stringDefecto = "";

	void Start(){
		this.stringDefecto = areatexto.GetComponent<TMPro.TextMeshProUGUI>().text;
	}

	public void OnClickBtnArbolGenerico(){
		//corregir esta linea que no hace nada.
		areatexto.GetComponent<TMPro.TextMeshProUGUI>().text = "asd";
	}
}
