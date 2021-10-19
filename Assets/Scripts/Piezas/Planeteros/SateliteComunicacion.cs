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
    int porFavorParaYa = 0;
    int puntosPorPlanetasAlineados(Casilla c, int direccion)
    {
        ++porFavorParaYa;
        print("direccion: " + direccion);
        print(c.transform.position);
        int puntos = 0;
        if(c.pieza && c.pieza.GetComponent<Planetas>())
        {
            puntos += puntosPorPlaneta;
        }
        if (c.adyacentes[direccion] != null && porFavorParaYa < 100)
        {
            puntos += puntosPorPlanetasAlineados(c.adyacentes[direccion],direccion);
        }
        return puntos;
    }
}
