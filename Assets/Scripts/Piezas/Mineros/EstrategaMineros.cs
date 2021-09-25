using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaMineros : Estratega
{
    public int nivel = 1;

    public override int Puntos()
    {
        return base.Puntos() * nivel;
    }

    public override List<Casilla> CasillasDisponibles()
    {
        return FiltroCasillas.CasillasLibres();
    }
 
}
