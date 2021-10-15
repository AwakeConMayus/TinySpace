using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorMineroMejorado : ExploradorMinero
{

    public override int Puntos()
    {
        int numPuntosPorClase = 4;

        List<Clase> clasesExploradas = new List<Clase>();
        int puntosExploracion = 0;

        foreach (Casilla adyacete in casilla.adyacentes)
        {
            if (!adyacete || !adyacete.pieza) continue;

            clasesExploradas.Add(adyacete.pieza.clase);
        }

        int repeticionesMaximas = 0;
        foreach(Clase c in clasesExploradas)
        {
            int repeticionesClaseActual = 0;
            foreach(Clase cc in clasesExploradas)
            {
                if (c == cc) ++repeticionesClaseActual;
            }
            if (repeticionesClaseActual > repeticionesMaximas) repeticionesMaximas = repeticionesClaseActual;
        }

        puntosExploracion = repeticionesMaximas * numPuntosPorClase;

        return puntosExploracion;
    }
}
