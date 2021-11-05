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
            Debug.Log("finales: " + puntos);
        }
        return puntos;
    }

    int puntosPorPlaneta = 2;
        //Clanta: este int es prueba definitiva de la frustracion de mikel
    int puntosPorPlanetasAlineados(Casilla c, int direccion)
    {
        int puntos = 0;
        if(c.pieza && (c.pieza.GetComponent<Planetas>() || c.pieza.GetComponent<PlanetaSagrado>()))
        {
            puntos += puntosPorPlaneta;
            Debug.Log("puntos añadidos" + puntos);
        }
        if (c.adyacentes[direccion] != null)
        {
            puntos += puntosPorPlanetasAlineados(c.adyacentes[direccion],direccion);
        }
        return puntos;
    }
}
