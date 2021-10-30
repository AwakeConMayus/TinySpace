using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono1Oyentes : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        Pieza piezaReferencia = Resources.Load<Pieza>("Planeta Planetarios");
        piezaReferencia.Set_Jugador(jugador);

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;

            PiezaIA ObtenerPiezaPoder = padre.opcionesIniciales[padre.opcionesDisponibles[0]].GetComponent<PiezaIA>();


            foreach(InfoTablero opcion in ObtenerPiezaPoder.Opcionificador(new InfoTablero(IATablero.instance.mapa)))
            {
                nuevosEstados.Add(opcion);
            }

            c.pieza = null;
        }

        return nuevosEstados;
    }
}
