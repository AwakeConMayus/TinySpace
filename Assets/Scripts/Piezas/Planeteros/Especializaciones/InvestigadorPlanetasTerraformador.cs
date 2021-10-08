using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorPlanetasTerraformador : InvestigadorPlanetas
{

    public override int Puntos()
    {
        int limite = 3;
        int puntos = 0;
        int incremento = 0;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.clase == Clase.explorador || adyacente.pieza.clase == Clase.astros)
            {
                foreach (Casilla adyacente2 in adyacente.adyacentes)
                {
                    if (adyacente2 && adyacente2 != casilla && adyacente2.pieza && adyacente2.pieza.clase == Clase.investigador && adyacente2.pieza.Get_Jugador() == jugador)
                    {
                        return 0;
                    }

                    if (incremento < limite)
                    {
                        ++incremento;
                        puntos += incremento;
                    }
                }
            }
        }
        return puntos;
    }
}
