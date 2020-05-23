using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class arbolController : MonoBehaviour
{
    //texto del que luego se hace el arbol.
    public string texto = "-";
    public string[] operaciones;
    public Dictionary<char, List<char>> nodo = new Dictionary<char, List<char>>();
    public List<char> nodosHijos = new List<char>();
    public int cantNodosHojas = 0;
    public GameObject arbolVisualPrefab;

    //se usa para guardar todos los nodos q se van presentando.
    private List<char> listaTotalNodos = new List<char>();

    public Dictionary<int, List<char>> niveles = new Dictionary<int, List<char>>();
    // niveles es un diccionario de (1,list(nodo1,nodo2))





    void Start()
    {

    }

    void Update()
    {

    }

    public void CapturaTextoInterfaz(GameObject textbox)
    {
        this.texto = textbox.GetComponent<TMPro.TextMeshProUGUI>().text;
        InterpretaTexto(this.texto);
    }
    public void InterpretaTexto(string texto)
    {
        try
        {
            // cada vez que interpreto el texto, reseteo todo para no agregar siempre a las mismas listas.
            nodo = new Dictionary<char, List<char>>();
            nodosHijos = new List<char>();
            cantNodosHojas = 0;
           

            operaciones = texto.ToUpper().Split('\n');
            foreach (string op in operaciones)
            {
                //guardo los dos valores que aparecieron en un arreglo, si no estaba antes.
                if (!listaTotalNodos.Contains(op[0]))
                {
                    listaTotalNodos.Add(op[0]);
                }
                if (!listaTotalNodos.Contains(op[2]))
                {
                    listaTotalNodos.Add(op[2]);
                }

                if (nodo.ContainsKey(op[0]))
                {
                    //si el arbol ya contenia un nodo con ese nombre, le agrego el hijo actual a la lista de hijos y lo guardo otra vez.
                    List<char> hijos = nodo[op[0]];
                    hijos.Add(op[2]);
                    nodo[op[0]] = hijos;
                }
                else
                {
                    // si el arbol no contenia a ese nodo, lo creo con un arreglo que solo contiene 1 hijo.
                    List<char> listanueva = new List<char>();
                    listanueva.Add(op[2]);
                    nodo.Add(op[0], listanueva);
                }

            }
            //MostrarArbol();

            // Con esto puedo saber cuantos nodos "hojas" voy a tener.
            foreach (char c in listaTotalNodos)
            {
                if (!nodo.ContainsKey(c))
                {
                    nodosHijos.Add(c);
                }
            }
            cantNodosHojas = nodosHijos.Count;
        }
        catch (Exception e)
        {
            Debug.Log("error: " + e);
        }

        //crea arbol visual.
        CrearArbolVisual();
        
    }

    public void MostrarArbol()
    {
        foreach (KeyValuePair<char, List<char>> kvp in nodo)
        {
            foreach (char c in kvp.Value)
            {
                Debug.Log("la key " + kvp.Key + " --> " + c);
            }
        }
    }

    private void CrearArbolVisual()
    {
        //reseteo los niveles y los separo.
        niveles = new Dictionary<int, List<char>>();
        separarArbolPorNivel('A', 0);
        GameObject.Find("ArbolVisual").GetComponent<arbolVisualController>().creaArbolVisual(niveles,nodo);
        
    }
    private void MostrarNiveles()
    {
        foreach (var e in niveles)
        {
            Debug.Log(" ------------------------ " + e.Key + " ------------------------");
            foreach (var f in e.Value)
            {
                Debug.Log(f);
            }
        }
    }

    private void separarArbolPorNivel(char c_nodo, int nivelActual)
    {
        
        if (niveles.ContainsKey(nivelActual))
        {// si el nivel existe, me traigo los elementos del nivel q ya tenia y le agrego
            var n = niveles[nivelActual];
            n.Add(c_nodo);
            niveles[nivelActual] = n;
        }
        else
        {// si el nivel no existia, creo uno nuevo con el numero de nivel y una lista con 1 solo elemento.
            List<char> nuevaLista = new List<char>();
            nuevaLista.Add(c_nodo);
            niveles.Add(nivelActual, nuevaLista);
        }

        //me traigo los hijos del nodo "c_nodo".
        // si es que tiene, sino, termina.
        if (nodo.ContainsKey(c_nodo))
        {
            var hijosDelNodo = nodo[c_nodo];
            //agrego este nodo al nivel actual y sus hijos al siguiente.
            foreach (char c_hijo in hijosDelNodo)
            {
                separarArbolPorNivel(c_hijo, nivelActual + 1);
            }
        }
    }
}
