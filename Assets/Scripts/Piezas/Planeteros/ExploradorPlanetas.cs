using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorPlanetas : Explorador
{


    public override List<Casilla> CasillasDisponibles()
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnTipo (new List<Clase> { Clase.astros });

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, true);

        casillasDisponibles = FiltroCasillas.CasillasLibres(casillasDisponibles);

        return casillasDisponibles;

    }
}
