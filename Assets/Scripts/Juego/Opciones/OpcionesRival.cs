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


    public void Primera_vez()
    {
        base.photonView.RPC("RPC_Primer_Orden", RpcTarget.Others, opcionesDisponibles[0], opcionesDisponibles[1], opcionesDisponibles[2], opcionesDisponibles[3], opcionesDisponibles[4]);
    }

    [PunRPC]
    public void RPC_Primer_Orden(int primera, int segunda, int tercera, int cuarta, int quinta)
    {
        opcionesDisponibles.Add(primera);
        opcionesDisponibles.Add(segunda);
        opcionesDisponibles.Add(tercera);
        opcionesDisponibles.Add(cuarta);
        opcionesDisponibles.Add(quinta);
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
