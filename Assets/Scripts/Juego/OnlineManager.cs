using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineManager : MonoBehaviourPunCallbacks
{

    public static OnlineManager instance;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this.gameObject);
    }


    public void Destroy_This_Pieza(Pieza p)
    {
        if (!p)
        {
            Debug.Log("la pieza a destruir no existe"); return;
        }
        if (!p.casilla)
        {
            Debug.Log("la pieza a destruir no tiene casilla"); return;
        }
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (p.gameObject.GetPhotonView() && p.gameObject.GetPhotonView().IsMine)
            {
                PhotonNetwork.Destroy(p.gameObject);
            }
            else
            {
                int numero = Tablero.instance.Get_Numero_Casilla(p.casilla.gameObject);
                base.photonView.RPC("RPC_Destroy_this_Pieza", RpcTarget.Others, numero);
            }
        }
        else
        {
            p.casilla.Clear();
        }
    }
    [PunRPC]
    public void RPC_Destroy_this_Pieza(int casilla)
    {
        Pieza p = Tablero.instance.mapa[casilla].pieza;
        PhotonNetwork.Destroy(p.gameObject);
    }

}
