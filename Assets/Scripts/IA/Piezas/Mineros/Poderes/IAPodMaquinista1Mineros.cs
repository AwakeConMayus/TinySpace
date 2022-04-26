using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodMaquinista1Mineros : PoderIABase
{
    [SerializeField] IAComodinMineros comodin;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        print("poderMaquinista");

        InfoTablero estadoIntermedio = SingleBestInmediateOpcion(tabBase);

        List<InfoTablero> opciones = new List<InfoTablero>();

        opciones.Add(SingleBestInmediateOpcion(estadoIntermedio));

        return opciones;
    }

    List<InfoTablero> SingleOpcionificador (InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);

        List<Casilla> posiblesMovimientos = FiltroCasillas.CasillasDeUnJugador(faccion, IATablero.instance.mapa);
        List<Casilla> posiblesDestinos = FiltroCasillas.CasillasLibres(IATablero.instance.mapa);

        int actualPuntos = Evaluar(IATablero.instance.mapa, faccion);

        int worstPuntos = int.MaxValue;
        Casilla worstCasilla = null;

        foreach (Casilla c in posiblesMovimientos)
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            int puntos = c.pieza.Puntos();
            if (puntos < worstPuntos)
            {
                worstPuntos = puntos;
                worstCasilla = c;
            }
        }

        if(worstCasilla) worstCasilla.pieza = null;

        InfoTablero estadoIntermedio = new InfoTablero(IATablero.instance.mapa);

        foreach(InfoTablero candidato in comodin.Opcionificador(estadoIntermedio))
        {
            IATablero.instance.PrintInfoTablero(candidato);
            if (Evaluar(IATablero.instance.mapa, faccion) > actualPuntos) nuevosEstados.Add(candidato);
        }

        return nuevosEstados;
    }

    InfoTablero SingleBestInmediateOpcion(InfoTablero tabBase)
    {
        InfoTablero MejorOpcion = tabBase;
        int mejorPuntuacion = int.MinValue;

        int numOpciones = 0;
        foreach (InfoTablero it in SingleOpcionificador(tabBase))
        {
            ++numOpciones;
            IATablero.instance.PrintInfoTablero(it);
            int puntosNuevos = Evaluar(IATablero.instance.mapa, faccion);
            if (puntosNuevos > mejorPuntuacion)
            {
                mejorPuntuacion = puntosNuevos;
                MejorOpcion = it;
            }
        }
        return MejorOpcion;
    }
} 

