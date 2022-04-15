using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExpMejMineros : PiezaIA
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();
        MejoraExploradoresMineros piezaReferencia = GetComponent<MejoraExploradoresMineros>();
        Pieza piezaColocar = piezaReferencia.exploradorMejorado.GetComponent<Pieza>();
        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
           
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
        }

        return nuevosEstados;
    }
}
