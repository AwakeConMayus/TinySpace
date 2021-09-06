using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaPlanetasCuartelesOrbitales : EstrategaPlanetas
{


    public override List<Casilla> CasillasDisponibles()
    {
        List<Casilla> casillasDisponibles = base.CasillasDisponibles();

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, false);

        casillasDisponibles = FiltroCasillas.CasillasLibres(casillasDisponibles);

        return casillasDisponibles;
    }
}
