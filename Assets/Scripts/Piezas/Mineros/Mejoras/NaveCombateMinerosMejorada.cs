using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCombateMinerosMejorada : NaveCombateMineros
{

    public override int Puntos()
    {
        int puntos =  base.Puntos();
        foreach(Casilla c in casilla.adyacentes)
        {
            if(c.pieza.CompareClase(Clase.combate) && c.pieza.jugador != jugador)
            {
                puntos += 2;
            }
        }
        return puntos;
    }
}
