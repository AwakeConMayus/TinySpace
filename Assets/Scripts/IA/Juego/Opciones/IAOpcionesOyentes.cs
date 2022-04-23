using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpcionesOyentes : IAOpciones
{

    List<InfoTablero>[] tablerosOrden = new List<InfoTablero>[5];

    List<PiezaIA> abanicoOpciones = new List<PiezaIA>();

    private void Start()
    {       
        foreach (GameObject g in opcionesIniciales) abanicoOpciones.Add(g.GetComponent<PiezaIA>());
    }


    public override List<InfoTablero> JugadaSimpleOpciones()
    {
        ResetTableros();

        List<InfoTablero> jugadas = new List<InfoTablero>();

        InfoTablero tabBase = new InfoTablero(Tablero.instance.mapa);


        for (int i = 0; i < 3; i++)
        {

            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();
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

    public override bool Ahogado()
    {
        for (int i = 0; i < 3; i++)
        {
            if (opcionesIniciales[opcionesDisponibles[i]].GetComponent<Pieza>().CasillasDisponibles().Count > 0) return false;
        }
        return true;
    }

    public override int BestRespuesta(InfoTablero tabBase)
    {
        List<InfoTablero> respuestas = new List<InfoTablero>();

        foreach (PiezaIA pia in abanicoOpciones) respuestas.Add(pia.BestInmediateOpcion(tabBase));

        int bestRespuesta = int.MinValue;

        foreach (InfoTablero it in respuestas)
        {
            IATablero.instance.PrintInfoTablero(it);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            bestRespuesta = bestRespuesta > puntos ? bestRespuesta : puntos;
        }
        return bestRespuesta;
    }
}
