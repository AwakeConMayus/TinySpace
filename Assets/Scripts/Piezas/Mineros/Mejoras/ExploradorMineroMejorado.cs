using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorMineroMejorado : ExploradorMinero
{

    public override int Puntos()
    {
        int puntos = 4;
        foreach (Casilla c in casilla.adyacentes)
        {
            if(c && c.pieza)
            {
                puntos = 0;
                break;
            }
        }
        return puntos;
    }
}
