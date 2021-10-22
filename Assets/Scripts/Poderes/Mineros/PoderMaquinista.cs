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

    public int numeroTps;
    int tps;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", SelectOrigen);
        EventManager.StartListening("ClickCasilla", SelectDestino);
        EventManager.StartListening("DesClickCasilla", Cancel);
    }

   
    public override void FirstAction()
    {
        List<Casilla> casillasPosibles = FiltroCasillas.CasillasDeUnJugador(jugador);

        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);

        selectOrigen = true;

    }

    public override void SecondAction()
    {
        FirstAction();
    }

    public void Cancel()
    {
        if (!selectDestino && !selectOrigen) return;
        selectDestino = false;
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

        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);

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
            ++tps;
            if (tps < numeroTps) Invoke("FirstAction", 1f);
            else
            {
                tps = 0;
                EventManager.TriggerEvent("AccionTerminadaConjunta");
            }
        }
    }
    void Teleport()
    {

        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (pieza.GetPhotonView().IsMine)
            {
                pieza.GetComponent<Pieza>().Set_Pieza_Extra();
                pieza.transform.position = destino.transform.position;
            }
            else
            {
                //Clanta: Este poder no deberia poder mover naves enemigas por eso he comentado este codigo
                Debug.LogError("Poder Maquinista ha intentado mover una pieza que no era suya");
                return;
                //si todo sale bien esto no tendria que pasar porque el minero solo mueve piezas aliadas;
                //int i = Tablero.instance.Get_Numero_Casilla(origen.gameObject);
                //int j = Tablero.instance.Get_Numero_Casilla(destino.gameObject);
                //base.photonView.RPC("RPC_Move_FromC_ToC2", RpcTarget.Others, i, j, true);
            }
            base.photonView.RPC("RPC_TPEfects", RpcTarget.All, Tablero.instance.Get_Numero_Casilla(origen.gameObject), Tablero.instance.Get_Numero_Casilla(destino.gameObject));
        }
        else
        {
            TPEfects(origen, destino);
            pieza.transform.position = destino.transform.position;
        }


        Tablero.instance.ResetCasillasEfects();
    }

    public void TPEfects(Casilla origen, Casilla destino)
    {
        origen.SetState(States.tpOut);
        destino.SetState(States.tpIn);
    }
    [PunRPC]
    public void RPC_TPEfects(int origen, int destino)
    {
        Casilla cOrigen = Tablero.instance.mapa[origen];
        Casilla cDestino = Tablero.instance.mapa[destino];
        cOrigen.SetState(States.tpOut);
        cDestino.SetState(States.tpIn);
    }
}
