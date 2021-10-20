using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WalkitolkiGarrapata : MonoBehaviourPunCallbacks
{
    public static WalkitolkiGarrapata instance;

    public int seleccion = 0;
    private int posicion = 0;

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
        Debug.Log("espio");
        Debug.Log("Seleccion: " + seleccion);
        Debug.Log("Posicion: " + posicion);
        if(seleccion == posicion)
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
        Debug.Log("aviso");
        mi_garrapata.Chantaje();
    }
    public void Setear_Nivel(int i)
    {
        base.photonView.RPC("RPC_SetearNivel", RpcTarget.All, i);
    }
    [PunRPC]
    public void RPC_SetearNivel(int i)
    {
        Debug.Log("Garrapata nivel:" + i);
        mi_garrapata.nivel = i;
        switch (i)
        {
            case 1:
                posicion = 3;
                break;
            case 2:
                posicion = 2;
                break;
            case 3:
                posicion = 1;
                break;
        }
    }
}
