using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrategaMinerosMejorada : Efecto
{
    public override void Accion()
    {
        EstrategaMineros objetivo = (EstrategaMineros)casilla.pieza;
        ++objetivo.nivel;
        EventManager.TriggerEvent("AccionTerminadaIndividual");
    }

    public override List<Casilla> CasillasDisponibles()
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(jugador);
        return FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.estratega}, casillasDisponibles);
    }
}
