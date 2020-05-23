using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arbolVisualController : MonoBehaviour
{
    public GameObject prefabNodo;
    public int saltoPorNivel;
    public int saltoHermano;

    public Dictionary<char, List<char>> arbol;

    void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }


    public void creaArbolVisual(Dictionary<int, List<char>> niveles, Dictionary<char, List<char>> _arbol)
    {
        arbol = _arbol;

        // primero destruyo todo rastro de un arbol viejo.
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // saco la capa cantidad de niveles hacia abajo.
        var n_maximo = 0;
        foreach (var e in niveles)
        {
            n_maximo = e.Key;
        }
        Debug.Log("NIVEL MAXIMO : " + n_maximo);

        //empiezo a graficar de la capa mas alta a la mas baja.
        for (int i = n_maximo; i >= 0; i--)
        {
            //para cada caracter_nodo en el nivel seleccionado.
            int cantX = 0;
            foreach (char c_nodo in niveles[i])
            {
                cantX++;
                var nodo = Instantiate(prefabNodo);
                nodo.name = "nodo_" + c_nodo;
                nodo.GetComponent<nodoController>().letra = c_nodo.ToString();
                nodo.transform.parent = this.gameObject.transform;
                nodo.transform.position = nodo.transform.parent.position - new Vector3(cantX * saltoHermano, -i * saltoPorNivel, 0);
            }

        }
        // ordeno la jerarquia.

        List<char> hijos = arbol['A'];
        OrdenarJerarquia('A', this.gameObject.transform, hijos);

    }

    public void OrdenarJerarquia(char c_nodoActual, Transform padre, List<char> hijos)
    {
        GameObject.Find("nodo_" + c_nodoActual).transform.parent = padre;

        foreach (char h in hijos)
        {
            if (arbol.ContainsKey(h))
            {// si el nodo que quiero guardar tambien tiene hijos.
                OrdenarJerarquia(h, GameObject.Find("nodo_" + c_nodoActual).transform.Find("hijos"), arbol[h]);
            }
            else
            {
                OrdenarJerarquia(h, GameObject.Find("nodo_" + c_nodoActual).transform.Find("hijos"), new List<char>());
            }
        }
    }

}
