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
        int puntosColonizacion = 3;

        int colonizacion = 0;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.Get_Jugador() == jugador)
                ++colonizacion;
            else --colonizacion;
        }


        if (colonizacion > 0) return puntosColonizacion;

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
    public override void Colocar(Casilla c)
    {
        base.Colocar(c);
        c.SetState(States.planeta);
    }
}
