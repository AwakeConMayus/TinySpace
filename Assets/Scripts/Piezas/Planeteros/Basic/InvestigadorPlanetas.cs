using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorPlanetas : Investigador
{

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, referencia);

        casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(Faccion.oyente, casillasDisponibles);

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, true);

        casillasDisponibles = FiltroCasillas.CasillasLibres(casillasDisponibles);

        return casillasDisponibles;

    }

}
