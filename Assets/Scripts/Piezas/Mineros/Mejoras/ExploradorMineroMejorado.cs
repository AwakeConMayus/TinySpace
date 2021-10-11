using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorMineroMejorado : ExploradorMinero
{

    public override int Puntos()
    {
        int puntosCasillaVacia = 1;

        int puntos = 0;
        foreach (Casilla c in casilla.adyacentes)
        {
            if(c && !c.pieza)
            {
                puntos += puntosCasillaVacia;
            }
        }
        return puntos;
    }
}
