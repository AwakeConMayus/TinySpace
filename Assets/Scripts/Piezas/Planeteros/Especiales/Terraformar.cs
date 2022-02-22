using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Terraformar : Efecto
{

    static int tipo_a_terraformar;

    [SerializeField] GameObject sol;
    [SerializeField] GameObject hielo;
    [SerializeField] GameObject volcanico;
    [SerializeField] GameObject sagrado;
    public override void Accion()
    {
        Debug.Log(tipo_a_terraformar + " TIPO A TERRAFORMAR");
        // Comprobacion de si el game se esta realizando online u offline
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            // Instanciacion que utiliza photon
            switch (tipo_a_terraformar)
            {
                case 0:
                    PhotonNetwork.Instantiate(sol.name, casilla.transform.position, Quaternion.identity);
                    ++tipo_a_terraformar;
                    break;
                case 1:
                    PhotonNetwork.Instantiate(hielo.name, casilla.transform.position, Quaternion.identity);
                    ++tipo_a_terraformar;
                    break;
                case 2:
                    PhotonNetwork.Instantiate(volcanico.name, casilla.transform.position, Quaternion.identity);
                    ++tipo_a_terraformar;
                    break;
                case 3:
                    PhotonNetwork.Instantiate(sagrado.name, casilla.transform.position, Quaternion.identity);
                    tipo_a_terraformar = 0;
                    break;
                default:
                    Debug.LogError("El int encargado de la terraformacion se ha salido fuera de los parametros esperados 0-3");
                    break;
            }

            //PhotonNetwork.Instantiate(investigador_astro.name, casilla.transform.position, Quaternion.identity);

        }
        else if (!PhotonNetwork.InRoom)
        {
            casilla.Clear();
            switch (tipo_a_terraformar)
            {
                case 0:
                    Instantiate(sol, casilla.transform.position, Quaternion.identity);
                    ++tipo_a_terraformar;
                    break;
                case 1:
                    Instantiate(hielo, casilla.transform.position, Quaternion.identity);
                    ++tipo_a_terraformar;
                    break;
                case 2:
                    Instantiate(volcanico, casilla.transform.position, Quaternion.identity);
                    ++tipo_a_terraformar;
                    break;
                case 3:
                    Instantiate(sagrado, casilla.transform.position, Quaternion.identity);
                    tipo_a_terraformar = 0;
                    break;
                default:
                    Debug.LogError("El int encargado de la terraformacion se ha salido fuera de los parametros esperados 0-3");
                    break;
            }
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
