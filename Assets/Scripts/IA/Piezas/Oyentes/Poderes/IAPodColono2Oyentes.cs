using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono2Oyentes : PoderIABase
{
    public override List<List<Casilla>> Opcionificador(List<Casilla> listaBase)
    {
        List<List<Casilla>> nuevosEstados = new List<List<Casilla>>();

        foreach (Casilla c in listaBase)
        {

            if (c.pieza.GetComponent<Planetas>())
            {
                Planetas sc = (Planetas)c.pieza;

                PlanetaSagrado nuevoPlaneta = new PlanetaSagrado();
                nuevoPlaneta.Set_Jugador(jugador);

                c.pieza = nuevoPlaneta;
                nuevoPlaneta.casilla = c;

                nuevosEstados.Add(listaBase);

                c.pieza = sc;
            }

        }

        return nuevosEstados;
    }
}
