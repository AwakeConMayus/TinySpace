using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorMinero : Explorador
{


    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        return FiltroCasillas.CasillasLibres(referencia);
    }
}
