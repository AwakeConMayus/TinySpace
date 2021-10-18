using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorMineros : Investigador
{

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        return FiltroCasillas.CasillasLibres(referencia);
    }
}
