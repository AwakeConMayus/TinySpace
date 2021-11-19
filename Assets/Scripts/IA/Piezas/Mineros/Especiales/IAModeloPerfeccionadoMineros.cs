using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAModeloPerfeccionadoMineros : PiezaIA
{
    public IAOpcionesMineros padre1;
    public OpcionesMineros padre2;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        PiezaIA pieza = null;

        if(padre1 != null)
        {
            if (padre1.opcionesDisponibles[0] != 4)
            {
                pieza = padre1.mejoras[padre1.opcionesDisponibles[0]].GetComponent<PiezaIA>();
            }
            else
            {
                pieza = padre1.mejoras[padre1.opcionesDisponibles[1]].GetComponent<PiezaIA>();
            }
        }
        else if (padre2 != null)
        {
            if (padre2.opcionesDisponibles[0] != 4)
            {
                pieza = padre2.mejoras[padre2.opcionesDisponibles[0]].GetComponent<PiezaIA>();
            }
            else
            {
                pieza = padre2.mejoras[padre2.opcionesDisponibles[1]].GetComponent<PiezaIA>();
            }
        }

        return pieza.Opcionificador(tabBase);
    }
}
