using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EstrategaMinerosMejorada : Efecto
{
    [SerializeField] GameObject estratega_astro;
    public override void Accion()
    {
        if (!gameObject.GetPhotonView().IsMine) return;

        // Comprobacion de si el game se esta realizando online u offline
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            // Instanciacion que utiliza photon
            PhotonNetwork.Instantiate(estratega_astro.name, casilla.transform.position, Quaternion.identity);

        }
        else
        {
            casilla.Clear();
            // Instanciacion de piezas en el offline
            GameObject thisPieza = Instantiate(estratega_astro);
            thisPieza.transform.position = casilla.transform.position;
            //GestorTurnos.instance.realizarJugada();
        }
    }

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(jugador, referencia);
        return FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.estratega}, casillasDisponibles);
    }
}
