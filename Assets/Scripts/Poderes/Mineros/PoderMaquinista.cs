using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderMaquinista : PoderMineros
{
    Casilla origen;
    Casilla destino;
    GameObject pieza;

    bool selectOrigen = false;
    bool selectDestino = false;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", SelectOrigen);
        EventManager.StartListening("ClickCasilla", SelectDestino);
    }

   
    public override void FirstAction()
    {
        List<Casilla> casillasPosibles = FiltroCasillas.CasillasDeUnJugador(jugador);

        ColorearCasillas.instance.initialColor();
        foreach (Casilla casilla in casillasPosibles) ColorearCasillas.instance.reColor("green", casilla);

        selectOrigen = true;
    }

    public override void SecondAction()
    {
        FirstAction();
    }

    void SelectOrigen ()
    {
        if (!selectOrigen) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = FiltroCasillas.CasillasDeUnJugador(jugador);
        if (casillasPosibles.Contains(c))
        {
            pieza = c.pieza.gameObject;
            origen = c;
            selectOrigen = false;
            FirstAction2();
        }
    }
    void FirstAction2()
    {
        List<Casilla> casillasPosibles = FiltroCasillas.CasillasLibres();

        ColorearCasillas.instance.initialColor();
        foreach (Casilla casilla in casillasPosibles) ColorearCasillas.instance.reColor("green", casilla);

        selectDestino = true;
    }
    void SelectDestino()
    {
        if (!selectDestino) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = FiltroCasillas.CasillasLibres();
        if (casillasPosibles.Contains(c))
        {
            destino = c;
            selectDestino = false;
            Teleport();
        }
    }
    void Teleport()
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (pieza.GetPhotonView().IsMine)
            {
                pieza.transform.position = destino.transform.position;
            }
            else
            {
                int i = Tablero.instance.Get_Numero_Casilla(origen.gameObject);
                int j = Tablero.instance.Get_Numero_Casilla(destino.gameObject);
                Debug.Log(base.photonView);
                base.photonView.RPC("RPC_Move_FromC_ToC1", RpcTarget.Others, i, j);
            }
        }
        else
        {
            pieza.transform.position = destino.transform.position;
        }

        ColorearCasillas.instance.initialColor();
    }
}
