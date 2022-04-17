using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MejoraCombateMineros : Efecto
{
    public GameObject combateMejorado;

    public override void Accion()
    {
        // Comprobacion de si el game se esta realizando online u offline
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2 && GetComponent<PhotonView>().IsMine)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            // Instanciacion que utiliza photon
            GameObject thisPieza = PhotonNetwork.Instantiate(combateMejorado.name, casilla.transform.position, Quaternion.identity);
        }
        else if (!PhotonNetwork.InRoom)
        {
            casilla.Clear();
            // Instanciacion de piezas en el offline
            GameObject thisPieza = Instantiate(combateMejorado);
            thisPieza.transform.position = casilla.transform.position;
        }


    }


    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        casillasDisponibles = FiltroCasillas.CasillasDeUnTipo(Clase.combate, casillasDisponibles);
        foreach (Casilla c in casillasDisponibles)
        {
          
            if (c.pieza.gameObject.name == combateMejorado.name) casillasDisponibles.Remove(c);
        }
        return casillasDisponibles;
    }
}
