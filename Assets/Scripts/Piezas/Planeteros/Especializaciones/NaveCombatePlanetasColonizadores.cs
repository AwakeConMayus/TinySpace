using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCombatePlanetasColonizadores : NaveCombatePlanetas
{


    public override int Puntos()
    {
        int puntos = base.Puntos();

        foreach(Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.clase == Clase.astros)
            {
                ++puntos;
            }
        }

        return puntos;
    }
}
