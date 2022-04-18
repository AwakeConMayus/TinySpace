using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCombatePlanetas : NaveCombate
{

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.planeta, Clase.luna, Clase.restos }, referencia);

        casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(Faccion.oyente, casillasDisponibles);

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, true);

        casillasDisponibles = FiltroCasillas.CasillasLibres(casillasDisponibles);

        return casillasDisponibles;
    }
}
