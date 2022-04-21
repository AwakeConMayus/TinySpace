using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCombatePlanetasColonizadores : NaveCombatePlanetas
{


    public override int Puntos()
    {
        int puntosAstroAdyacente = 3;

        int puntos = base.Puntos();

        foreach(Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.CompareClase(new List<Clase> { Clase.planeta, Clase.luna, Clase.restos }))
            {
                puntos += puntosAstroAdyacente;
            }
        }

        return puntos;
    }
}
