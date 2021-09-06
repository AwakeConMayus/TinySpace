using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorMineros : Investigador
{

    public override List<Casilla> CasillasDisponibles()
    {
        return FiltroCasillas.CasillasLibres();
    }
}
