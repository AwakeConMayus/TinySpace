using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAComMineros : PiezaIA
{
    public override List<List<Casilla>> Opcionificador(List<Casilla> listaBase)
    {
        Pieza piezaReferencia;
        List<List<Casilla>> nuevosEstados = new List<List<Casilla>>();
        piezaReferencia = new NaveCombateMineros();
        piezaReferencia.Set_Jugador(jugador);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(listaBase))
        {
            Pieza piezaColocar = new NaveCombateMineros();
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new List<Casilla>(listaBase));
            c.pieza = null;
        }

        return nuevosEstados;
    }
}
