using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NaveCombate : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.combate;
    }

    public override int Puntos()
    {
        int puntos = 0;
        foreach(Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.CompareClase(Clase.combate) && adyacente.pieza.jugador == jugador)
            {
                puntos += 3;
            }
        }
        return puntos;
    }
}
