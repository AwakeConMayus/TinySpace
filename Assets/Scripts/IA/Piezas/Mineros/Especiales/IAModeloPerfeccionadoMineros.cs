using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAModeloPerfeccionadoMineros : PiezaIA
{
    public IAOpcionesMineros padre;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        PiezaIA pieza;
        if(padre.opcionesDisponibles[0] != 4)
        {
            pieza = padre.mejoras[padre.opcionesDisponibles[0]].GetComponent<PiezaIA>();
        }
        else
        {
            pieza = padre.mejoras[padre.opcionesDisponibles[1]].GetComponent<PiezaIA>();
        }

        return pieza.Opcionificador(tabBase);
    }
}
