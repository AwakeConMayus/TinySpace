using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCombateMinerosMejorada : NaveCombateMineros
{

    public override int Puntos()
    {
        int puntosEnemigoAdyacente = 2;

        int puntos =  base.Puntos();
        foreach(Casilla c in casilla.adyacentes)
        {
            if(c && c.pieza && c.pieza.faccion != faccion)
            {
                puntos += puntosEnemigoAdyacente;
            }
        }
        return puntos;
    }
}
