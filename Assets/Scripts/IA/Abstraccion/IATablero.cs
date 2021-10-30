using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATablero : MonoBehaviour
{
    public static IATablero instance;

    [SerializeField] float distanciaEntreCasillas;
    [SerializeField] GameObject casilla;
    [SerializeField] float relacion_lateral;

    PiezasDataBase DataBase;


    public List<Casilla> mapa = new List<Casilla>();

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);

        CreacionDeMapa();
        CreacionDeConexiones();
        DataBase = Resources.Load<PiezasDataBase>("PiezasDataBase");
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
                casillaActual.transform.position += new Vector3((i * distanciaEntreCasillas) + (distanciaEntreCasillas / 2), 0, desplazamientoLateral);
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
                casillaActual.transform.position += new Vector3((i * distanciaEntreCasillas) + distanciaEntreCasillas, 0, desplazamientoLateral);
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
        for (int i = 0; i < mapa.Count; ++i)
        {
            if (mapa[i].transform == c.transform)
            {
                return i;
            }
        }

        Debug.LogError("No se ha encontrado la casilla");
        return 0;
    }

    public void ResetCasillasEfects()
    {
        foreach (Casilla c in mapa)
        {
            c.SetState(States.none);
        }
    }


    public int[] RecuentoPuntos()
    {
        int[] puntuaciones = new int[4];

        foreach (Casilla c in Tablero.instance.mapa)
        {
            if (c.pieza)
            {
                puntuaciones[(int)c.pieza.faccion - 1] += c.pieza.Puntos();
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

    public void PrintInfoTablero(InfoTablero newTab)
    {
        while (newTab.tablero.Length > mapa.Count) Crear_Casilla_Vacia();

        for (int i = 0; i < newTab.tablero.Length; i++)
        {
            //Ya esta igual a como debe ser
            if (!mapa[i].pieza && newTab.tablero[i] == 0) continue;
            if (mapa[i].pieza && newTab.tablero[i] == (int)DataBase.GetPieza(mapa[i].pieza.gameObject)) continue;

            //Hay algo que no debe estar
            if(mapa[i].pieza && newTab.tablero[i] != (int)DataBase.GetPieza(mapa[i].pieza.gameObject))
            {
                mapa[i].pieza.SelfDestruction();
                mapa[i].pieza = null;
            }

            //Hay que Crear una Pieza
            if(newTab.tablero[i] != 0)
            {
                GameObject pieza = Instantiate(DataBase.GetPieza((IDPieza)newTab.tablero[i]));
                pieza.transform.position = mapa[i].transform.position;
                pieza.GetComponent<Pieza>().casilla = mapa[i];
                mapa[i].pieza = pieza.GetComponent<Pieza>();
            }
        }
    }
 }

public struct InfoTablero
{
    public int[] tablero;
    

    public InfoTablero(List<Casilla> tabBase)
    {
        PiezasDataBase DataBase = Resources.Load<PiezasDataBase>("PiezasDataBase");
        tablero = new int[tabBase.Count];

        for (int i = 0; i < tablero.Length; i++)
        {
            if (tabBase[i].pieza) tablero[i] = ((int)DataBase.GetPieza(tabBase[i].pieza.gameObject));
            else tablero[i] = (0);
        }
    }
}

