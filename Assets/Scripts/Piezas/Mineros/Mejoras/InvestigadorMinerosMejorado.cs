using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigadorMinerosMejorado : InvestigadorMineros
{
    public override bool Anulador()
    {
        return false;
    }
     
    public override int Puntos()
    {
        int limite = 3;
        int puntos = 0;
        int incremento = 0;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.explorador))
            {
                if (incremento < limite)
                {
                    ++incremento;
                    puntos += incremento;
                }
            }
        }
        return puntos;
    }
}
