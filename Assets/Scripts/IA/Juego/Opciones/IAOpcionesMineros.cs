using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpcionesMineros : IAOpciones
{
    public GameObject[] mejoras = new GameObject[4];
    public int mineral = 5;

    List<InfoTablero>[] tablerosPorCostes = new List<InfoTablero>[10];
    List<InfoTablero>[] tablerosOrden = new List<InfoTablero>[5];



    private void Start()
    {
        EventManager.StartListening("RecogerMineral", AddMineral);
        ResetTableros();
    }

    public override List<InfoTablero> JugadaSimple()
    {
        print("Nuevo Turno Minero");

        List<InfoTablero> opciones = new List<InfoTablero>();

        InfoTablero tabBase = new InfoTablero(Tablero.instance.mapa);

        ResetTableros();

        for (int i = 0; i < 3; i++)
        {
            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();

            int mineralGastar = 0;
            if (opcionesDisponibles[i] == 4) mineralGastar = opcionesIniciales[4].GetComponent<Especial>().coste;

            if (mineral >= mineralGastar)
            {
                foreach(InfoTablero newTab in pieza.Opcionificador(tabBase))
                {
                    opciones.Add(newTab);
                    tablerosPorCostes[mineralGastar].Add(newTab);
                    tablerosOrden[i].Add(newTab);
                }
                
            }
        }

        if (mineral >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (opcionesDisponibles[i] == 4) continue;

                PiezaIA pieza = mejoras[opcionesDisponibles[i]].GetComponent<PiezaIA>();

                foreach (InfoTablero newTab in pieza.Opcionificador(tabBase))
                {
                    opciones.Add(newTab);
                    tablerosPorCostes[3].Add(newTab);
                    tablerosOrden[i].Add(newTab);
                }
            }
        }

        return opciones;
    }

    public void AddMineral()
    {
        ++mineral;
    }

    void ResetTableros()
    {
        for (int i = 0; i < tablerosPorCostes.Length; i++)
        {
            tablerosPorCostes[i] = new List<InfoTablero>();
        }
        for (int i = 0; i < tablerosOrden.Length; i++)
        {
            tablerosOrden[i] = new List<InfoTablero>();
        }
    }

    public override void EjecutarJugada(InfoTablero newTab)
    {
        base.EjecutarJugada(newTab);

        for (int i = 0; i < tablerosPorCostes.Length; i++)
        {
            if (tablerosPorCostes[i].Contains(newTab))
            {
                mineral -= i;
            }
        }
        for (int i = 0; i < tablerosOrden.Length; i++)
        {
            if (tablerosOrden[i].Contains(newTab))
            {
                IARotarOpcion(i);
            }
        }

        ResetTableros();
    }
}
