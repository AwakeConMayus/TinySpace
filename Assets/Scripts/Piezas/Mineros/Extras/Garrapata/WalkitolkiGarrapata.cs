﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WalkitolkiGarrapata : MonoBehaviourPunCallbacks
{
    public static WalkitolkiGarrapata instance;

    public int seleccion = 0;

    public GarrapataMinero mi_garrapata;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    private void Start()
    {
        EventManager.StartListening("ColocarPieza", Espiar);
    }
    public void Espiar()
    {
        if(seleccion == mi_garrapata.nivel)
        {
            base.photonView.RPC("RPC_Avisar", RpcTarget.All);
            seleccion = 0;
        }
    }

    [PunRPC]
    public void RPC_Avisar()
    {
        Avisar();
    }
    public void Avisar()
    {
        mi_garrapata.Chantaje();
    }
    public void Setear_Nivel(int i)
    {
        base.photonView.RPC("RPC_SetearNivel", RpcTarget.All, i);
    }
    [PunRPC]
    public void RPC_SetearNivel(int i)
    {
        mi_garrapata.nivel = i;
    }
}