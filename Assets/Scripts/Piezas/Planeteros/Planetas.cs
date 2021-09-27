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
        int[] players = new int[2];

        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            ++players[adyacente.pieza.jugador];
        }

        int diferencia = players[1] - players[0];

        if (diferencia < 0) jugador = 0;
        else if (diferencia > 0) jugador = 1;
        else return 0;

        return 3;
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
