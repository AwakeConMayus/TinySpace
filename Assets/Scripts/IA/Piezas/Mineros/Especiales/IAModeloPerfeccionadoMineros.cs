using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAModeloPerfeccionadoMineros : PiezaIA
{
    [SerializeField] List<PiezaIA> piezasMejoradas;
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        foreach(PiezaIA pi in piezasMejoradas)
        {
            opciones.Add(pi.BestInmediateOpcion(tabBase));
        }

        return opciones;
    }
}
