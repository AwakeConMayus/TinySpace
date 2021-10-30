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
        int numPuntosCombateAliadas = 3;

        int puntos = 0;
        foreach(Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.CompareClase(Clase.combate) && adyacente.pieza.faccion == faccion)
            {
                puntos += numPuntosCombateAliadas;
            }
        }
        return puntos;
    }
}
