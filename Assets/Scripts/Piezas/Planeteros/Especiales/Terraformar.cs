using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Terraformar : Efecto
{


    public override void Accion()
    {
        // Comprobacion de si el game se esta realizando online u offline
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            // Instanciacion que utiliza photon
            //PhotonNetwork.Instantiate(investigador_astro.name, casilla.transform.position, Quaternion.identity);

        }
        else if (!PhotonNetwork.InRoom)
        {
            casilla.Clear();
            // Instanciacion de piezas en el offline
            //GameObject thisPieza = Instantiate(investigador_astro);
            //thisPieza.transform.position = casilla.transform.position;
            //GestorTurnos.instance.realizarJugada();
        }


    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillas = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        casillas = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, casillas);

        return casillas;
    }
}
