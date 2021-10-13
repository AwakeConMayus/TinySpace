using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorMinerosMejorado : InvestigadorMineros
{
    public override int Puntos()
    {
        int puntosIniciales = 2;
        int incremento = 3;

        int puntos = 0;

        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.explorador) && adyacente.pieza.Get_Jugador() != jugador)
            {
                puntos += puntosIniciales;
                puntosIniciales += incremento;
            }
        }
        return puntos;
    }
}
