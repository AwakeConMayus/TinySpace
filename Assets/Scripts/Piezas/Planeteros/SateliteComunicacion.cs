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

    int puntosPorPlaneta = 2;
    int puntosPorPlanetasAlineados(Casilla c, int direccion)
    {
        print("direccion: " + direccion);
        int puntos = 0;
        if(casilla.pieza && casilla.pieza.GetComponent<Planetas>())
        {
            puntos += puntosPorPlaneta;
        }
        if (casilla.adyacentes[direccion] != null)
        {
            puntos += puntosPorPlanetasAlineados(casilla.adyacentes[direccion],direccion);
        }
        return puntos;
    }
}
