using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExpMejMineros : PiezaIA
{
    public override List<List<Casilla>> Opcionificador(List<Casilla> listaBase)
    {
        Pieza piezaReferencia;
        List<List<Casilla>> nuevosEstados = new List<List<Casilla>>();
        piezaReferencia = Resources.Load<Pieza>("Explorador Mineros Mejorado");
        piezaReferencia.Set_Jugador(jugador);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(listaBase))
        {
            Pieza piezaColocar = Resources.Load<Pieza>("Explorador Mineros Mejorado");
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new List<Casilla>(Auxiliar.Copy(listaBase)));
            c.pieza = null;
        }

        return nuevosEstados;
    }
}
