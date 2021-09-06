using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCombateMinerosMejorada : NaveCombateMineros
{

    public override int Puntos()
    {
        int puntos =  base.Puntos();
        if(puntos > 1)
        {
            puntos += 2;
        }
        return puntos;
    }
}
