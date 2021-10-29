using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class OpcionesRivalMineros : OpcionesRival
{

    public int mineral = 0;


    public override void Rotar(int i)
    {
        base.Rotar(i);
        mineral =  poder.GetComponent<OpcionesMineros>().mineral;
        base.photonView.RPC("RPC_Mandar_Mineral", RpcTarget.Others, mineral);
    }

    [PunRPC]
    public void RPC_Mandar_Mineral(int i)
    {
        mineral = i;
        EventManager.TriggerEvent("CambioMineral");
    }

}
