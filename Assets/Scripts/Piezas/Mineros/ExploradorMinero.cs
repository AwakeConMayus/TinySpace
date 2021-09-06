using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorMinero : Explorador
{


    public override List<Casilla> CasillasDisponibles()
    {
        return FiltroCasillas.CasillasLibres();
    }
}
