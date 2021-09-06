using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaMinerosMejorada : EstrategaMineros
{
    public override int Puntos()
    {
        int puntos = base.Puntos();

        foreach (Casilla adyacentes in casilla.adyacentes)
        {
            if (!adyacentes || !adyacentes.pieza) continue;
            if (adyacentes.pieza.clase == Clase.combate && adyacentes.pieza.jugador != jugador)
            {
                puntos += 2;
                break;
            }
        }

        return puntos;
    }
}
