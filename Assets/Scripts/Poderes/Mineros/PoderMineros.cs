using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoderMineros : Poder
{
    public int mineral = 5;
    GameObject meteorito;

    public void RecogerMineral()
    {
        ++mineral;
        print("Recogido" + mineral);
    }

    public bool GastarMineral(int i)
    {
        if (mineral >= i)
        {
            mineral -= i;
            return true;
        }
        else return false;
    }

    public override void InitialAction()
    {
        EventManager.StartListening("RecogerMineral", RecogerMineral);
        meteorito = Resources.Load("Meteorito", typeof(GameObject)) as GameObject;
        meteorito.GetComponent<Pieza>().jugador = jugador;


        for (int i = 0; i < 10; i++)
        {
            int rnd;
            do
            {
                rnd = Random.Range(0, Tablero.instance.mapa.Count);
            } while (!meteorito.GetComponent<Pieza>().CasillasDisponibles().Contains(Tablero.instance.mapa[rnd]));


            GameObject thisPieza = Instantiate(meteorito);
            thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
        }
    }
}
