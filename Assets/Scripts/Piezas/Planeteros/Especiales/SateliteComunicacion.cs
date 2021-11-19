using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteComunicacion : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.satelite;
    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasLibres(referencia);
        return casillasDisponibles;
    }

    public override int Puntos()
    {
        int puntos = 0;
        for (int i = 0; i < casilla.adyacentes.Length; i++)
        {
            puntos += puntosPorPlanetasAlineados(casilla.adyacentes[i], i);
        }
        return puntos;
    }

    int puntosPorPlaneta = 3;
    int puntosPorPlanetasAlineados(Casilla c, int direccion)
    {
        if (!c) return 0;
        int puntos = 0;
        if(c.pieza && (c.pieza.CompareClase(Clase.planeta)))
        {
            puntos += puntosPorPlaneta;
        }
        if (c.adyacentes[direccion] != null)
        {
            puntos += puntosPorPlanetasAlineados(c.adyacentes[direccion],direccion);
        }
        return puntos;
    }
}
