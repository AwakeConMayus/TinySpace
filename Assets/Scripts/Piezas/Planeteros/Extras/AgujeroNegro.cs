using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgujeroNegro : Pieza
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
        List<Casilla> posibles_lugares = FiltroCasillas.CasillasDeUnTipo(Clase.investigador);
        posibles_lugares = FiltroCasillas.CasillasDeUnJugador(faccion, posibles_lugares);
        posibles_lugares = FiltroCasillas.CasillasAdyacentes(posibles_lugares, true);
        posibles_lugares = FiltroCasillas.CasillasLibres(posibles_lugares);
        return posibles_lugares;
    }
}
