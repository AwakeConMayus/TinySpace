using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCombate : Pieza
{

    public override int Puntos()
    {
        int puntos = 0;
        foreach(Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.CompareClase(Clase.combate) && adyacente.pieza.jugador == jugador)
            {
                ++puntos;
            }
        }
        return puntos;
    }

    public override List<Casilla> CasillasDisponibles()
    {
        throw new System.NotImplementedException();
    }
}
