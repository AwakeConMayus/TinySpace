using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodMaquinista1Mineros : PoderIABase
{
    public override List<List<Casilla>> Opcionificador(List<Casilla> listaBase)
    {
        List<List<Casilla>> nuevosEstados = new List<List<Casilla>>();

        List<Casilla> lc1 = new List<Casilla>(listaBase);

        List<Casilla> posiblesMovimientos1 = FiltroCasillas.CasillasDeUnJugador(jugador, listaBase);
        List<Casilla> posiblesDestinos1 = FiltroCasillas.CasillasLibres(listaBase);

        foreach (Casilla c1 in posiblesMovimientos1)
        {
            foreach (Casilla cc1 in posiblesDestinos1)
            {
                listaBase = lc1;

                cc1.pieza = c1.pieza;
                c1.pieza = null;

                List<Casilla> lc2 = new List<Casilla>(listaBase);

                List<Casilla> posiblesMovimientos2 = FiltroCasillas.CasillasDeUnJugador(jugador, listaBase);
                List<Casilla> posiblesDestinos2 = FiltroCasillas.CasillasLibres(listaBase);

                foreach (Casilla c2 in posiblesMovimientos2)
                {
                    foreach (Casilla cc2 in posiblesDestinos2)
                    {
                        listaBase = lc2;

                        cc2.pieza = c2.pieza;
                        c2.pieza = null;

                        nuevosEstados.Add(new List<Casilla>(listaBase));
                    }
                }
            }
        }

        return nuevosEstados;
    }
} 

