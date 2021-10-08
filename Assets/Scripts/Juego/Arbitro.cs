using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class Arbitro : MonoBehaviourPunCallbacks
{

    //AVISO: El gameobject en el que vaya este script tiene que tener un photon view o dara error
    public List<Opciones> opciones;
    Opciones player;
    bool active;
    bool specialActive;

    bool specialPhase = true;
    int turno0 = 0;
    int turno1 = 0;

    bool inputActive = false;

    private void Start()
    {
        EventManager.StartListening("AccionTerminada", NextTurn);

        if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
        {
            SetInitial();
        }
    }

    public void SetInitial()
    {
        active = specialActive = true;
        switch (Random.Range(0, 2))
        {
            case 0:
                player = opciones[0];
                base.photonView.RPC("RPC_SetNotInitial", RpcTarget.Others, 1);
                break;
            case 1:
                player = opciones[1];
                base.photonView.RPC("RPC_SetNotInitial", RpcTarget.Others, 0);
                break;
        }
        player.gameObject.SetActive(true);
        player.jugador = 0;
    }

    [PunRPC]
    public void RPC_SetNotInitial(int i)
    {
        active = specialActive = false;
        player = opciones[i];
        player.gameObject.SetActive(true);
        player.jugador = 1;
    }

    public void NextTurn()
    {
        if (specialPhase) SpecialTurn();
        else Turn();
    }

    void SpecialTurn()
    {
        if (!specialActive)
        {
            if (inputActive) SwitchActive();
            return;
        }
        if(turno0 % 2 != 0)
        {
            specialPhase = false;
        }
        if(++turno0 % 2 != 0)
        {
            specialActive = !specialActive;
        }
    }

    void Turn()
    {
        if(turno1 >= 19)
        {
            EndGame();
        }
        if (!active)
        {
            if (inputActive) SwitchActive();
            return;
        }
        if((turno1+1) % 10 == 0)
        {
            specialPhase = true;
        }
        if (++turno1 % 2 == 0)
        {
            active = !active;
        }
    }

    void SwitchActive()
    {
        inputActive = player.active = !inputActive;
    }

    void EndGame()
    {
        Debug.Log("NO HAY FINAL, EMPIEZAN LOS ERRORES :)");
    }
}
