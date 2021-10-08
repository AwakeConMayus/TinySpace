using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EstrategaMinerosMejorada : Efecto
{
    public override void Accion()
    {
        EstrategaMineros objetivo = (EstrategaMineros)casilla.pieza;
        ++objetivo.nivel;

        if (this.gameObject.GetPhotonView().IsMine) EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public override List<Casilla> CasillasDisponibles()
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(jugador);
        return FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.estratega}, casillasDisponibles);
    }
}
