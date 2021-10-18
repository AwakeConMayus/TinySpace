using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaSagrado : Planetas
{
    public override int Puntos()
    {
        int puntos = base.Puntos();
        int puntosExtra = 0;

        foreach(Casilla c in casilla.adyacentes)
        {
            if(c && c.pieza)
            {
                if (c.pieza.Get_Jugador() != jugador) return puntos;

                puntosExtra += 3;
            }
        }

        puntos += puntosExtra;

        return puntos;
    }

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillas = FiltroCasillas.CasillasDeUnJugador(jugador, referencia);
        casillas = FiltroCasillas.CasillasDeUnTipo(Clase.astros, casillas);

        return casillas;
    }
}
