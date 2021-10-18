using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorPlanetas : Investigador
{

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.astros }, referencia);

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, true);

        casillasDisponibles = FiltroCasillas.CasillasLibres(casillasDisponibles);

        return casillasDisponibles;

    }

}
