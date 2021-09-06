using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCombateMineros : NaveCombate
{
    // Start is called before the first frame update

    public override List<Casilla> CasillasDisponibles()
    {
        return FiltroCasillas.CasillasLibres();
    }
}
