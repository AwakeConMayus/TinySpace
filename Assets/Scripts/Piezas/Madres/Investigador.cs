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
        int puntosIniciales = 1;
        int incremento = 1;

        int puntos = 0;

        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.CompareClase(Clase.explorador))
            {
                print(adyacente.pieza.clase);
                puntos += puntosIniciales;
                puntosIniciales += incremento;
            }
        }
        return puntos;
    }
}
