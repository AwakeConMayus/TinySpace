using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono1Oyentes : PoderIABase
{
    public override List<List<Casilla>> Opcionificador(List<Casilla> listaBase)
    {
        List<List<Casilla>> nuevosEstados = new List<List<Casilla>>();

        Pieza piezaReferencia = Resources.Load<Pieza>("Planeta Planetarios");
        piezaReferencia.Set_Jugador(jugador);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(listaBase))
        {
            Pieza piezaColocar = piezaReferencia;
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;

            PiezaIA ObtenerPiezaPoder = padre.opcionesIniciales[padre.opcionesDisponibles[0]].GetComponent<PiezaIA>();

            foreach(List<Casilla> opcion in ObtenerPiezaPoder.Opcionificador(listaBase))
            {
                nuevosEstados.Add(Auxiliar.Copy(listaBase));
            }

            c.pieza = null;
        }

        return nuevosEstados;
    }
}
