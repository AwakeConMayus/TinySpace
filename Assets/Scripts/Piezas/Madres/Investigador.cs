using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Investigador : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.investigador;
    }    

    public override int Puntos()
    {
        int puntos = 0;
        int incremento = 1;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.CompareClase(Clase.explorador))
            {
                puntos += incremento;
                incremento += 1;
            }
        }
        return puntos;
    }
}
