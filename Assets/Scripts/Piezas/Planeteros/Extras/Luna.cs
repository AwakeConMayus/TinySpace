using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luna : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.luna;
    }
    public override int Puntos()
    {
        int puntos = 0;

        foreach(Casilla c in casilla.adyacentes)
        {
            if (c && c.pieza && c.pieza.clase == Clase.planeta) puntos++;
        }

        return puntos;
    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        for (int i = casillasDisponibles.Count-1; i >= 0; i--)
        {
            if (!casillasDisponibles[i].pieza.gameObject.GetComponent<Planetas>())
            {
                casillasDisponibles.Remove(casillasDisponibles[i]);
            }
        }
        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, true);
        casillasDisponibles = FiltroCasillas.CasillasLibres(casillasDisponibles);


        return casillasDisponibles;
    }
}
