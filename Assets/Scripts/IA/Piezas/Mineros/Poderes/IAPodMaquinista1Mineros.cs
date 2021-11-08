using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodMaquinista1Mineros : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        print("poderMaquinista");

        InfoTablero estadoIntermedio = SingleBestInmediateOpcion(tabBase);        

        return SingleOpcionificador(estadoIntermedio);
    }

    List<InfoTablero> SingleOpcionificador (InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);

        List<Casilla> posiblesMovimientos = FiltroCasillas.CasillasDeUnJugador(faccion, IATablero.instance.mapa);
        List<Casilla> posiblesDestinos = FiltroCasillas.CasillasLibres(IATablero.instance.mapa);

        Pieza candidata = null;
        InfoTablero candidato = new InfoTablero();
        int bestPuntos = int.MinValue;

        foreach (Casilla c in posiblesMovimientos)
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            Pieza posibleCandidata = c.pieza;
            c.pieza = null;

            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);

            if(puntos > bestPuntos)
            {
                bestPuntos = puntos;
                candidata = posibleCandidata;
                candidato = new InfoTablero(IATablero.instance.mapa);
            }
        }

        IATablero.instance.PrintInfoTablero(candidato);

        foreach (Casilla c in posiblesDestinos)
        {
            IATablero.instance.PrintInfoTablero(candidato);
            c.pieza = candidata;

            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
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

