using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Estratega : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.estratega;
    }

    public override int Puntos()
    {
        int puntosCombateCercano = 3;

        int puntos = 0;
        foreach(Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.combate))
            {
                puntos += puntosCombateCercano;
            }
        }
        return puntos;
    }
}
