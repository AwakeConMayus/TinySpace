using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaPlanetasCuartelesOrbitales : EstrategaPlanetas
{


    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasLibres(referencia);

        return casillasDisponibles;
    }

    public override int Puntos()
    {
        int puntos = base.Puntos();

        for (int i = 0; i < casilla.adyacentes.Length; i++)
        {
            puntos += puntosPorPlanetasAlineados(casilla.adyacentes[i], i);
        }
        return puntos;
    }

    protected override void SetClase()
    {
        clase = Clase.estratega;
    }
    int puntosPorPlaneta = 2;
    int puntosPorPlanetasAlineados(Casilla c, int direccion)
    {
        if (!c) return 0;
        int puntos = 0;
        if (c.pieza && (c.pieza.CompareClase(Clase.planeta)))
        {
            puntos += puntosPorPlaneta;
        }
        if (c.adyacentes[direccion] != null)
        {
            puntos += puntosPorPlanetasAlineados(c.adyacentes[direccion], direccion);
        }
        return puntos;
    }
}
