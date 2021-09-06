using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorMineroMejorado : ExploradorMinero
{


    public override int Puntos()
    {
        int puntos = base.Puntos();
        if (puntos > 2) puntos += 2;
        return puntos;
    }
}
