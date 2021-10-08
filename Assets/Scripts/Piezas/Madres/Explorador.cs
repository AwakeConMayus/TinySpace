using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Explorador : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.explorador;
    }

    public override int Puntos()
    {
        List<Clase> clasesExploradas = new List<Clase>();
        int puntosExploracion = 0;

        foreach(Casilla adyacete in casilla.adyacentes)
        {
            if (!adyacete || !adyacete.pieza) continue;

            if(!clasesExploradas.Contains(adyacete.pieza.clase) && !adyacete.pieza.CompareClase(Clase.explorador))
            {
                clasesExploradas.Add(adyacete.pieza.clase);
            }
        }
        puntosExploracion = clasesExploradas.Count * 2;

        return puntosExploracion;
    }
}
