using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InvestigadorMinerosMejorado : Efecto
{

    [SerializeField] GameObject investigador_astro;

    public override void Accion()
    {
        // Comprobacion de si el game se esta realizando online u offline
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2 && GetComponent<PhotonView>().IsMine)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            // Instanciacion que utiliza photon
            GameObject thisPieza = PhotonNetwork.Instantiate(investigador_astro.name, casilla.transform.position, Quaternion.identity);
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();


        }
        else if(!PhotonNetwork.InRoom)
        {
            casilla.Clear();
            // Instanciacion de piezas en el offline
            GameObject thisPieza = Instantiate(investigador_astro);
            thisPieza.transform.position = casilla.transform.position;
            //GestorTurnos.instance.realizarJugada();
        }


    }


    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        return FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.investigador }, casillasDisponibles);
    }
}
