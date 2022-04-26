using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetas : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.planeta;
    }
    List<Casilla> tablero = null;
    public override int Puntos()
    {
        int puntosColonizacion = 3;

        int planetasTablero = 0;
        if (tablero == null) tablero = FiltroCasillas.TableroOriginal(casilla);
        foreach (Casilla c in tablero)
        {
            if (c.pieza && c.pieza.clase == Clase.planeta) ++planetasTablero;
        }

        puntosColonizacion = planetasTablero - 1;

        int colonizacion = 0;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.faccion == faccion)
                ++colonizacion;
            else --colonizacion;
        }


        if (colonizacion > 0) return puntosColonizacion;

        return 0;
    }

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> result = FiltroCasillas.CasillasLibres(referencia);
        List<Casilla> resta = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, referencia);
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
