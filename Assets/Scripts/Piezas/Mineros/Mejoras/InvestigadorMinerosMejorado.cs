using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorMinerosMejorado : InvestigadorMineros
{
    public override int Puntos()
    {
        int puntos = 0;
        int incremento = 0;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.explorador) && adyacente.pieza.Get_Jugador() == jugador)
            {
                puntos += incremento;
                incremento += 4;
            }
        }
        return puntos;
    }
}
