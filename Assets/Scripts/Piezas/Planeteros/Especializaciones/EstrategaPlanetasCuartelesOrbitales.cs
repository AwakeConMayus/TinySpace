using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaPlanetasCuartelesOrbitales : EstrategaPlanetas
{


    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.planeta, Clase.luna }, referencia);

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, true);

        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles, false);

        casillasDisponibles = FiltroCasillas.CasillasLibres(casillasDisponibles);

        return casillasDisponibles;
    }

    public override int Puntos()
    {
        int puntos = base.Puntos();

        int puntosColonizacion = 3;

        int colonizacion = 0;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.faccion == faccion)
                ++colonizacion;
            else --colonizacion;
        }


        if (colonizacion > 0) puntos += puntosColonizacion;


        return puntos;
    }

    protected override void SetClase()
    {
        clase = Clase.planeta;
    }
}
