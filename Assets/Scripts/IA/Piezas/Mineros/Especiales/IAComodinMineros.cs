using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAComodinMineros : PiezaIA
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        Pieza piezaReferencia;
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        //Explorador
        piezaReferencia = Resources.Load<Pieza>("Explorador Mineros");

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }

        //Combate
        piezaReferencia = Resources.Load<Pieza>("Combate Mineros");

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }


        //Laboratorio
        piezaReferencia = Resources.Load<Pieza>("Laboratorio Mineros");

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }

        //Estratega
        piezaReferencia = Resources.Load<Pieza>("Estratega Mineros");

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }

        return nuevosEstados;
    }
}
