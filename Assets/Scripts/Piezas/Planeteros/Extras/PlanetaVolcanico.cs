using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaVolcanico : Planetas
{

    public override int Puntos()
    {
        int puntos = base.Puntos();

        int puntosEnemigoAdyacente = 3;

        foreach (Casilla c in casilla.adyacentes)
        {
            if (c && c.pieza && c.pieza.faccion != faccion)
            {
                puntos += puntosEnemigoAdyacente;
            }
        }

        return puntos;
    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillas = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        casillas = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, casillas);

        return casillas;
    }
}
