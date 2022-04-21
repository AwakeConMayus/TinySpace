using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorPlanetasTerraformador : InvestigadorPlanetas
{

    public override int Puntos()
    {
        int puntosIniciales = 0;
        int incremento = 2;

        int puntos = 0;

        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(new List<Clase> { Clase.planeta, Clase.luna, Clase.explorador, Clase.restos }))
            {
                puntos += puntosIniciales;
                puntosIniciales += incremento;
            }
        }
        return puntos;
    }
}
