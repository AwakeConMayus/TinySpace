using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono1Oyentes : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        tabBase = PonerMejorPlaneta(tabBase);

        PiezaIA planetaIA = Resources.Load<GameObject>("Planeta").GetComponent<PiezaIA>();

        nuevosEstados = planetaIA.Opcionificador(tabBase);        

        return nuevosEstados;
    }
}
