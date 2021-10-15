using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaMineros : Estratega
{

    public override List<Casilla> CasillasDisponibles()
    {
        return FiltroCasillas.CasillasLibres();
    }
 
}
