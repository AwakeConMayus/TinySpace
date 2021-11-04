using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EstrategaMinerosMejorada : Efecto
{
    [SerializeField] GameObject estratega_astro;
    public override void Accion()
    {
        
        // Comprobacion de si el game se esta realizando online u offline
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            // Instanciacion que utiliza photon
            PhotonNetwork.Instantiate(estratega_astro.name, casilla.transform.position, Quaternion.identity);

        }
        else if(!PhotonNetwork.InRoom)
        {
            Destroy(casilla.pieza.gameObject);
            // Instanciacion de piezas en el offline
            GameObject thisPieza = Instantiate(estratega_astro, casilla.transform.position, Quaternion.identity);
            thisPieza.transform.position = casilla.transform.position;
            //GestorTurnos.instance.realizarJugada();
        }
    }

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        return FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.estratega}, casillasDisponibles);
    }
}
