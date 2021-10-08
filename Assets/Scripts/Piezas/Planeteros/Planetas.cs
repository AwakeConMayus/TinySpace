using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetas : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.astros;
    }

    public override int Puntos()
    {
        int colonizacion = 0;

        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.jugador == jugador)
                ++colonizacion;
            else --colonizacion;
        }


        if (colonizacion > 0) return 3;

        return 0;
    }

    public override List<Casilla> CasillasDisponibles()
    {
        List<Casilla> result = FiltroCasillas.CasillasLibres();
        List<Casilla> resta = FiltroCasillas.CasillasDeUnTipo(Clase.astros);
        resta = FiltroCasillas.CasillasAdyacentes(resta, true);
        result = FiltroCasillas.RestaLista(result, resta);
        return result;
    }
}
