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

        foreach (Casilla c in posiblesMovimientos)
        {
            foreach (Casilla cc in posiblesDestinos)
            {
                IATablero.instance.PrintInfoTablero(tabBase);

                cc.pieza = c.pieza;
                c.pieza = null;

                nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            }
        }

        return nuevosEstados;
    }

    InfoTablero SingleBestInmediateOpcion(InfoTablero tabBase)
    {
        InfoTablero MejorOpcion = tabBase;
        int mejorPuntuacion = -1000;

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

