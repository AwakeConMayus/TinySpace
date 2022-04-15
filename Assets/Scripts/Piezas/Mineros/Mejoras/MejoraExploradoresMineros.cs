using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class MejoraExploradoresMineros : Efecto
{
    public GameObject exploradorMejorado;

    public override void Accion()
    {
        // Comprobacion de si el game se esta realizando online u offline
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2 && GetComponent<PhotonView>().IsMine)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            // Instanciacion que utiliza photon
            GameObject thisPieza = PhotonNetwork.Instantiate(exploradorMejorado.name, casilla.transform.position, Quaternion.identity);
        }
        else if (!PhotonNetwork.InRoom)
        {
            casilla.Clear();
            // Instanciacion de piezas en el offline
            GameObject thisPieza = Instantiate(exploradorMejorado);
            thisPieza.transform.position = casilla.transform.position;
        }


    }


    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        FiltroCasillas.CasillasDeUnTipo(Clase.explorador, casillasDisponibles);
        foreach(Casilla c in casillasDisponibles)
        {
            if (c.pieza.gameObject.name == exploradorMejorado.name) casillasDisponibles.Remove(c);
        }
        return casillasDisponibles;
    }
}
