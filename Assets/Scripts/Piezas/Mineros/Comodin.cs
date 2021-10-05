using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Comodin : EfectoEspecial
{
    public override void Accion()
    {
        if (PhotonNetwork.InRoom)
        {
            if(GetComponent<PhotonView>().IsMine)
            MenuComodin.instance.Convocar(casilla);
        }
        else
        {
            MenuComodin.instance.Convocar(casilla);
        }
    }

    public override List<Casilla> CasillasDisponibles()
    {
        return FiltroCasillas.CasillasLibres();
    }
}
