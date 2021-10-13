using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EstrategaMinerosMejorada : Efecto
{
    public override void Accion()
    {
        EstrategaMineros objetivo = (EstrategaMineros)casilla.pieza;
        ++objetivo.nivelInicial;

        objetivo.transform.localScale *= 2; //Visual

        if (this.gameObject.GetPhotonView().IsMine) EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public override List<Casilla> CasillasDisponibles()
    {
        print("jugador estratega mej:" + jugador);
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(jugador);
        return FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.estratega}, casillasDisponibles);
    }
}
