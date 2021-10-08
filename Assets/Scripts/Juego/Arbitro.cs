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

    int numeroPoder = 0;

    bool specialPhase = true;
    int specialTurno = 0;
    int turno = 0;

    bool inputActive = false;

    private void Start()
    {
        EventManager.StartListening("AccionTerminadaConjunta", NextTurnDoble);

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
        player.PrepararPreparacion();
        player.jugador = 0;
    }

    [PunRPC]
    public void RPC_SetNotInitial(int i)
    {
        active = specialActive = false;
        player = opciones[i];
        player.gameObject.SetActive(true);
        player.PrepararPreparacion();
        player.jugador = 1;

        print("¿Esta preparada la preparacion?");
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public void NextTurnDoble()
    {
        base.photonView.RPC("RPC_NextTurn", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_NextTurn()
    {
        if (specialPhase) SpecialTurn();
        else Turn();
    }

    void SpecialTurn()
    {
        bool estaVezToca = specialActive;

        if (!specialActive)
        {
            if (inputActive) SwitchActive();
        }
        else if (specialActive)
        {
            if (!inputActive) SwitchActive();
        }

        if (specialTurno % 2 != 0)
        {
            specialPhase = false;
        }
        if (++specialTurno % 2 != 0)
        {
            specialActive = !specialActive;
        }

        if (estaVezToca) 
        {
            switch (numeroPoder)
            {
                case 0:
                    player.poder.GetComponent<Poder>().InitialAction();
                    break;
                case 1:
                    player.poder.GetComponent<Poder>().FirstAction();
                    break;
                case 2:
                    player.poder.GetComponent<Poder>().SecondAction();
                    break;
                default:
                    Debug.Log("el turno de poder no esta de acorde");
                    break;
            }
            ++numeroPoder; 
        }
    }

    void Turn()
    {
        if(turno >= 19)
        {
            EndGame();
        }
        Debug.Log(active);
        Debug.Log(turno);
        if (!active)
        {
            if (inputActive) SwitchActive();
        }
        else if (active)
        {
            if (!inputActive) SwitchActive();
        }
        if((turno+1) % 10 == 0)
        {
            specialPhase = true;
        }
        if (++turno % 2 != 0)
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
