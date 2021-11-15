using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaPlanetasCuartelesOrbitales : EstrategaPlanetas
{


    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, referencia);

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, true);

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, false);

        casillasDisponibles = FiltroCasillas.CasillasLibres(casillasDisponibles);

        return casillasDisponibles;
    }

    protected override void SetClase()
    {
        clase = Clase.planeta;
    }
}
