using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luna : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.astros;
    }
    public override int Puntos()
    {
        return 0;
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
