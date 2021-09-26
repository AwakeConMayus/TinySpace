using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estratega : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.estratega;
    }

    public override int Puntos()
    {
        int puntos = 0;
        foreach(Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.combate))
            {
                puntos += 2;
            }
        }
        return puntos;
    }

    public override List<Casilla> CasillasDisponibles()
    {
        throw new System.NotImplementedException();
    }

}
