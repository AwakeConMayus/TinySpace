using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono1Oyentes : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        PiezaIA piezaReferencia = Resources.Load<PiezaIA>("Planeta Planetarios");

        IATablero.instance.PrintInfoTablero(tabBase);

        InfoTablero newTabBase = piezaReferencia.BestInmediateOpcion(tabBase);

        PiezaIA ObtenerPiezaPoder = padre.opcionesIniciales[padre.opcionesDisponibles[0]].GetComponent<PiezaIA>();

        nuevosEstados = ObtenerPiezaPoder.Opcionificador(newTabBase);        

        return nuevosEstados;
    }
}
