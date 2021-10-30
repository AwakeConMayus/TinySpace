using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono2Oyentes : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in IATablero.instance.mapa)
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            if (c.pieza.GetComponent<Planetas>())
            {
                PlanetaSagrado nuevoPlaneta = new PlanetaSagrado();
                nuevoPlaneta.Set_Jugador(jugador);

                c.pieza = nuevoPlaneta;
                nuevoPlaneta.casilla = c;

                nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            }

        }

        return nuevosEstados;
    }
}
