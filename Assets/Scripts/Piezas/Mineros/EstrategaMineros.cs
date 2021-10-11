using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaMineros : Estratega
{
    [HideInInspector]
    public int nivel = 0;

    public override int Puntos()
    {
        int puntosExtraNivel = 2;

        int navesNetas = base.Puntos() / 3;

        int incremento = 3 + nivel * puntosExtraNivel;

        return navesNetas * incremento;
    }

    public override List<Casilla> CasillasDisponibles()
    {
        return FiltroCasillas.CasillasLibres();
    }
 
}
