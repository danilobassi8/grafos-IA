using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arbolVisualController : MonoBehaviour
{
    public GameObject prefabNodo;
    public int saltoPorNivel;
    public int saltoHermano;
    public Dictionary<char, List<char>> arbol;

    private float posx = 0;
    private List<char> listasNodosGraficados = new List<char>();

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
                nodo.transform.position = nodo.transform.parent.position - new Vector3(0, -i * saltoPorNivel, 0);
            }

        }

        // ordeno la jerarquia.

        List<char> hijos = arbol['A'];
        OrdenarJerarquia('A', this.gameObject.transform, hijos);

        posx = 0;
        Invoke("GraficarDesdeRaiz", 0.1f);
    }

    public void GraficarDesdeRaiz()
    {
        Graficar('A');
    }

    private void Graficar(char c_nodo)
    {
        Debug.Log(c_nodo);
        if (tieneHijos(c_nodo))
        {
            //mando a dibujar al primer hijo.
            char hijo1 = GameObject.Find("nodo_" + c_nodo).transform.Find("hijos").GetChild(0).name.Replace("nodo_", "")[0];
            Graficar(hijo1);
        }
        else
        {   // si no tiene mas hijos, lo pongo en posx.
            dibujar(c_nodo);
        }
        if (tieneHermanoDerecha(c_nodo))
        {
            //SOLO SI ES PADRE. si sus hijos ya fueron dibujados, los pongo en el promedio de ellos.
            if (tieneHijos(c_nodo))
            {
                dibujar_padre(c_nodo);
            }

            // mando a graficar al primer hermano a la derecha.
            var nodo = GameObject.Find("nodo_" + c_nodo).gameObject;
            int indice_hermano = nodo.transform.GetSiblingIndex();
            var padre = nodo.transform.parent.transform.parent.gameObject;
            char hermano = padre.transform.Find("hijos").transform.GetChild(indice_hermano + 1).gameObject.name.Replace("nodo_", "")[0];

            Graficar(hermano);

        }
        else
        {
            if (tieneHijos(c_nodo))
            {
                dibujar_padre(c_nodo);
            }
        }

    }

    private void dibujar(char c_nodo)
    {
        var nodo = GameObject.Find("nodo_" + c_nodo).gameObject;
        nodo.transform.position = new Vector3(posx, nodo.transform.position.y, 0);
        listasNodosGraficados.Add(nodo.name.Replace("nodo_", "")[0]);
        posx = posx - saltoHermano;
    }
    private void dibujar_padre(char c_nodo)
    {
        float promediox = 0;

        var ObjetoHijos = GameObject.Find("nodo_" + c_nodo).transform.Find("hijos").gameObject;
        foreach (Transform hijo in ObjetoHijos.transform)
        {
            promediox += hijo.transform.Find("nodoFrente").transform.position.x;
        }
        promediox = promediox / (ObjetoHijos.transform.childCount);

        var nodo = GameObject.Find("nodo_" + c_nodo).transform.Find("nodoFrente").gameObject;
        nodo.transform.position = new Vector3(promediox, nodo.transform.position.y, 0);
        listasNodosGraficados.Add(nodo.name.Replace("nodo_", "")[0]);
    }

    public bool tieneHijos(char c_nodo)
    {
        if (GameObject.Find("nodo_" + c_nodo).transform.Find("hijos").transform.childCount > 0)
            return true;
        return false;
    }
    public bool tieneHermanoDerecha(char c_nodo)
    {

        //si es A, devuelvo directo.
        if (c_nodo == 'A')
            return false;

        //obtengo indice de hermano.
        var nodo = GameObject.Find("nodo_" + c_nodo).gameObject;
        int indice_hermano = nodo.transform.GetSiblingIndex();

        //veo si el padre tiene mas hermanos aparte de el.
        var padre = nodo.transform.parent.transform.parent.gameObject;
        if (padre.transform.Find("hijos").transform.childCount - 1 > indice_hermano)
            return true;
        else
            return false;
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
