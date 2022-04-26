using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono1Oyentes : PoderIABase
{
    [SerializeField] PiezaIA planetaIA;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        tabBase = PonerMejorPlaneta(tabBase);        

        nuevosEstados = planetaIA.Opcionificador(tabBase);        

        return nuevosEstados;
    }
}
