using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodMaquinista1Mineros : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);

        List<Casilla> posiblesMovimientos1 = FiltroCasillas.CasillasDeUnJugador(faccion, IATablero.instance.mapa);
        List<Casilla> posiblesDestinos1 = FiltroCasillas.CasillasLibres(IATablero.instance.mapa);

        foreach (Casilla c1 in posiblesMovimientos1)
        {
            foreach (Casilla cc1 in posiblesDestinos1)
            {
                IATablero.instance.PrintInfoTablero(tabBase);

                cc1.pieza = c1.pieza;
                c1.pieza = null;

                InfoTablero estadoIntermedio = new InfoTablero(IATablero.instance.mapa);

                List<Casilla> posiblesMovimientos2 = FiltroCasillas.CasillasDeUnJugador(faccion, IATablero.instance.mapa);
                List<Casilla> posiblesDestinos2 = FiltroCasillas.CasillasLibres(IATablero.instance.mapa);

                foreach (Casilla c2 in posiblesMovimientos2)
                {
                    foreach (Casilla cc2 in posiblesDestinos2)
                    {
                        IATablero.instance.PrintInfoTablero(estadoIntermedio);

                        cc2.pieza = c2.pieza;
                        c2.pieza = null;

                        nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
                    }
                }
            }
        }

        return nuevosEstados;
    }
} 

