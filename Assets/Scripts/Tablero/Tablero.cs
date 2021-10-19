﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablero : MonoBehaviour
{
    public static Tablero instance;

    [SerializeField] float distanciaEntreCasillas;
    [SerializeField] GameObject casilla;
    [SerializeField] float relacion_lateral;

    public List<Casilla> mapa = new List<Casilla>();

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    
        CreacionDeMapa();
        CreacionDeConexiones();
        
    }

    void CreacionDeMapa()
    {
       mapa = new List<Casilla>();
        //Fila Central
        for (int i = 0; i < 10; i++)
        {
            GameObject casillaActual = Instantiate(casilla, transform);
            casillaActual.transform.position += new Vector3(i * distanciaEntreCasillas, 0, 0);
            mapa.Add(casillaActual.GetComponent<Casilla>());
        }
        //Filas Medias
        for (int i = 0; i < 9; i++)
        {
            float desplazamientoLateral = distanciaEntreCasillas * relacion_lateral;
            for (int j = 0; j < 2; j++)
            {
                GameObject casillaActual = Instantiate(casilla, transform);
                casillaActual.transform.position += new Vector3((i * distanciaEntreCasillas) + (distanciaEntreCasillas / 2),0, desplazamientoLateral);
                mapa.Add(casillaActual.GetComponent<Casilla>());
                desplazamientoLateral *= -1;
            }
        }
        //Filas Exteriores
        for (int i = 0; i < 8; i++)
        {
            float desplazamientoLateral = 2 * distanciaEntreCasillas * relacion_lateral;
            for (int j = 0; j < 2; j++)
            {
                GameObject casillaActual = Instantiate(casilla, transform);
                casillaActual.transform.position += new Vector3((i * distanciaEntreCasillas) + distanciaEntreCasillas,0, desplazamientoLateral);
                mapa.Add(casillaActual.GetComponent<Casilla>());
                desplazamientoLateral *= -1;
            }
        }
    }
    public void CreacionDeConexiones()
    {
        List<Vector3> vectoresAdyacencia = new List<Vector3>
        {
            new Vector3(-1, 0, 0),
            new Vector3(-0.5f, 0, -relacion_lateral),
            new Vector3(0.5f, 0, -relacion_lateral),
            new Vector3(1, 0, 0),
            new Vector3(0.5f, 0, relacion_lateral),
            new Vector3(-0.5f, 0, relacion_lateral)

        };
        for (int i = 0; i < vectoresAdyacencia.Count; i++)
        {
            vectoresAdyacencia[i] *= distanciaEntreCasillas;
        }

        foreach (Casilla casilla in mapa)
        {
            foreach (Casilla posibleAdyacente in mapa)
            {
                for (int i = 0; i < vectoresAdyacencia.Count; i++)
                {
                    if (casilla.transform.position + vectoresAdyacencia[i] == posibleAdyacente.transform.position)
                    {
                        casilla.adyacentes[i] = posibleAdyacente;
                    }
                }
            }
        }
    }

    public int Get_Numero_Casilla(GameObject c)
    {
        for(int i = 0; i < mapa.Count; ++i)
        {
            if(mapa[i].transform == c.transform)
            {
                return i;
            }
        }

        Debug.LogError("No se ha encontrado la casilla");
        return 0;
    }

    public void ResetCasillasEfects()
    {
        foreach(Casilla c in mapa)
        {
                c.SetState(States.none);
        }
    }


    public int[] RecuentoPuntos()
    {
        int[] puntuaciones = new int[2];

        foreach (Casilla c in Tablero.instance.mapa)
        {
            if (c.pieza)
            {
                puntuaciones[c.pieza.Get_Jugador()] += c.pieza.Puntos();
            }
        }

        return puntuaciones;
    }

    public GameObject Crear_Casilla_Vacia()
    {
        GameObject c = Instantiate(casilla, new Vector3(0, 0, 0), Quaternion.identity);
        mapa.Add(c.GetComponent<Casilla>());
        return c;
    }
}
