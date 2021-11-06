using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpcionesOyentes : IAOpciones
{

    List<InfoTablero>[] tablerosOrden = new List<InfoTablero>[5];


    public override List<InfoTablero> JugadaSimpleOpciones()
    {
        ResetTableros();
        print("Nuevo Turno Oyente");

        List<InfoTablero> jugadas = new List<InfoTablero>();

        InfoTablero tabBase = new InfoTablero(Tablero.instance.mapa);


        for (int i = 0; i < 3; i++)
        {

            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();
            print(pieza.gameObject.name);
            foreach (InfoTablero newTab in pieza.Opcionificador(tabBase))
            {
                jugadas.Add(newTab);
                tablerosOrden[i].Add(newTab);
            }
        }

        return jugadas;
    }

    void ResetTableros()
    {
        for (int i = 0; i < tablerosOrden.Length; i++)
        {
            tablerosOrden[i] = new List<InfoTablero>();
        }
    }

    public override void EjecutarJugada(InfoTablero newTab)
    {
        base.EjecutarJugada(newTab);

        
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
