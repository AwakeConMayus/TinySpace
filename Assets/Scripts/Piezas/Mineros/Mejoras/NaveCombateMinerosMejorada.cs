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
            if(c && c.pieza && c.pieza.CompareClase(Clase.combate) && c.pieza.Get_Jugador() != jugador)
            {
                puntos += 1;
            }
        }
        return puntos;
    }
}
