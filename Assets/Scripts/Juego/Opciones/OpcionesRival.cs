using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OpcionesRival : MonoBehaviourPunCallbacks
{


    public GameObject poder;
    public GameObject[] opcionesIniciales = new GameObject[5];


    [HideInInspector]
    public List<int> opcionesDisponibles = new List<int>();


    public void Primera_vez(/*int opcion1, int opcion2, int opcion3, int opcion4, int */ List<int> disponible)
    {

        base.photonView.RPC("RPC_Primer_Orden", RpcTarget.Others,disponible );
    }

    [PunRPC]
    public void RPC_Primer_Orden(/*int primera, int segunda, int tercera, int cuarta, int quinta*/List<int> disponibles)
    {
        /*opcionesDisponibles.Add(primera);
        opcionesDisponibles.Add(segunda);
        opcionesDisponibles.Add(tercera);
        opcionesDisponibles.Add(cuarta);
        opcionesDisponibles.Add(quinta);*/
        opcionesDisponibles = disponibles;
    }

    public void Rotar(int i)
    {
        base.photonView.RPC("RPC_Rotar", RpcTarget.Others, i);
    }

    [PunRPC]
    public void RPC_Rotar(int i)
    {
        if (i < 0) return;
        int aux = opcionesDisponibles[i];
        opcionesDisponibles.Remove(aux);
        opcionesDisponibles.Add(aux);
        i = -1;
        EventManager.TriggerEvent("RotacionOpciones");
    }

}
