using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorMineroMejorado : ExploradorMinero
{

    public override int Puntos()
    {
        int puntosBase = base.Puntos();

        int numPuntosPorClase = 2;

        List<Clase> clasesExploradas = new List<Clase>();
        int puntosExploracion = 0;

        List<Casilla> casillasRango = FiltroCasillas.CasillasEnRango(2, casilla, false);
        foreach (Casilla adyacete in casillasRango)
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

        return puntosExploracion + puntosBase;
    }
}
