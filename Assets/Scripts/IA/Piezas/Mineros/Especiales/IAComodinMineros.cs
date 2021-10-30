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
        piezaReferencia.Set_Jugador(jugador);

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }

        //Combate
        piezaReferencia = Resources.Load<Pieza>("Combate Mineros");
        piezaReferencia.Set_Jugador(jugador);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }


        //Laboratorio
        piezaReferencia = Resources.Load<Pieza>("Laboratorio Mineros");
        piezaReferencia.Set_Jugador(jugador);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }

        //Estratega
        piezaReferencia = Resources.Load<Pieza>("Estratega Mineros");
        piezaReferencia.Set_Jugador(jugador);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }

        return nuevosEstados;
    }
}
