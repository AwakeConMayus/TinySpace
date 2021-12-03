using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaHelado : Planetas
{
    public override int Puntos()
    {
        int puntos = base.Puntos();

        int numPuntosPorClase = 2;

        List<Clase> clasesExploradas = new List<Clase>();
        int puntosExploracion = 0;

        foreach (Casilla adyacete in casilla.adyacentes)
        {
            if (!adyacete || !adyacete.pieza) continue;

            if (!clasesExploradas.Contains(adyacete.pieza.clase))
            {
                clasesExploradas.Add(adyacete.pieza.clase);
            }
        }
        puntosExploracion = clasesExploradas.Count * numPuntosPorClase;

        puntos += puntosExploracion;

        return puntos;
    }

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillas = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        casillas = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, casillas);

        return casillas;
    }
}
